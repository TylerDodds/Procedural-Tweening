using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BabyDinoHerd.ProceduralTweening.Demo
{
    abstract class UpdateObjectBase<T, D> : IObjectUpdate where T : IValueTweenable<T, D> where D : IDerivativeTweenable<T, D>
    {
        protected TweenWrapper<T, D> TweenWrapper { get; private set; }

        protected abstract IVelocityTween<T, D> BaseTween { get; }

        protected bool DoUpdateTweenInfo
        {
            get
            {
                bool doUpdate = TweenWrapper.TweenUpdateCondition == TweenUpdateCondition.Always || 
                    (TweenWrapper.TweenUpdateCondition == TweenUpdateCondition.OnlyOnClick && IsUserInputForTargetUpdate());
                doUpdate = doUpdate && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
                return doUpdate;
            }
        }

        protected virtual bool IsUserInputForTargetUpdate()
        {
            return Input.GetMouseButtonDown(0);
        }


        protected float NetMouseWheel { get { return _netMouseWheel; } }
        private float _netMouseWheel = 0f;

        public UpdateObjectBase(TweenType tweenType, T target, T value, D derivative, TweenUpdateCondition tweenUpdateCondition, float initialSlider1Value, float initialSlider2Value)
        {
            TweenWrapper = TweenWrapperFactory.GetTweenWrapper(tweenType, target, value, derivative, tweenUpdateCondition, initialSlider1Value, initialSlider2Value);
        }

        private void UpdateMouseWheel()
        {
            _netMouseWheel += (int)Input.mouseScrollDelta.y;
        }

        public void UpdateTweenTargetInput()
        {
            UpdateMouseWheel();
            UpdateTargetInput();
        }

        protected abstract void UpdateTargetInput();

        public void UpdateTweenParametersAndObject(GameObject gameObject, float slider1Value, float slider2Value, float deltaTime)
        {
            if (DoUpdateTweenInfo)
            {
                TweenWrapper.UpdateTweenParameters(slider1Value, slider2Value);
            }
            TweenWrapper.UpdateTweenOverTime(deltaTime);
            UpdateObjectFromTweenValue(gameObject);
        }

        protected abstract void UpdateObjectFromTweenValue(GameObject gameObject);

        public string GetTweenDescription()
        {
            return TweenWrapper.GetTweenDescription();
        }
    }
}
