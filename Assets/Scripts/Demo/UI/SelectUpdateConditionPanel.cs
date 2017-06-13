using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace BabyDinoHerd.ProceduralTweening.Demo
{
    class SelectUpdateConditionPanel : MonoBehaviour
    {
        public bool UpdateOnlyOnClick { get { return _updateOnlyOnClickToggle.isOn; } }

        public UnityBoolEvent OnUpdateClickConditionChanged = new UnityBoolEvent();

        [SerializeField]
        private Toggle _updateOnlyOnClickToggle;

        void Awake()
        {
            _updateOnlyOnClickToggle.onValueChanged.AddListener(InvokeUpdateClickConditionChanged);
        }

        private void InvokeUpdateClickConditionChanged(bool value)
        {
            OnUpdateClickConditionChanged.Invoke(value);
        }
    }
}
