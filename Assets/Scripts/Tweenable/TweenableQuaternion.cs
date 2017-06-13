using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BabyDinoHerd.ProceduralTweening
{
    struct TweenableQuaternionValue : IValueTweenable<TweenableQuaternionValue, TweenableQuaternionDerivative>
    {
        public TweenableQuaternionValue Value { get { return _value; } }
        private Quaternion _value;

        public TweenableQuaternionValue(Quaternion vector)
        {
            _value = vector;
        }

        public float Magnitude
        {
            get
            {
                float angle; Vector3 axis;
                _value.ToAngleAxis(out angle, out axis);
                return angle * Mathf.Deg2Rad;
            }
        }

        public TweenableQuaternionValue Inverse
        {
            get { return Quaternion.Inverse(_value); }
        }

        public TweenableQuaternionDerivative AsDerivative
        {
            get
            {
                float angle; Vector3 axis;
                _value.ToAngleAxis(out angle, out axis);
                return axis * angle * Mathf.Deg2Rad;
            }
        }

        public float InnerProduct(TweenableQuaternionValue other)
        {
            //NB Quaternion.Dot(Value, other.Value) will not work, since it's just cos(angle), which is even in angle
            float angle; Vector3 axis;
            _value.ToAngleAxis(out angle, out axis);
            float angle2; Vector3 axis2;
            ((Quaternion)other).ToAngleAxis(out angle2, out axis2);
            return Vector3.Dot(angle * axis * Mathf.Deg2Rad, angle2 * axis2 * Mathf.Deg2Rad);
        }

        public TweenableQuaternionValue CompositionFraction(float fraction)
        {
            float angle; Vector3 axis;
            _value.ToAngleAxis(out angle, out axis);
            return Quaternion.AngleAxis(angle * fraction, axis);
        }

        public TweenableQuaternionValue ComposeWith(TweenableQuaternionValue other)
        {
            return _value * other;
        }

        public TweenableQuaternionValue IntegrateVelocity(TweenableQuaternionDerivative velocity, float deltaTime)
        {
            var velocityToRotation = Quaternion.AngleAxis(velocity.Magnitude * deltaTime * Mathf.Rad2Deg, velocity.Normalized);
            return Value * velocityToRotation;
        }

        public static implicit operator Quaternion(TweenableQuaternionValue differentiableQuaternion)
        {
            return differentiableQuaternion._value;
        }

        public static implicit operator TweenableQuaternionValue(Quaternion vector)
        {
            return new TweenableQuaternionValue(vector);
        }
    }

    struct TweenableQuaternionDerivative : IDerivativeTweenable<TweenableQuaternionValue, TweenableQuaternionDerivative>
    {
        public TweenableQuaternionDerivative Value { get { return _value; } }
        private Vector3 _value;

        public TweenableQuaternionDerivative(Vector3 vector)
        {
            _value = vector;
        }

        public float Magnitude
        {
            get { return _value.magnitude; }
        }


        public TweenableQuaternionDerivative Normalized
        {
            get { return _value.normalized; }
        }

        public float InnerProduct(TweenableQuaternionDerivative other)
        {
            return Vector3.Dot(_value, other);
        }

        public TweenableQuaternionDerivative CompositionFraction(float fraction)
        {
            return _value * fraction;
        }

        public TweenableQuaternionDerivative ComposeWith(TweenableQuaternionDerivative other)
        {
            return _value + other;
        }

        public static implicit operator Vector3(TweenableQuaternionDerivative derivativeQuaternion)
        {
            return derivativeQuaternion._value;
        }

        public static implicit operator TweenableQuaternionDerivative(Vector3 vector)
        {
            return new TweenableQuaternionDerivative(vector);
        }
    }
}
