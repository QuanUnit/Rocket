using System;
using UnityEngine;

namespace Player
{
    public abstract class RocketInputService : MonoBehaviour
    {
        public event Action Pressed;
        public event Action Released;
        public event Action<float> RotateFactorChanged;
    }
}