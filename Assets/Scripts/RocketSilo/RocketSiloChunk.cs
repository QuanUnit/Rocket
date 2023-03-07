using System;
using UnityEngine;

namespace RocketSilo
{
    public class RocketSiloChunk : MonoBehaviour
    {
        [field:SerializeField][Min(0)] public float ChuckHeight = 3.5f;
        [field:SerializeField] public RocketSiloBox LeftBox;
        [field:SerializeField] public RocketSiloBox RightBox;
    }
}
