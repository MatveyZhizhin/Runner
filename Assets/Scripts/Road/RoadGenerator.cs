using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Road.SpawnOfObjects;

namespace Assets.Scripts.Road
{
    public class RoadGenerator : MonoBehaviour
    {
        private List<RoadSegment> _roadSegments = new List<RoadSegment>();

        [SerializeField] private RoadSegment _roadSegmentPrefab;
        [SerializeField] private Transform _spawnPoint;

        [SerializeField] private int _roadSegmentCount;

        private void Start()
        {
            GenerateRoad();
        }

        private void GenerateRoad()
        {
            var segmentSize = _roadSegmentPrefab.transform.localScale.x;
            for (int i = 0; i < _roadSegmentCount; i++)
            {
                var newRoadSegment = Instantiate(_roadSegmentPrefab, _spawnPoint.position, Quaternion.identity);
                _roadSegments.Add(newRoadSegment);
                newRoadSegment.transform.parent = transform;
                var newSpawnPointPosition = _spawnPoint.localPosition.x + segmentSize;
                ChangeSpawnPointPosition(newSpawnPointPosition);
            }

            
        }

        private void ChangeSpawnPointPosition(float newPostion = 0)
        {
            _spawnPoint.localPosition = new Vector3(newPostion, _spawnPoint.localPosition.y, _spawnPoint.localPosition.z);
        }
    }
}
