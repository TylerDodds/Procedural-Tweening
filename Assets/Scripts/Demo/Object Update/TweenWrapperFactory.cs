using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BabyDinoHerd.ProceduralTweening.Demo
{
    static class TweenWrapperFactory 
    {
        public static TweenWrapper<T, D> GetTweenWrapper<T, D>(TweenType tweenType, T target, T value, D derivative, TweenUpdateCondition tweenUpdateCondition, float initialSlider1Value, float initialSlider2Value) where T : IValueTweenable<T, D> where D : IDerivativeTweenable<T, D>
        {
            TweenWrapper<T, D> wrapper = null;
            switch(tweenType)
            {
                case TweenType.ExponentialDamping:
                    wrapper = new ExponentialDampingTweenWrapper<T, D>(target, value, derivative, tweenUpdateCondition, initialSlider1Value, initialSlider2Value);
                    break;
                case TweenType.DampedOscillator:
                    wrapper = new DampedOscillatorTweenWrapper<T, D>(target, value, derivative, tweenUpdateCondition, initialSlider1Value, initialSlider2Value);
                    break;
                case TweenType.BounceOscillator:
                    wrapper = new BounceOscillatorTweenWrapper<T, D>(target, value, derivative, tweenUpdateCondition, initialSlider1Value, initialSlider2Value);
                    break;
                case TweenType.InfiniteTimeDamping:
                    wrapper = new InfiniteTimeDampingTweenWrapper<T, D>(target, value, derivative, tweenUpdateCondition, initialSlider1Value, initialSlider2Value);
                    break;
                case TweenType.FiniteTimeDamping:
                    wrapper = new FiniteTimeDampingTweenWrapper<T, D>(target, value, derivative, tweenUpdateCondition, initialSlider1Value, initialSlider2Value);
                    break;
            }
            return wrapper;
        }

        public class ExponentialDampingTweenWrapper<T, D> : TweenWrapper<T, D> where T : IValueTweenable<T, D> where D : IDerivativeTweenable<T, D>
        {
            private ExponentialDampingVelocityTween<T, D> _tween;

            internal ExponentialDampingTweenWrapper(T target, T value, D derivative, TweenUpdateCondition tweenUpdateCondition, float initialSlider1Value, float initialSlider2Value)
                : base(tweenUpdateCondition)
            {
                _tween = new ExponentialDampingVelocityTween<T, D>(value, target, initialSlider1Value);
            }

            internal override IVelocityTween<T, D> Tween {  get { return _tween; } }

            internal override string GetTweenDescription()
            {
                return string.Format("A damped tween that reaches halfway to target in time {0}.", _tween.HalfLife);
            }

            internal override void UpdateTweenParameters(float value1, float value2)
            {
                _tween.HalfLife = value1;
            }
        }

        public class DampedOscillatorTweenWrapper<T, D> : TweenWrapper<T, D> where T : IValueTweenable<T, D> where D : IDerivativeTweenable<T, D>
        {
            private DampedOscillatorAccelerationTween<T, D> _tween;

            internal DampedOscillatorTweenWrapper(T target, T value, D derivative, TweenUpdateCondition tweenUpdateCondition, float initialSlider1Value, float initialSlider2Value)
                : base(tweenUpdateCondition)
            {
                _tween = new DampedOscillatorAccelerationTween<T, D>(value, derivative, target, 1e-6f, initialSlider2Value, initialSlider1Value);
            }

            internal override IVelocityTween<T, D> Tween { get { return _tween; } }

            internal override string GetTweenDescription()
            {
                return string.Format("A damped spring; keep damping fraction ({0}) below 1 to prevent overdamping.", _tween.Zeta);
            }

            internal override void UpdateTweenParameters(float value1, float value2)
            {
                _tween.Frequency = value2;
                _tween.HalfLife = value1;
            }
        }

        public class BounceOscillatorTweenWrapper<T, D> : TweenWrapper<T, D> where T : IValueTweenable<T, D> where D : IDerivativeTweenable<T, D>
        {
            private BounceOscillatorAccelerationTween<T, D> _tween;

            internal BounceOscillatorTweenWrapper(T target, T value, D derivative, TweenUpdateCondition tweenUpdateCondition, float initialSlider1Value, float initialSlider2Value)
                : base(tweenUpdateCondition)
            {
                _tween = new BounceOscillatorAccelerationTween<T, D>(value, derivative, target, 1e-6f, initialSlider2Value, initialSlider1Value);
            }

            internal override IVelocityTween<T, D> Tween { get { return _tween; } }

            internal override string GetTweenDescription()
            {
                return string.Format("A bounced damped spring; keep damping fraction ({0}) below 1 to prevent overdamping.", _tween.Zeta);
            }

            internal override void UpdateTweenParameters(float value1, float value2)
            {
                _tween.Frequency = value2;
                _tween.HalfLife = value1;
            }
        }

        public class InfiniteTimeDampingTweenWrapper<T, D> : TweenWrapper<T, D> where T : IValueTweenable<T, D> where D : IDerivativeTweenable<T, D>
        {
            private InfiniteTimeDampingVelocityTween<T, D> _tween;

            internal InfiniteTimeDampingTweenWrapper(T target, T value, D derivative, TweenUpdateCondition tweenUpdateCondition, float initialSlider1Value, float initialSlider2Value)
                : base(tweenUpdateCondition)
            {
                _tween = new InfiniteTimeDampingVelocityTween<T, D>(value, target, initialSlider1Value, initialSlider2Value);
            }

            internal override IVelocityTween<T, D> Tween { get { return _tween; } }

            internal override string GetTweenDescription()
            {
                return string.Format("A damped tween whose velocity never reaches zero.");
            }

            internal override void UpdateTweenParameters(float value1, float value2)
            {
                _tween.InverseLinearity = value1;
                _tween.DecelerationFactor = value2;
            }

            internal override void UpdateTweenOverTime(float deltaTime)
            {
                int reps = 100;
                for (int i = 0; i < reps; i++)
                {
                    _tween.UpdateTween(deltaTime / reps);
                }
            }
        }

        public class FiniteTimeDampingTweenWrapper<T, D> : TweenWrapper<T, D> where T : IValueTweenable<T, D> where D : IDerivativeTweenable<T, D>
        {
            private FiniteTimeDampingVelocityTween<T, D> _tween;

            internal FiniteTimeDampingTweenWrapper(T target, T value, D derivative, TweenUpdateCondition tweenUpdateCondition, float initialSlider1Value, float initialSlider2Value)
                : base(tweenUpdateCondition)
            {
                _tween = new FiniteTimeDampingVelocityTween<T, D>(value, target, initialSlider1Value, initialSlider2Value);
            }

            internal override IVelocityTween<T, D> Tween { get { return _tween; } }

            internal override string GetTweenDescription()
            {
                return string.Format("A damped tween taking a finite amount of time {0}.", _tween.CurrentDecelerationTime);
            }

            internal override void UpdateTweenParameters(float value1, float value2)
            {
                _tween.Linearity = value1;
                if (TweenUpdateCondition == TweenUpdateCondition.OnlyOnClick)
                {
                    _tween.CurrentDecelerationTime = value2;
                }
                else if(TweenUpdateCondition == TweenUpdateCondition.Always)
                {
                    _tween.DecelerationFactor = value2;
                }
            }
        }
    }
}
