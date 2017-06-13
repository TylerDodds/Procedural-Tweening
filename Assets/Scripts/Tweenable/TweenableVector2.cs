using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BabyDinoHerd.ProceduralTweening
{
    struct TweenableVector2Value : IValueTweenable<TweenableVector2Value, TweenableVector2Derivative>
    {
        public TweenableVector2Value Value { get { return _value; } }
        private Vector2 _value;

        public TweenableVector2Value(Vector2 vector)
        {
            _value = vector;
        }

        public float Magnitude
        {
            get {return _value.magnitude; }
        }

        public TweenableVector2Value Inverse
        {
            get { return -_value; }
        }

        public TweenableVector2Derivative AsDerivative
        {
            get { return _value; }
        }

        public float InnerProduct(TweenableVector2Value other)
        {
            return Vector2.Dot(_value, other);
        }

        public TweenableVector2Value CompositionFraction(float fraction)
        {
            return _value * fraction;
        }

        public TweenableVector2Value ComposeWith(TweenableVector2Value other)
        {
            return _value + other;
        }

        public TweenableVector2Value IntegrateVelocity(TweenableVector2Derivative velocity, float deltaTime)
        {
            return _value + (Vector2)velocity * deltaTime;
        }

        public static implicit operator Vector2(TweenableVector2Value differentiableVector2)
        {
            return differentiableVector2._value;
        }

        public static implicit operator TweenableVector2Value(Vector2 vector)
        {
            return new TweenableVector2Value(vector);
        }
    }

    struct TweenableVector2Derivative : IDerivativeTweenable<TweenableVector2Value, TweenableVector2Derivative>, 
                                        IDerivativeTweenable<TweenablePeriodicVector2Value, TweenableVector2Derivative>
    {
        public TweenableVector2Derivative Value { get { return _value; } }
        private Vector2 _value;

        public TweenableVector2Derivative(Vector2 vector)
        {
            _value = vector;
        }

        public float Magnitude
        {
            get { return _value.magnitude; }
        }

        public TweenableVector2Derivative Normalized
        {
            get { return _value.normalized; }
        }

        public float InnerProduct(TweenableVector2Derivative other)
        {
            return Vector2.Dot(_value, other);
        }

        public TweenableVector2Derivative CompositionFraction(float fraction)
        {
            return _value * fraction;
        }

        public TweenableVector2Derivative ComposeWith(TweenableVector2Derivative other)
        {
            return _value + other;
        }

        public static implicit operator Vector2(TweenableVector2Derivative derivativeVector2)
        {
            return derivativeVector2._value;
        }

        public static implicit operator TweenableVector2Derivative(Vector2 vector)
        {
            return new TweenableVector2Derivative(vector);
        }
    }
}
