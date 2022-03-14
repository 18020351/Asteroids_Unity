using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public float startDelay = 1f;
    public float repeatRate = 1f;
    public int spawnAmount = 1;
    public float spawnDistance = 15f;
    public float trajectoryVariance = 15f;
    public Asteroids asteroidsPrefabs;
    private void Start()
    {
        InvokeRepeating("Spawn", startDelay, repeatRate);
    }
    private void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroids asteroid = Instantiate(asteroidsPrefabs, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }

}
