# Procedural Tweening

##### Overview
*Procedural Tweening* allows easing/tweening of a value toward a target over time. Unlike traditional interpolation-based methods, this type of tweening can respond continuously to a new target value being assigned at any time.

##### IValueTweenable and IDerivativeTweenable
These interfaces define requirements for a value type and its derivative type to be procedurally tweenable. For simple types, such as floating-point types and 2D/3D vectors, the derivative type is the same as the value type. For other value types, it can be difference; for instance, quaternion value types (representing rotations) have a derivative of angular velocity, which is a three-dimensional (pseudo)vector.

Unity examples provided: floating-point (real) values, 2D and 3D vectors, quaternions, and 2D vectors periodic within the unit square.

##### IVelocityTween and IAccelerationTween
These interfaces define the tweening functions that act on the values to be tweened. They specify the velocity or acceleration of the value at any point in time, respectively.

Unity examples provided: exponential damping, finite-time damping, infinite-time damping, damped oscillator, damped bouncing oscillator.