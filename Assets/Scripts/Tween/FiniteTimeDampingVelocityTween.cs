using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BabyDinoHerd.ProceduralTweening
{
    class FiniteTimeDampingVelocityTween<T, D> : VelocityTweenBase<T, D> where T : IValueTweenable<T, D> where D : IDerivativeTweenable<T, D>
    {
        private float _decelerationFactor;
        public float DecelerationFactor { get { return _decelerationFactor; } set { _decelerationFactor = Math.Max(0, value); } }
        public float CurrentDecelerationTime { get { return GetCurrentDecelerationTime(); } set { SetCurrentDecelerationTime(value); } }

        private float _decelerationPower = 0f;
        public float Linearity { get { return 1f - _decelerationPower; } set { _decelerationPower = 1f - MakeNearlyPositive(value); } }

        public FiniteTimeDampingVelocityTween(T value, T target, float linearity, float currentDecelerationTime) : base(value, target)
        {
            Linearity = linearity;
            CurrentDecelerationTime = currentDecelerationTime;
        }

        private float GetCurrentDecelerationTime()
        {
            var distance = Target.DifferenceFrom(Value).Magnitude;
            var linearity = Linearity;
            if(distance == 0f) { return 0f; }
            return (float)(Math.Pow(distance, linearity) / (linearity * _decelerationFactor));
        }

        private void SetCurrentDecelerationTime(float decelerationTime)
        {
            var distance = Target.DifferenceFrom(Value).Magnitude;
            var linearity = Linearity;
            _decelerationFactor = (float)(Math.Pow(distance, linearity) / (linearity * decelerationTime));
        }

        private static float MakeNearlyPositive(float value)
        {
            return Math.Max(1e-6f, value);
        }

        protected override float CalculateSpeed(float targetDistance)
        {
            return (float)(_decelerationFactor * Math.Pow(targetDistance, _decelerationPower));
        }
    }
}