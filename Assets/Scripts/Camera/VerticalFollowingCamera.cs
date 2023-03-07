using RocketEnvironment;
using UnityEngine;
using Zenject;

namespace CameraComponents
{
    [RequireComponent(typeof(Camera))]
    public class VerticalFollowingCamera : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        private Rocket _rocket;

        [Inject]
        public void Construct(Rocket rocket)
        {
            _rocket = rocket;
        }

        private void Update()
        {
            Follow();
        }

        private void Follow()
        {
            Vector3 oldPosition = transform.position;

            transform.position = new Vector3(oldPosition.x, _rocket.transform.position.y, oldPosition.z) + _offset;
        }
    }
}