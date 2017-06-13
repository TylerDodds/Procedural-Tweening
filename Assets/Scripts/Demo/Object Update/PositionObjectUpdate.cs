using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BabyDinoHerd.ProceduralTweening.Demo
{
    class PositionUpdateObject : UpdateObjectBase<TweenableVector3Value, TweenableVector3Derivative>
    {
        public PositionUpdateObject(TweenType tweenType, TweenableVector3Value target, TweenableVector3Value value, TweenableVector3Derivative derivative, TweenUpdateCondition tweenUpdateCondition, float initialSlider1Value, float initialSlider2Value)
            : base(tweenType, target, value, derivative, tweenUpdateCondition, initialSlider1Value, initialSlider2Value)
        {

        }

        internal const float DistanceFromCamera = 10f;

        protected override bool IsUserInputForTargetUpdate()
        {
            return base.IsUserInputForTargetUpdate();
        }

        protected override IVelocityTween<TweenableVector3Value, TweenableVector3Derivative> BaseTween
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
                var target = PointerToWorldPosition();
                TweenWrapper.Tween.Target = target;
            }
        }

        protected override void UpdateObjectFromTweenValue(GameObject gameObject)
        {
            gameObject.transform.position = TweenWrapper.Tween.Value;
        }

        private Vector3 PointerToWorldPosition()
        {
            var screenPosition = Input.mousePosition;
            screenPosition.z = DistanceFromCamera;
            var worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }
    }
}
