using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BabyDinoHerd.ProceduralTweening.Demo
{
    /// <summary>
    /// Manages selections for tween inputs and tween types.
    /// </summary>
    public class TweenSelectionManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _tweenedObject;

        [SerializeField]
        private UnityEngine.UI.Text _tooltipText;

        private SelectTweenableInputPanel _selectTweenableInputPanel;
        private SelectTweenTypePanel _selectTweenTypePanel;
        private SelectUpdateConditionPanel _selectUpdateConditionPanel;

        private TweenTypePanel[] _tweenTypePanels;
        private TweenableInputPanel[] _tweenableInputPanels;

        public int NumberTweenTypePanels { get { return _tweenTypePanels.Length; } }
        public int NumberTweenableInputPanels { get { return _tweenableInputPanels.Length; } }

        public TweenTypePanel CurrentActiveTweenTypePanel { get; private set; }
        public TweenableInputPanel CurrentActiveTweenableInputPanel { get; private set; }

        private IObjectUpdate _updateObjectPositon;
        private IObjectUpdate _updateObjectRotation;

        private TooltipManager _tooltipManager;

        #region startup

        void Awake()
        {
            _tweenTypePanels = GetComponentsInChildren<TweenTypePanel>(includeInactive: true);
            _tweenableInputPanels = GetComponentsInChildren<TweenableInputPanel>(includeInactive: true);

            _selectTweenableInputPanel = GetComponentInChildren<SelectTweenableInputPanel>();
            _selectTweenableInputPanel.OnInputTypeChanged.AddListener(HandleInputTypeChanged);

            _selectTweenTypePanel = GetComponentInChildren<SelectTweenTypePanel>();
            _selectTweenTypePanel.OnTweenTypeChanged.AddListener(HandleTweenTypeChanged);

            _selectUpdateConditionPanel = GetComponentInChildren<SelectUpdateConditionPanel>();
            _selectUpdateConditionPanel.OnUpdateClickConditionChanged.AddListener(HandleUpdateConditionChanged);

            _tooltipManager = new TooltipManager();
            _tooltipManager.Initialize(_tooltipText, _tweenTypePanels, _tweenableInputPanels);
        }

        void Start()
        {
            HandleTweenTypeChanged(0);
            HandleInputTypeChanged(0);
            HandleUpdateConditionChanged(GetTweenUpdateCondition() == TweenUpdateCondition.OnlyOnClick);
        }

        #endregion

        #region get/set values

        private TweenableInputType GetInputType()
        {
            return CurrentActiveTweenableInputPanel == null ? TweenableInputType.None : CurrentActiveTweenableInputPanel.TweenableInputType;
        }

        private TweenType GetTweenType()
        {
            return CurrentActiveTweenTypePanel == null ? TweenType.None : CurrentActiveTweenTypePanel.TweenType;
        }

        private TweenUpdateCondition GetTweenUpdateCondition()
        {
            bool updateOnlyOnClick = _selectUpdateConditionPanel == null ? false : _selectUpdateConditionPanel.UpdateOnlyOnClick;
            return updateOnlyOnClick ? TweenUpdateCondition.OnlyOnClick : TweenUpdateCondition.Always;
        }

        private float GetSlider1Value()
        {
            return CurrentActiveTweenTypePanel == null ? 0f : CurrentActiveTweenTypePanel.GetSlider1Value();
        }

        private float GetSlider2Value()
        {
            return CurrentActiveTweenTypePanel == null ? 0f : CurrentActiveTweenTypePanel.GetSlider2Value();
        }

        private void SetTweenDescription()
        {
            var inputType = GetInputType();
            string sliderNote = inputType == TweenableInputType.MouseControlsRotation ? _updateObjectRotation.GetTweenDescription() : _updateObjectPositon.GetTweenDescription();
            _selectTweenTypePanel.SetTweenDescription(sliderNote);
        }

        #endregion

        #region set input or tween type

        private void SetInputTypeAndTween()
        {
            var inputType = GetInputType();
            var tweenType = GetTweenType();
            var updateOnlyOnClick = GetTweenUpdateCondition();
            var slider1Value = GetSlider1Value();
            var slider2Value = GetSlider2Value();

            switch (inputType)
            {
                case TweenableInputType.MouseControlsPosition:
                    var objectPosition = _tweenedObject.transform.position;
                    var objectScreenPosition = Camera.main.WorldToScreenPoint(objectPosition);
                    objectScreenPosition.z = PositionUpdateObject.DistanceFromCamera;
                    var targetPosition = Camera.main.ScreenToWorldPoint(objectScreenPosition);
                    _updateObjectPositon = new PositionUpdateObject(tweenType, targetPosition, objectPosition, Vector3.zero, updateOnlyOnClick, slider1Value, slider2Value);
                    _updateObjectRotation = new RotationUpdateObject(TweenType.ExponentialDamping, Quaternion.identity, _tweenedObject.transform.rotation, Vector3.zero, TweenUpdateCondition.Never, 0.2f, 0.2f);
                    break;
                case TweenableInputType.MouseControlsPeriodicPosition:
                    var objectViewportPosition = (Vector2)Camera.main.WorldToViewportPoint(_tweenedObject.transform.position);
                    _updateObjectPositon = new ViewportPositionUpdateObject(tweenType, objectViewportPosition, objectViewportPosition, Vector2.zero, updateOnlyOnClick, slider1Value, slider2Value);
                    _updateObjectRotation = new RotationUpdateObject(TweenType.ExponentialDamping, Quaternion.identity, _tweenedObject.transform.rotation, Vector3.zero, TweenUpdateCondition.Never, 0.2f, 0.2f);
                    break;
                case TweenableInputType.MouseControlsRotation:
                    var objectRotation = _tweenedObject.transform.rotation;
                    _updateObjectPositon = new PositionUpdateObject(TweenType.ExponentialDamping, Vector3.forward * -5f, _tweenedObject.transform.position, Vector3.zero, TweenUpdateCondition.Never, 0.2f, 0.2f);
                    _updateObjectRotation = new RotationUpdateObject(tweenType, objectRotation, objectRotation, Vector3.zero, updateOnlyOnClick, slider1Value, slider2Value);
                    break;
                case TweenableInputType.TimedRandomPositions:
                    break;
                case TweenableInputType.None:
                default:
                    break;
            }
        }

        #endregion

        #region update

        void Update()
        {
            UpdateTarget();
            UpdateTweens();
            SetTweenDescription();
        }

        private void UpdateTarget()
        {
            if (_updateObjectPositon != null)
            {
                _updateObjectPositon.UpdateTweenTargetInput();
            }
            if (_updateObjectRotation != null)
            {
                _updateObjectRotation.UpdateTweenTargetInput();
            }
        }

        private void UpdateTweens()
        {
            float slider1Value = GetSlider1Value();
            float slider2Value = GetSlider2Value();
            if(_updateObjectPositon != null)
            {
                _updateObjectPositon.UpdateTweenParametersAndObject(_tweenedObject, slider1Value, slider2Value, Time.deltaTime);
            }
            if (_updateObjectRotation != null)
            {
                _updateObjectRotation.UpdateTweenParametersAndObject(_tweenedObject, slider1Value, slider2Value, Time.deltaTime);
            }
        }

        #endregion

        #region respond to ui changed events

        private void HandleInputTypeChanged(int index)
        {
            for (int i = 0; i < _tweenableInputPanels.Length; i++)
            {
                var panel = _tweenableInputPanels[i];
                panel.gameObject.SetActive(i == index);
            }
            CurrentActiveTweenableInputPanel = _tweenableInputPanels[index];
            SetInputTypeAndTween();
            CurrentActiveTweenTypePanel.UpdateFromUpdateOnClick(GetTweenUpdateCondition());
        }

        private void HandleTweenTypeChanged(int index)
        {
            for(int i = 0; i < _tweenTypePanels.Length; i++)
            {
                var panel = _tweenTypePanels[i];
                panel.gameObject.SetActive(i == index);
            }
            CurrentActiveTweenTypePanel = _tweenTypePanels[index];
            CurrentActiveTweenTypePanel.UpdateFromUpdateOnClick(GetTweenUpdateCondition());
            SetInputTypeAndTween();
        }

        private void HandleUpdateConditionChanged(bool updateOnlyOnClick)
        {
            var tweenUpdateCondition = GetTweenUpdateCondition();
            CurrentActiveTweenTypePanel.UpdateFromUpdateOnClick(tweenUpdateCondition);
            SetInputTypeAndTween();
        }

        #endregion
    }
}