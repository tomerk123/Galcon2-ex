using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float spawnInterval = 2.5f;

    [SerializeField]
    private float timeSinceLastSpawn = 0f;

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            Attack();
            timeSinceLastSpawn = 0f;
        }
    }


    void Attack()
    {
      
     
    }
}
