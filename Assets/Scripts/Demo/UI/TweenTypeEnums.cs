using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BabyDinoHerd.ProceduralTweening.Demo
{
    public enum TweenableInputType
    {
        None,
        MouseControlsPosition,
        MouseControlsRotation,
        MouseControlsPeriodicPosition,
        TimedRandomPositions,
    }

    public enum TweenType
    {
        None,
        ExponentialDamping,
        DampedOscillator,
        BounceOscillator,
        InfiniteTimeDamping,
        FiniteTimeDamping,
    }
}
