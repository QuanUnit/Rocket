using UnityEngine;

namespace CameraComponents
{
    [RequireComponent(typeof(Camera))]
    public class VerticalFollowingCamera : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;

        private void Update()
        {
            Follow();
        }

        private void Follow()
        {
            Vector3 oldPosition = transform.position;

            transform.position = new Vector3(oldPosition.x, _target.position.y, oldPosition.z) + _offset;
        }

        private void OnValidate()
        {
            Follow();
        }
    }
}