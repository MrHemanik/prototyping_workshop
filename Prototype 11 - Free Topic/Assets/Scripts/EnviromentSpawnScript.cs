using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using Random = UnityEngine.Random;

public class EnviromentSpawnScript : MonoBehaviour
{
    public GameObject[] spawnableObjects;
    private SphereCollider[] _spawnableObjectsColliders;
    private int _spawnTriesPerResource = 10;
    private int _maxResources = 10;
    private float _timeTilResourceSpawn = 10f;
    
    //Internal
    private float _curTime = 0f;
    private void Start()
    {
        _spawnableObjectsColliders = new SphereCollider[spawnableObjects.Length];
        for (int i = 0; i < spawnableObjects.Length; i++)
        {
            _spawnableObjectsColliders[i] = spawnableObjects[i].GetComponent<SphereCollider>();
        }

        for (int i = 0; i < 3; i++)
        {
            StartCoroutine(SpawnObject());
        }
    }

    void FixedUpdate()
    {
        _curTime += Time.deltaTime;
        if (_curTime >= _timeTilResourceSpawn)
        {
            StartCoroutine(SpawnObject());
            _curTime = 0f;
        }
    }
    private IEnumerator SpawnObject()
    {
        if (transform.childCount >= _maxResources)
        {
            yield break;
        }
        int randomIndex = Random.Range(0, spawnableObjects.Length);
        float radius = _spawnableObjectsColliders[randomIndex].radius;
        int spawnTries = 0;
        while (spawnTries < _spawnTriesPerResource)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-4.8f,4.8f),0f,Random.Range(-4.8f,4.8f));
            Collider[] hitColliders = Physics.OverlapSphere(transform.position+randomPosition, radius);
            //find out if one of them has "resource" tag
            bool hasResource = false;
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.CompareTag("Resource")||hitCollider.gameObject.CompareTag("Player"))
                {
                    hasResource = true;
                }
            }
            if (hasResource)
            {
                spawnTries++;
            }
            else
            {
                Instantiate(spawnableObjects[randomIndex], transform.position+randomPosition, Quaternion.Euler(new Vector3(0,Random.Range(0f,360f),0)), transform);
                spawnTries = 1000;
            }
        }
    }
}
