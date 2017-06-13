using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BabyDinoHerd.ProceduralTweening.Demo
{
    public class SelectTweenTypePanel : MonoBehaviour
    {
        [SerializeField]
        private Text _tweenTypeDescriptionText;

        [SerializeField]
        private Text _tweenNoteText;

        private TweenSelectionManager _manager;
        private Slider _tweenTypeSlider;

        public UnityIntEvent OnTweenTypeChanged = new UnityIntEvent();

        void Awake()
        {
            _manager = GetComponentInParent<TweenSelectionManager>();

            _tweenTypeSlider = GetComponentInChildren<Slider>();

            _tweenTypeSlider.onValueChanged.AddListener(HandleTypeChanged);
        }

        void Start()
        {
            _tweenTypeSlider.maxValue = _manager.NumberTweenTypePanels - 1;
            _tweenTypeSlider.value = 0;
            HandleTypeChanged(0);
        }

        void Update()
        {

        }

        internal void SetTweenDescription(string noteText)
        {
            _tweenNoteText.text = noteText;
        }

        private void HandleTypeChanged(float type)
        {
            int typeIndex = (int)type;
            OnTweenTypeChanged.Invoke(typeIndex);            
            _tweenTypeDescriptionText.text = _manager.CurrentActiveTweenTypePanel.Description;
        }

    }
}