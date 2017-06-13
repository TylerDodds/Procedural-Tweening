using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BabyDinoHerd.ProceduralTweening
{
    public abstract class AccelerationTweenBase<T, D> : IAccelerationTween<T, D> where T : IValueTweenable<T, D> where D : IDerivativeTweenable<T, D>
    {

        public AccelerationTweenBase(T value, D velocity, T target, float epsilon)
        {
            _value = value;
            _velocity = velocity;
            _target = target;
            _epsilon = epsilon;
        }

        private float _epsilon;
        private float Epsilon
        {
            get { return _epsilon; }
        }

        private T _value;
        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private D _velocity;
        public D Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        private T _target;
        public T Target
        {
            get { return _target; }
            set { _target = value; }
        }

        protected abstract void UpdateTweenVelocityAndValue(float deltaTime);

        public void UpdateTween(float deltaTime)
        {   
            UpdateTweenVelocityAndValue(deltaTime);
            if (Velocity.Magnitude < Epsilon && Target.DifferenceFrom(Value).Magnitude < Epsilon)
            {
                Velocity = Velocity.CompositionFraction(0f);
                Value = Target;
            }
        }
    }
}