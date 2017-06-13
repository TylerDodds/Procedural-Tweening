using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BabyDinoHerd.ProceduralTweening
{
    class InfiniteTimeDampingVelocityTween<T, D> : VelocityTweenBase<T, D> where T : IValueTweenable<T, D> where D : IDerivativeTweenable<T, D>
    {
        private float _decelerationFactor;
        public float DecelerationFactor { get { return _decelerationFactor; } set { _decelerationFactor = Math.Max(0, value); } }

        private float _decelerationPower = 0f;
        public float InverseLinearity { get { return -1f + _decelerationPower; } set { _decelerationPower = 1f + Math.Max(0f, value); } }

        public InfiniteTimeDampingVelocityTween(T value, T target, float inverseLinearity, float decelerationFactor) : base(value, target)
        {
            InverseLinearity = inverseLinearity;
            DecelerationFactor = decelerationFactor;
        }

        protected override float CalculateSpeed(float targetDistance)
        {
            return (float)(_decelerationFactor * Math.Pow(targetDistance, _decelerationPower));
        }
    }
}
