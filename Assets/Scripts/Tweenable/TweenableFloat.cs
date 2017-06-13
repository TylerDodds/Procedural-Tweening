using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BabyDinoHerd.ProceduralTweening
{
    struct TweenableFloatValue : IValueTweenable<TweenableFloatValue, TweenableFloatDerivative>
    {
        public TweenableFloatValue Value { get { return _value; } }
        private float _value;

        public TweenableFloatValue(float floatValue)
        {
            _value = floatValue;
        }

        public float Magnitude
        {
            get {return Mathf.Abs(_value); }
        }

        public TweenableFloatValue Inverse
        {
            get { return -_value; }
        }

        public TweenableFloatDerivative AsDerivative
        {
            get { return _value; }
        }

        public float InnerProduct(TweenableFloatValue other)
        {
            return _value * other;
        }

        public TweenableFloatValue CompositionFraction(float fraction)
        {
            return _value * fraction;
        }

        public TweenableFloatValue ComposeWith(TweenableFloatValue other)
        {
            return _value + other;
        }

        public TweenableFloatValue IntegrateVelocity(TweenableFloatDerivative velocity, float deltaTime)
        {
            return _value + (float)velocity * deltaTime;
        }

        public static implicit operator float(TweenableFloatValue differentiablefloat)
        {
            return differentiablefloat._value;
        }

        public static implicit operator TweenableFloatValue(float floatValue)
        {
            return new TweenableFloatValue(floatValue);
        }
    }

    struct TweenableFloatDerivative : IDerivativeTweenable<TweenableFloatValue, TweenableFloatDerivative>
    {
        public TweenableFloatDerivative Value { get { return _value; } }
        private float _value;

        public TweenableFloatDerivative(float floatValue)
        {
            _value = floatValue;
        }

        public float Magnitude
        {
            get { return Mathf.Abs(_value); }
        }


        public TweenableFloatDerivative Normalized
        {
            get { return Mathf.Sign(_value); }
        }

        public float InnerProduct(TweenableFloatDerivative other)
        {
            return _value * other;
        }

        public TweenableFloatDerivative CompositionFraction(float fraction)
        {
            return _value * fraction;
        }

        public TweenableFloatDerivative ComposeWith(TweenableFloatDerivative other)
        {
            return _value + other;
        }

        public static implicit operator float(TweenableFloatDerivative derivativefloat)
        {
            return derivativefloat._value;
        }

        public static implicit operator TweenableFloatDerivative(float floatValue)
        {
            return new TweenableFloatDerivative(floatValue);
        }
    }
}
