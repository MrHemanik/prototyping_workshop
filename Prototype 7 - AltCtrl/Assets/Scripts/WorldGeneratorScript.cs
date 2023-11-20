using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorldGeneratorScript : MonoBehaviour
{
    public GameObject[] segments;
    private int _nextSegmentPositon = 0;
    public List<GameObject> _loadedSegments = new List<GameObject>();

    private void Start()
    {
        GenerateNextSegment();
    }

    private void GenerateNextSegment()
    {
        //get a random segment
        GameObject segment = segments[UnityEngine.Random.Range(0, segments.Length)];
        //instantiate it at the next position
        _loadedSegments.Add(Instantiate(segment, new Vector3(0, 0, _nextSegmentPositon), Quaternion.identity));
        //update the next position
        _nextSegmentPositon += 100;
    }

    public void OnNextSegment()
    {
        if (_loadedSegments.Count >= 2)
        {
        //destroy the oldest segment
        Destroy(_loadedSegments[0]);
        _loadedSegments.RemoveAt(0);
        }
    //generate a new segment
        GenerateNextSegment();
    }
    
}
