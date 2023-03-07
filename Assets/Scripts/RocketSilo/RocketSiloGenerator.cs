using RocketEnvironment;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace RocketSilo
{
    public class RocketSiloGenerator : MonoBehaviour
    {
        [SerializeField][Min(0)] private float _rearrangeChunkDistance = 10f;
        [SerializeField][Min(1)] private int _extensionChunkStep;
        [SerializeField][Range(0, 1)] private float _minExtensionChunkValue = 0.3f;

        private Queue<RocketSiloChunk> _chunksQueue;
        private long _chunkCounter;
        private Rocket _rocket;

        [Inject]
        public void Construct(Rocket rocket)
        {
            _rocket = rocket;
        }

        private void Awake()
        {
            RocketSiloChunk[] preparedChunks = GetComponentsInChildren<RocketSiloChunk>();
            _chunksQueue = new Queue<RocketSiloChunk>(preparedChunks);
        }

        private void FixedUpdate()
        {
            RocketSiloChunk lastChunk = _chunksQueue.Peek();
            Vector3 lastChunkPosition = lastChunk.transform.position;

            float distance = _rocket.transform.position.y - lastChunkPosition.y;

            if (distance >= _rearrangeChunkDistance)
            {
                RearrangeLastChunk();
            }
        }

        private void RearrangeLastChunk()
        {
            _chunkCounter++;
            
            RocketSiloChunk lastChunk = _chunksQueue.Dequeue();
            ResetChunk(lastChunk);
            
            if (_chunkCounter % _extensionChunkStep == 0)
            {
                SetRandomConfigureChuck(lastChunk);
            }

            float additiveY = (_chunksQueue.Count + 1) * lastChunk.ChuckHeight;
            lastChunk.transform.position += new Vector3(0, additiveY, 0);
            
            _chunksQueue.Enqueue(lastChunk);

            lastChunk = _chunksQueue.Peek();
            SetChuckAsSafeLine(lastChunk);
        }

        private void ResetChunk(RocketSiloChunk chunk)
        {
            chunk.LeftBox.ExtensionValue = 0;
            chunk.RightBox.ExtensionValue = 0;
        }
        
        private void SetRandomConfigureChuck(RocketSiloChunk chunk)
        {
            bool left = Random.Range(0, 2) == 0;

            RocketSiloBox box = left ? chunk.LeftBox : chunk.RightBox;
            box.ExtensionValue = Random.Range(_minExtensionChunkValue, 1);
        }

        private void SetChuckAsSafeLine(RocketSiloChunk chunk)
        {
            chunk.LeftBox.ExtensionValue = 1;
            chunk.RightBox.ExtensionValue = 1;
        }
    }
}