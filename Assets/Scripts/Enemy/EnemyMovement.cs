using System;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        private Transform _player;
        private NavMeshAgent _enemyNav;
        private PlayerHealth _playerHealth;
        private EnemyHealth _enemyHealth;

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _enemyNav = GetComponent<NavMeshAgent>();
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _enemyHealth = GetComponent<EnemyHealth>();
        }

        private void Update()
        {
            if (_enemyHealth.currentHealth > 0 && _playerHealth.currentHealth > 0)
            {
                _enemyNav.SetDestination(_player.position); 
            }
            else
            {
                _enemyNav.enabled = false;
            }
            
        }
    }
}
