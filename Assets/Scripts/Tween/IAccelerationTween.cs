using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BabyDinoHerd.ProceduralTweening
{
    interface IAccelerationTween<T, D> : IVelocityTween<T, D> where T : IValueTweenable<T, D> where D : IDerivativeTweenable<T, D>
    {
        D Velocity { get; set; }
    }
}
