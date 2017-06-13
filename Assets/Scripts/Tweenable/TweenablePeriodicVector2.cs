using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BabyDinoHerd.ProceduralTweening
{
    struct TweenablePeriodicVector2Value : IValueTweenable<TweenablePeriodicVector2Value, TweenableVector2Derivative>
    {
        public TweenablePeriodicVector2Value Value { get { return _value; } }
        private Vector2 _value;

        public TweenablePeriodicVector2Value(Vector2 vector)
        {
            _value = vector;
        }

        public float Magnitude
        {
            get { return _value.magnitude; }
        }

        public TweenablePeriodicVector2Value Inverse
        {
            get { return -_value; }
        }

        public TweenableVector2Derivative AsDerivative
        {
            get { return _value; }
        }

        public float InnerProduct(TweenablePeriodicVector2Value other)
        {
            return Vector2.Dot(_value, other);
        }

        public TweenablePeriodicVector2Value CompositionFraction(float fraction)
        {
            return MakePeriodic(_value * fraction);
        }

        public TweenablePeriodicVector2Value ComposeWith(TweenablePeriodicVector2Value other)
        {
            return MakePeriodic(_value + other);
        }

        public TweenablePeriodicVector2Value IntegrateVelocity(TweenableVector2Derivative velocity, float deltaTime)
        {
            return MakePeriodic(_value + (Vector2)velocity * deltaTime);
        }

        public static implicit operator Vector2(TweenablePeriodicVector2Value differentiableVector2)
        {
            return differentiableVector2._value;
        }

        public static implicit operator TweenablePeriodicVector2Value(Vector2 vector)
        {
            return new TweenablePeriodicVector2Value(vector);
        }

        private static Vector2 MakePeriodic(Vector2 value)
        {
            return new Vector2(MakePeriodic(value.x), MakePeriodic(value.y));
        }

        private static float MakePeriodic(float value)
        {
            var plusHalf = value + 0.5f;
            var frac = plusHalf - Mathf.Floor(plusHalf);
            return frac - 0.5f;
        }
    }
}
