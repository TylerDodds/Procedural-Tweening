using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BabyDinoHerd.ProceduralTweening
{
    public class BounceOscillatorAccelerationTween<T, D> : DampedOscillatorTweenBase<T, D> where T : IValueTweenable<T, D> where D : IDerivativeTweenable<T, D>
    {
        public BounceOscillatorAccelerationTween(T value, D velocity, T target, float epsilon, float frequency, float halfLife)
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
            var targetDifference = Target.DifferenceFrom(Value);
            var targetDifferenceAsVelocity = targetDifference.AsDerivative;
            var deltaVelocityPart2 = targetDifferenceAsVelocity.CompositionFraction(deltaTime * Omega * Omega);
            var deltaVelocity = deltaVelocityPart1.ComposeWith(deltaVelocityPart2);
            
            var updatedVelocity = Velocity.ComposeWith(deltaVelocity);
            var directionForVelocity = targetDifferenceAsVelocity.Normalized.CompositionFraction(targetDifferenceAsVelocity.InnerProduct(updatedVelocity) >= 0f ? 1f : -1f);
            var newVelocity = directionForVelocity.CompositionFraction(updatedVelocity.Magnitude);
            var newValue = Value.IntegrateVelocity(newVelocity, deltaTime);

            var targetNewDifference = Target.DifferenceFrom(newValue);
            var innerProduct = targetDifference.InnerProduct((T)targetNewDifference);
            if (innerProduct < 0f)
            {
                var prevMag = targetDifference.Magnitude;
                var newMag = targetNewDifference.Magnitude;
                var newMagFraction = Mathf.Abs(newMag) / prevMag;
                newValue = Target.ComposeWith(Value.DifferenceFrom(Target).CompositionFraction(newMagFraction));
                newVelocity = newVelocity.CompositionFraction(-1.0f);
            }

            Velocity = newVelocity;
            Value = newValue;
        }
    }
}