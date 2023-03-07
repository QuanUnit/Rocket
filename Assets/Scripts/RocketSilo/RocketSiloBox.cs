using System;
using UnityEngine;

namespace RocketSilo
{
    public class RocketSiloBox : MonoBehaviour
    {
        public float ExtensionValue
        {
            get => _extensionValue;
            set
            {
                value = Mathf.Clamp01(value);
                
                Vector3 position = transform.position;
                float x = Mathf.Lerp(_xStart, _xEnd, value);
                transform.position = new Vector3(x, position.y, position.z);
            }
        }
        
        [SerializeField][Range(0, 1)] private float _extensionValue;
        [SerializeField] private float _xStart;
        [SerializeField] private float _xEnd;

        private void OnValidate()
        {
            ExtensionValue = _extensionValue;
        }
    }
}