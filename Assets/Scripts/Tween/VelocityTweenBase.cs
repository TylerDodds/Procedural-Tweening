using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BabyDinoHerd.ProceduralTweening
{
    public abstract class VelocityTweenBase<T, D> : IVelocityTween<T, D> where T : IValueTweenable<T, D> where D : IDerivativeTweenable<T, D>
    {

        public VelocityTweenBase(T value, T target)
        {
            _value = value;
            _target = target;
        }

        private T _value;
        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private T _target;

        public T Target
        {
            get { return _target; }
            set { _target = value; }
        }

        protected abstract float CalculateSpeed(float targetDistance);

        public void UpdateTween(float deltaTime)
        {
            UpdateTween_UsingDerivativeUnit(deltaTime);
        }

        private void UpdateTween_UsingDerivativeUnit(float deltaTime)
        { 
            var thisToTarget = Target.DifferenceFrom(Value);
            var thisToTargetAsDerivative = thisToTarget.AsDerivative;
            var unit = thisToTargetAsDerivative.Normalized;
            var targetDistance = thisToTargetAsDerivative.Magnitude;
            var calculatedSpeed = CalculateSpeed(targetDistance);
            if (calculatedSpeed * deltaTime < targetDistance)
            {
                var calculatedValue = Value.IntegrateVelocity(unit.CompositionFraction(calculatedSpeed), deltaTime);
                Value = calculatedValue;
            }
            else
            {
                Value = Target;
            }
        }
    }
}