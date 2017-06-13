using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BabyDinoHerd.ProceduralTweening
{
    interface IVelocityTween<T, D> where T : IValueTweenable<T, D> where D : IDerivativeTweenable<T, D>
    {
        T Target { get; set; }
        T Value { get; set; }
        void UpdateTween(float deltaTime);
    }
}
