using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BabyDinoHerd.ProceduralTweening.Demo
{
    class RotationUpdateObject : UpdateObjectBase<TweenableQuaternionValue, TweenableQuaternionDerivative> 
    {
        public RotationUpdateObject(TweenType tweenType, TweenableQuaternionValue target, TweenableQuaternionValue value, TweenableQuaternionDerivative derivative, TweenUpdateCondition tweenUpdateCondition, float initialSlider1Value, float initialSlider2Value)
            : base(tweenType, target, value, derivative, tweenUpdateCondition, initialSlider1Value, initialSlider2Value)
        {

        }

        protected override bool IsUserInputForTargetUpdate()
        {
            return base.IsUserInputForTargetUpdate() || Input.mouseScrollDelta != Vector2.zero;
        }

        protected override IVelocityTween<TweenableQuaternionValue, TweenableQuaternionDerivative> BaseTween
        {
            get
            {
                return TweenWrapper.Tween;
            }
        }

        protected override void UpdateTargetInput()
        {
            if (DoUpdateTweenInfo)
            {
                var target = PointerToRotation();
                TweenWrapper.Tween.Target = target;
            }
        }

        protected override void UpdateObjectFromTweenValue(GameObject gameObject)
        {
            gameObject.transform.rotation = TweenWrapper.Tween.Value;
        }

        private Quaternion PointerToRotation()
        {
            float _zComponent = NetMouseWheel * 0.25f;
            var screenPosition = Input.mousePosition;
            var viewportPosition = Camera.main.ScreenToViewportPoint(screenPosition);
            viewportPosition.z = _zComponent;
            viewportPosition *= 180f;

            var euler = Quaternion.Euler(viewportPosition);
            return euler;
        }
    }
}
