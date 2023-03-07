using System;

namespace RocketEnvironment.InputServices
{
    public interface IRocketInputService
    {
        event Action Pressed;
        event Action Released;
        event Action<float> RotateFactorChanged;
    }
}