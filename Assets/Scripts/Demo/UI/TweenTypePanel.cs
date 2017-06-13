using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BabyDinoHerd.ProceduralTweening.Demo
{
    public class TweenTypePanel : MonoBehaviour
    {
        public TweenType TweenType { get { return _tweenType; } }
        [SerializeField]
        private TweenType _tweenType;

        [SerializeField]
        Slider _slider1;

        [SerializeField]
        Slider _slider2;

        public string Description { get { return TweenTypeDescription(_tweenType); } }


        void Awake()
        {
            
        }

        void Start()
        {

        }

        void Update()
        {
            
        }

        public float GetSlider2Value()
        {
            return _slider2 != null ? _slider2.value : 0f;
        }

        public float GetSlider1Value()
        {
            return _slider1 != null ? _slider1.value : 0f;
        }

        private string TweenTypeDescription(TweenType type)
        {
            string ret = "Unknown";
            switch(type)
            {
                case TweenType.ExponentialDamping:
                    ret = "Exponential Damping";
                    break;
                case TweenType.DampedOscillator:
                    ret = "Damped Oscillator";
                    break;
                case TweenType.BounceOscillator:
                    ret = "Bounce Oscillator";
                    break;
                case TweenType.InfiniteTimeDamping:
                    ret = "Infinite Time Damping";
                    break;
                case TweenType.FiniteTimeDamping:
                    ret = "FiniteTime  Damping";
                    break;
            }
            return ret;
        }

        internal void UpdateFromUpdateOnClick(TweenUpdateCondition tweenUpdateCondition)
        {
            switch (_tweenType)
            {
                case TweenType.ExponentialDamping:
                    
                    break;
                case TweenType.DampedOscillator:
                    
                    break;
                case TweenType.BounceOscillator:
                    
                    break;
                case TweenType.InfiniteTimeDamping:
                    
                    break;
                case TweenType.FiniteTimeDamping:
                    var slider2Text = GetSlider2Text();
                    if (tweenUpdateCondition == TweenUpdateCondition.OnlyOnClick)
                    {
                        slider2Text.text = "Tweening Time";
                    }
                    else
                    {
                        slider2Text.text = "Deceleration Amount";
                    }
                    break;
            }
        }

        private Text GetSlider1Text()
        {
            return _slider1.transform.parent.GetComponentInChildren<Text>();
        }

        private Text GetSlider2Text()
        {
            return _slider2.transform.parent.GetComponentInChildren<Text>();
        }
    }
}