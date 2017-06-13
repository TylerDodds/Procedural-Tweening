using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BabyDinoHerd.ProceduralTweening
{
    public class DampedOscillatorAccelerationTween<T, D> : DampedOscillatorTweenBase<T, D> where T : IValueTweenable<T, D> where D : IDerivativeTweenable<T, D>
    {
        public DampedOscillatorAccelerationTween(T value, D velocity, T target, float epsilon, float frequency, float halfLife) 
            : base(value, velocity, target, epsilon, frequency, halfLife)
        {

        }

        protected override void UpdateTweenVelocityAndValue(float deltaTime)
        {
            UpdateSemiImplicitEuler(deltaTime);
        }

        private void UpdateSemiImplicitEuler(float deltaTime)
        {
            var deltaVelocityPart1 = Velocity.CompositionFraction(-2.0f * deltaTime * Zeta * Omega);
            var targetDifferenceAsVelocity = Target.DifferenceFrom(Value).AsDerivative;
            var deltaVelocityPart2 = targetDifferenceAsVelocity.CompositionFraction(deltaTime * Omega * Omega);
            var deltaVelocity = deltaVelocityPart1.ComposeWith(deltaVelocityPart2);
            var newVelocity = Velocity.ComposeWith(deltaVelocity);
            var newValue = Value.IntegrateVelocity(newVelocity, deltaTime);
            Velocity = newVelocity;
            Value = newValue;
        }
    }
}