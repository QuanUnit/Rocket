using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Player
{
    public class MobileRocketInputService : IRocketInputService, ITickable
    {
        public event Action Pressed;
        public event Action Released;
        public event Action<float> RotateFactorChanged;

        private float RotateFactor
        {
            get => _rotateFactor;
            set
            {
                _rotateFactor = value;
                RotateFactorChanged?.Invoke(_rotateFactor);
            }
        }
        private float _rotateFactor;
        private Vector2? _cachedTouchPosition;
        
        public void Tick()
        {
            ProcessInput();
        }

        private void ProcessInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Press();
                return;
            }

            if (Input.GetMouseButtonUp(0))
            {
                Release();
                return;
            }

            if (Input.GetMouseButton(0))
            {
                Slide();
                return;
            }
        }

        private void Press()
        {
            _cachedTouchPosition = Input.mousePosition;
            Pressed?.Invoke();
        }

        private void Release()
        {
            _cachedTouchPosition = null;
            RotateFactor = 0;
            Released?.Invoke();
        }

        private void Slide()
        {
            Vector2 touchPosition = Input.mousePosition;
            
            float xDelta = touchPosition.x - _cachedTouchPosition.Value.x;
            _cachedTouchPosition = touchPosition;
            RotateFactor += (xDelta / Screen.width);
        }
    }
}
