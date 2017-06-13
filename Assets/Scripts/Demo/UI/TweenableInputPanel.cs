using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BabyDinoHerd.ProceduralTweening.Demo
{
    public class TweenableInputPanel : MonoBehaviour
    {
        public TweenableInputType TweenableInputType { get { return _tweenableInputType; } }
        [SerializeField]
        private TweenableInputType _tweenableInputType;

        public string Description { get { return TweenableInputTypeDescription(_tweenableInputType); } }

        void Start()
        {

        }

        void Update()
        {

        }

        private string TweenableInputTypeDescription(TweenableInputType type)
        {
            string ret = "Unknown";
            switch (type)
            {
                case TweenableInputType.MouseControlsPosition:
                    ret = "Mouse Controls Position";
                    break;
                case TweenableInputType.MouseControlsRotation:
                    ret = "Mouse Controls Rotation";
                    break;
                case TweenableInputType.MouseControlsPeriodicPosition:
                    ret = "Mouse Controls Position (Wrapped)";
                    break;
                case TweenableInputType.TimedRandomPositions:
                    ret = "Randomly Changing Positions";
                    break;
            }
            return ret;
        }
    }
}