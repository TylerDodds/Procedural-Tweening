using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BabyDinoHerd.ProceduralTweening
{
    public interface IValueTweenable<T, D> where T : IValueTweenable<T, D> where D : IDerivativeTweenable<T, D>
    {
        T Value { get; }

        float Magnitude { get; }

        T Inverse { get; }
        D AsDerivative { get; }

        float InnerProduct(T other);
        T CompositionFraction(float fraction);
        T ComposeWith(T other);
        T IntegrateVelocity(D velocity, float deltaTime);
    }

    public interface IDerivativeTweenable<T, D> where T : IValueTweenable<T, D> where D : IDerivativeTweenable<T, D>
    {
        D Value { get; }

        float Magnitude { get; }        

        D Normalized { get; }

        float InnerProduct(D other);
        D CompositionFraction(float fraction);
        D ComposeWith(D other);
    }
}
