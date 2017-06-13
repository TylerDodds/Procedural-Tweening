using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BabyDinoHerd.ProceduralTweening
{
    struct TweenableVector3Value : IValueTweenable<TweenableVector3Value, TweenableVector3Derivative>
    {
        public TweenableVector3Value Value { get { return _value; } }
        private Vector3 _value;

        public TweenableVector3Value(Vector3 vector)
        {
            _value = vector;
        }

        public float Magnitude
        {
            get { return _value.magnitude; }
        }

        public TweenableVector3Value Inverse
        {
            get { return -_value; }
        }

        public TweenableVector3Derivative AsDerivative
        {
            get { return _value; }
        }

        public float InnerProduct(TweenableVector3Value other)
        {
            return Vector3.Dot(_value, other);
        }

        public TweenableVector3Value CompositionFraction(float fraction)
        {
            return _value * fraction;
        }

        public TweenableVector3Value ComposeWith(TweenableVector3Value other)
        {
            return _value + other;
        }

        public TweenableVector3Value IntegrateVelocity(TweenableVector3Derivative velocity, float deltaTime)
        {
            return _value + (Vector3)velocity * deltaTime;
        }

        public static implicit operator Vector3(TweenableVector3Value differentiableVector3)
        {
            return differentiableVector3._value;
        }

        public static implicit operator TweenableVector3Value(Vector3 vector)
        {
            return new TweenableVector3Value(vector);
        }
    }

    struct TweenableVector3Derivative : IDerivativeTweenable<TweenableVector3Value, TweenableVector3Derivative>
    {
        public TweenableVector3Derivative Value { get { return _value; } }
        private Vector3 _value;

        public TweenableVector3Derivative(Vector3 vector)
        {
            _value = vector;
        }

        public float Magnitude
        {
            get { return _value.magnitude; }
        }


        public TweenableVector3Derivative Normalized
        {
            get { return _value.normalized; }
        }

        public float InnerProduct(TweenableVector3Derivative other)
        {
            return Vector3.Dot(_value, other);
        }

        public TweenableVector3Derivative CompositionFraction(float fraction)
        {
            return _value * fraction;
        }

        public TweenableVector3Derivative ComposeWith(TweenableVector3Derivative other)
        {
            return _value + other;
        }

        public static implicit operator Vector3(TweenableVector3Derivative derivativeVector3)
        {
            return derivativeVector3._value;
        }

        public static implicit operator TweenableVector3Derivative(Vector3 vector)
        {
            return new TweenableVector3Derivative(vector);
        }
    }
}
