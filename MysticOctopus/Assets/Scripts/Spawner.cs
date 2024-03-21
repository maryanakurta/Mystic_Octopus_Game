using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] pipes;
    public float maxYpos;
    Vector3 spawnPos;

    public float spawnBreak;
    public bool spawnPipes;

    public static Spawner instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        spawnPos = transform.position;

        StartSpawning();
    }

    public void StartSpawning()
    {
        spawnPipes = true;
        StartCoroutine("SpawnPipe");
    }

    IEnumerator SpawnPipe()
    {
        yield return new WaitForSeconds(0.5f);

        while(spawnPipes)
        {
            int randomPipe = Random.Range(0, pipes.Length);

            spawnPos.y = Random.Range(-maxYpos, maxYpos);

            Instantiate(pipes[randomPipe], spawnPos, Quaternion.identity);

            yield return new WaitForSeconds(spawnBreak);
        }
    }
}
