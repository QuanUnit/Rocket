using System;

namespace Player
{
    public interface IRocketInputService
    {
        event Action Pressed;
        event Action Released;
        event Action<float> RotateFactorChanged;
    }
}