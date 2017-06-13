using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BabyDinoHerd.ProceduralTweening.Demo
{
    class ViewportPositionUpdateObject : UpdateObjectBase<TweenablePeriodicVector2Value, TweenableVector2Derivative>
    {
        public ViewportPositionUpdateObject(TweenType tweenType, TweenablePeriodicVector2Value target, TweenablePeriodicVector2Value value, TweenableVector2Derivative derivative, TweenUpdateCondition tweenUpdateCondition, float initialSlider1Value, float initialSlider2Value)
            : base(tweenType, target, value, derivative, tweenUpdateCondition, initialSlider1Value, initialSlider2Value)
        {

        }

        protected override bool IsUserInputForTargetUpdate()
        {
            return base.IsUserInputForTargetUpdate();
        }

        protected override IVelocityTween<TweenablePeriodicVector2Value, TweenableVector2Derivative> BaseTween
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
                var target = PointerToViewportPosition();
                TweenWrapper.Tween.Target = target;
            }
        }

        protected override void UpdateObjectFromTweenValue(GameObject gameObject)
        {
            var currentGameObjectViewportPos = Camera.main.WorldToViewportPoint(gameObject.transform.position);
            const float targetViewportZ = 10f;

            var tweenViewportPos = (Vector3)(Vector2)TweenWrapper.Tween.Value;
            if (tweenViewportPos.x < 0f) { tweenViewportPos.x += 1.0f; }
            if (tweenViewportPos.y < 0f) { tweenViewportPos.y += 1.0f; }
            tweenViewportPos.z = currentGameObjectViewportPos.z + (targetViewportZ - currentGameObjectViewportPos.z) * Time.deltaTime * (float)Math.Log(2.0) / 0.2f;
            gameObject.transform.position = Camera.main.ViewportToWorldPoint(tweenViewportPos);
        }

        private Vector2 PointerToViewportPosition()
        {
            const float distanceFromCamera = 10f;
            var screenPosition = Input.mousePosition;
            screenPosition.z = distanceFromCamera;
            var viewportPosition = Camera.main.ScreenToViewportPoint(screenPosition);
            return viewportPosition;
        }
    }
}
