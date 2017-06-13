using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BabyDinoHerd.ProceduralTweening.Demo
{
    class TooltipManager
    {
        public TooltipManager()
        {
        }

        public void Initialize(Text tooltipText, TweenTypePanel[] tweenTypePanels, TweenableInputPanel[] inputTypePanels)
        {
            tooltipText.gameObject.SetActive(false);

            foreach (var panel in tweenTypePanels)
            {
                AddTooltipToSliders(tooltipText, panel.gameObject);
            }
            foreach (var panel in inputTypePanels)
            {
                AddTooltipToSliders(tooltipText, panel.gameObject);
            }
        }

        private static void AddTooltipToSliders(Text tooltipText, GameObject parent)
        {
            var sliders = parent.GetComponentsInChildren<Slider>(includeInactive: true);
            foreach (var slider in sliders)
            {
                var tooltip = slider.gameObject.AddComponent<Tooltip>();
                tooltip.Initialize(0.2f, tooltipText, () => slider.value.ToString("0.00"), () => ((RectTransform )slider.transform).InverseTransformPoint(slider.handleRect.transform.position));
            }
        }
    }
}
