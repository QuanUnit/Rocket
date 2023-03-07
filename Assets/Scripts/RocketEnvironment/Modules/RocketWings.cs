using UnityEngine;

namespace RocketEnvironment.Modules
{
    public class RocketWings : RocketModule
    {
        private float _angleThreshold;
        private float _rotationSmoothness;

        private float _rotateFactor;

        public RocketWings(Rocket owner, float angleThreshold, float rotationSmoothness, bool isActive = true) : base(owner, isActive)
        {
            _angleThreshold = angleThreshold;
            _rotationSmoothness = rotationSmoothness;
        }

        public void ApplyRotateFactor(float rotateFactor)
        {
            _rotateFactor = rotateFactor;
        }

        protected override void OnFixedUpdate()
        {
            Vector3 oldEulers = Owner.transform.rotation.eulerAngles;

            oldEulers.x = NormalizeAngle(oldEulers.x);
            oldEulers.y = NormalizeAngle(oldEulers.y);
            oldEulers.z = NormalizeAngle(oldEulers.z);

            float zAngleTarget = Mathf.Clamp(Mathf.Abs(_rotateFactor) * _angleThreshold, 0, _angleThreshold);
            zAngleTarget = -Mathf.Sign(_rotateFactor) * zAngleTarget;

            Vector3 targetEulers = new Vector3(0, 0, zAngleTarget);
            targetEulers = Vector3.Lerp(oldEulers, targetEulers, _rotationSmoothness);
            Owner.transform.rotation = Quaternion.Euler(targetEulers);
        }

        private float NormalizeAngle(float angle)
        {
            float result = angle > 180 ? angle - 360 : angle;
            return result;
        }
    }
}