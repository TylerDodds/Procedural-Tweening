using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BabyDinoHerd.ProceduralTweening
{
    public abstract class DampedOscillatorTweenBase<T, D> : AccelerationTweenBase<T, D> where T : IValueTweenable<T, D> where D : IDerivativeTweenable<T, D>
    {
        private float _omega = 1f;
        private float _zeta = 1f;

        public float Frequency { get { return _omega / (Mathf.PI * 2f); } set { var previousHalfLife = HalfLife; _omega = 2f * Mathf.PI * MakeParameterNearlyPositive(value); HalfLife = previousHalfLife; } }
        public float HalfLife { get { return -Mathf.Log(0.5f) / (_omega * _zeta); } set { _zeta = -Mathf.Log(0.5f) / (_omega * MakeParameterNearlyPositive(value)); } }

        public float Omega { get { return _omega; } }
        public float Zeta { get { return _zeta; } }


        public DampedOscillatorTweenBase(T value, D velocity, T target, float epsilon, float frequency, float halfLife) :  base(value, velocity, target, epsilon)
        {
            SetFrequencyAndHalfLife(frequency, halfLife);
        }

        private void SetFrequencyAndHalfLife(float frequency, float halfLife)
        {
            HalfLife = MakeParameterNearlyPositive(halfLife);
            Frequency = MakeParameterNearlyPositive(frequency);
        }

        private static float MakeParameterNearlyPositive(float value)
        {
            return Math.Max(1e-6f, value);
        }

    }
}