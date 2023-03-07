using UnityEngine;

namespace RocketEnvironment.Modules
{
    public class RocketBallast : RocketModule
    {
        private float _ballastForce;

        public RocketBallast(Rocket owner, float ballastForce, bool isActive = true) : base(owner, isActive)
        {
            _ballastForce = ballastForce;
        }

        protected override void OnFixedUpdate()
        {
            Rigidbody.AddForce(Vector3.down * _ballastForce, ForceMode.Force);
        }
    }
}