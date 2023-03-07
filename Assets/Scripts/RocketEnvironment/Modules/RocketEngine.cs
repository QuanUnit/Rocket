using UnityEngine;

namespace RocketEnvironment.Modules
{
    public class RocketEngine : RocketModule
    {
        private float _acceleration;
        private float _maxVelocity;

        public RocketEngine(Rocket owner, float acceleration, float maxVelocity, bool isActive = true) : base(owner, isActive)
        {
            _acceleration = acceleration;
            _maxVelocity = maxVelocity;
        }

        protected override void OnFixedUpdate()
        {
            ApplyAcceleration();
        }

        private void ApplyAcceleration()
        {
            Vector3 direction = Rigidbody.transform.up;

            Rigidbody.velocity += direction * _acceleration;

            Vector3 velocity = Rigidbody.velocity;

            if (velocity.y > 0)
            {
                Rigidbody.velocity = Vector3.ClampMagnitude(velocity, _maxVelocity);
            }
        }
    }
}