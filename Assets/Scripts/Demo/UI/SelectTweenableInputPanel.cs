using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BabyDinoHerd.ProceduralTweening.Demo
{
    public class SelectTweenableInputPanel : MonoBehaviour
    {
        [SerializeField]
        private Text _inputTypeDescriptionText;

        private TweenSelectionManager _manager;
        private Slider _inputTypeSlider;

        public UnityIntEvent OnInputTypeChanged = new UnityIntEvent();
        

        void Awake()
        {
            _manager = GetComponentInParent<TweenSelectionManager>();

            _inputTypeSlider = GetComponentInChildren<Slider>();

            _inputTypeSlider.onValueChanged.AddListener(HandleTypeChanged);
        }

        void Start()
        {
            _inputTypeSlider.maxValue = _manager.NumberTweenableInputPanels - 1;
            _inputTypeSlider.value = 0;
            HandleTypeChanged(0);
        }

        void Update()
        {

        }

        private void HandleTypeChanged(float type)
        {
            int typeIndex = (int)type;
            OnInputTypeChanged.Invoke(typeIndex);
            _inputTypeDescriptionText.text = _manager.CurrentActiveTweenableInputPanel.Description;
        }
    }
}