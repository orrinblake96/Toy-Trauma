using System.Collections;
using Player;
using UnityEngine;

namespace Managers
{
    public class EnemyManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;
        public GameObject enemy;
        public float spawnTime = 3f;
        public Transform[] spawnPoints;

        private void Start() 
        {
            InvokeRepeating(nameof(Spawn), spawnTime, spawnTime);
        }

        private void Spawn()
        {
            if (playerHealth.currentHealth <= 0f) return;

            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
    }
}
