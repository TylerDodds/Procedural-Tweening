using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BabyDinoHerd.ProceduralTweening
{
    public class ExponentialDampingVelocityTween<T, D> : VelocityTweenBase<T, D> where T : IValueTweenable<T, D> where D : IDerivativeTweenable<T, D>
    {
        private float _dampingFactor;
        public float HalfLife { get { return (float)Math.Log(2) / _dampingFactor; } set { _dampingFactor = (float)Math.Log(2) / Math.Max(1e-6f, value); } }

        public ExponentialDampingVelocityTween(T value, T target, float halfLife) : base(value, target)
        {
            var dampingFactor = (float)Math.Log(2) / halfLife;
            _dampingFactor = dampingFactor;
        }

        protected override float CalculateSpeed(float targetDistance)
        {
            return targetDistance * _dampingFactor;
        }
    }
}