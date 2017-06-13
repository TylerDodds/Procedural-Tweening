using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BabyDinoHerd.ProceduralTweening.Demo
{
    class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private float _waitTime = 0f;

        Coroutine _tooltipRoutine;
        private Text _tooltipTextObject;
        private Func<string> _textUpdate;
        private Func<Vector3> _getLocalPosition;

        public void Initialize(float waitTime, Text tooltipObject, Func<string> textUpdate, Func<Vector3> getLocalPosition)
        {
            _waitTime = waitTime;
            _tooltipTextObject = tooltipObject;
            _textUpdate = textUpdate;
            _getLocalPosition = getLocalPosition;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_tooltipRoutine != null)
            {
                StopCoroutine(_tooltipRoutine);
                _tooltipRoutine = null;
            }
            _tooltipRoutine = StartCoroutine(TooltipHover());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tooltipTextObject.gameObject.SetActive(false);
            if (_tooltipRoutine != null)
            {
                StopCoroutine(_tooltipRoutine);
                _tooltipRoutine = null;
            }
        }

        private IEnumerator TooltipHover()
        {
            yield return new WaitForSeconds(_waitTime);
            _tooltipTextObject.gameObject.SetActive(true);
            _tooltipTextObject.transform.SetParent(transform, worldPositionStays: false);
            bool keepGoing = true;
            while(keepGoing)
            {
                _tooltipTextObject.text = _textUpdate();
                Vector2 localPos = _getLocalPosition();
                _tooltipTextObject.transform.localPosition = localPos;
                yield return null;
            }
            yield break;
        }

        private Vector2 GetLocalPositionFromMousePosition()
        {
            Vector2 localPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform, Input.mousePosition, null, out localPos);
            return localPos;
        }
    }
}
