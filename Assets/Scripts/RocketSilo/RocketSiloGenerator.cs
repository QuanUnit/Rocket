using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RocketSilo
{
    public class RocketSiloGenerator : MonoBehaviour
    {
        [SerializeField] private Transform _rocket;
        [SerializeField][Min(0)] private float _rearrangeChunkDistance = 10f;
        [SerializeField][Min(1)] private int _extensionChunkStep;
        [SerializeField] private RocketSiloChunk[] _preparedChunks;

        private Queue<RocketSiloChunk> _chunksQueue;
        private long _chunkCounter;
        
        private void Awake()
        {
            if (_preparedChunks == null || _preparedChunks.Length == 0)
            {
                Debug.LogError("Prepared chunks of the rocket silo are not defined");
                return;
            }
            
            _chunksQueue = new Queue<RocketSiloChunk>(_preparedChunks);
        }

        private void FixedUpdate()
        {
            Vector3 lastChunkPosition = _chunksQueue.Peek().transform.position;
            Debug.Log(lastChunkPosition);
            float lastChunkDistance = _rocket.position.y - lastChunkPosition.y;

            if (lastChunkDistance >= _rearrangeChunkDistance)
            {
                RearrangeLastChunk();
            }
        }

        private void RearrangeLastChunk()
        {
            _chunkCounter++;
            
            RocketSiloChunk chunk = _chunksQueue.Dequeue();
            ResetChunk(chunk);
            
            if (_chunkCounter % _extensionChunkStep == 0)
            {
                SetRandomConfigureChuck(chunk);
            }

            float additiveY = (_chunksQueue.Count - 1) * chunk.ChuckHeight;
            chunk.transform.position += new Vector3(0, additiveY, 0);
            
            _chunksQueue.Enqueue(chunk);
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
            box.ExtensionValue = Random.value;
        }
    }
}