using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BabyDinoHerd.ProceduralTweening
{
    static class TweenableExtensions
    {
        public static IValueTweenable<T,D> DifferenceFrom<T,D>(this IValueTweenable<T,D> value, IValueTweenable<T, D> target) where T : IValueTweenable<T, D> where D : IDerivativeTweenable<T, D>
        {
            var inverse = target.Inverse;
            return inverse.ComposeWith(value.Value);
        }
    }
}
