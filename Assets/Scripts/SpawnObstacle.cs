using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefab;
    float initialSpeed = 10f;
    float maxSpeed = 35f;
    float speedIncreaseRate = 1f;
    float timeElapsed = 0f;
    int score = 0;
    float scoreIncreaseRate = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpwanObtacles());
        StartCoroutine(DelayedOperation());
    }

    IEnumerator SpwanObtacles()
    {
        while (true)
        {
            float randomDelay = UnityEngine.Random.Range(0.7f, 3f);
            yield return new WaitForSeconds(randomDelay);
            int i = UnityEngine.Random.Range(0, obstaclePrefab.Length);

            UnityEngine.Vector3 spawnPosition = transform.position;

            if (i == 1)
            {
                spawnPosition.y += 1;

            }
            GameObject obstacle = Instantiate(obstaclePrefab[i], spawnPosition, UnityEngine.Quaternion.identity);
            Obstacle obstacleScript = obstacle.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                obstacleScript.SetSpeed(initialSpeed);
            }
        }
    }

    void IncreaseSpeed()
    {
        initialSpeed += speedIncreaseRate;
        if (initialSpeed > maxSpeed)
        {
            initialSpeed = maxSpeed;
        }
        scoreIncreaseRate = initialSpeed / 10f;
    }

    IEnumerator DelayedOperation()
    {
        while (true)
        {
            score++;
            Debug.Log(score);
            yield return new WaitForSeconds(1f / scoreIncreaseRate);
        }
    }


    private void FixedUpdate()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= 5f)
        {
            timeElapsed = 0;
            IncreaseSpeed();
        }
    }
}
