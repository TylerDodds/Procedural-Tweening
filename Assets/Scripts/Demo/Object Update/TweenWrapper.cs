using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BabyDinoHerd.ProceduralTweening.Demo
{
    abstract class TweenWrapper<T, D> where T : IValueTweenable<T, D> where D : IDerivativeTweenable<T, D>
    {
        public TweenUpdateCondition TweenUpdateCondition { get; private set; }

        public TweenWrapper(TweenUpdateCondition tweenUpdateCondition)
        {
            TweenUpdateCondition = tweenUpdateCondition;
        }

        internal abstract IVelocityTween<T, D> Tween { get; }

        internal abstract void UpdateTweenParameters(float value1, float value2);

        internal virtual void UpdateTweenOverTime(float deltaTime)
        {
            Tween.UpdateTween(deltaTime);
        }

        internal abstract string GetTweenDescription();
    }
}
