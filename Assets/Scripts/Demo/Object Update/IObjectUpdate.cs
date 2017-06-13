using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BabyDinoHerd.ProceduralTweening.Demo
{
    public interface IObjectUpdate
    {
        void UpdateTweenTargetInput();
        void UpdateTweenParametersAndObject(GameObject gameObject, float slider1Value, float slider2Value, float deltaTime);
        string GetTweenDescription();
    }
}
