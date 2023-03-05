using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Rocket : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private RocketInputService _inputService;

        [Inject]
        public void Construct(RocketInputService inputService)
        {
            _inputService = inputService;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            _inputService.Released += ReleasedHandle;
            _inputService.Pressed += PressedHandle;
            _inputService.RotateFactorChanged += RotateFactorChangeHandle;
        }

        private void ReleasedHandle()
        {

        }

        private void PressedHandle()
        {

        }

        private void RotateFactorChangeHandle(float rotateFactor)
        {

        }

        private void OnDestroy()
        {
            _inputService.Released -= ReleasedHandle;
            _inputService.Pressed -= PressedHandle;
            _inputService.RotateFactorChanged -= RotateFactorChangeHandle;
        }
    }
}