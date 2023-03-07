using RocketEnvironment.InputServices;
using RocketEnvironment.Modules;
using UnityEngine;
using Zenject;

namespace RocketEnvironment
{
    [RequireComponent(typeof(Rigidbody))]
    public class Rocket : MonoBehaviour
    {
        [SerializeField][Min(0)] private float _engineAcceleration;
        [SerializeField][Min(0)] private float _engineMaxVelocity;

        [SerializeField][Range(0, 90)] private float _angleThreshold;
        [SerializeField][Range(0, 1)] private float _rotationSmoothness;

        [SerializeField][Min(0)] private float _ballastForce;

        private IRocketInputService _inputService;
        private Rigidbody _rigidbody;

        private RocketEngine _engine;
        private RocketWings _wings;
        private RocketBallast _ballast;

        [Inject]
        public void Construct(IRocketInputService inputService)
        {
            _inputService = inputService;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            _inputService.Released += ReleasedHandle;
            _inputService.Pressed += PressedHandle;
            _inputService.RotateFactorChanged += RotateFactorChangeHandle;

            CreateModules();
        }

        protected void CreateModules()
        {
            _engine = new RocketEngine(this, _engineAcceleration, _engineMaxVelocity, false);
            _wings = new RocketWings(this, _angleThreshold, _rotationSmoothness);
            _ballast = new RocketBallast(this, _ballastForce);
        }

        private void FixedUpdate()
        {
            _engine.FixedUpdate();
            _wings.FixedUpdate();
            _ballast.FixedUpdate();
        }

        private void PressedHandle()
        {
            DisalePhysics();
            _engine.IsModuleActive = true;
            _wings.IsModuleActive = true;
        }

        private void ReleasedHandle()
        {
            EnablePhysics();
            _engine.IsModuleActive = false;
            _wings.IsModuleActive = false;
        }

        private void RotateFactorChangeHandle(float rotateFactor)
        {
            _wings.ApplyRotateFactor(rotateFactor);
        }

        private void EnablePhysics()
        {
            _rigidbody.constraints =
                RigidbodyConstraints.FreezePositionZ |
                RigidbodyConstraints.FreezeRotationX;
        }

        private void DisalePhysics()
        {
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.constraints =
                RigidbodyConstraints.FreezePositionZ |
                RigidbodyConstraints.FreezeRotationX |
                RigidbodyConstraints.FreezeRotationZ;
        }

        private void OnDestroy()
        {
            _inputService.Released -= ReleasedHandle;
            _inputService.Pressed -= PressedHandle;
            _inputService.RotateFactorChanged -= RotateFactorChangeHandle;
        }
    }
}