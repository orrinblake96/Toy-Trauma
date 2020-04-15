using System;
using System.Collections;
using Player;
using UnityEngine;

namespace Managers
{
    public class PowerUpManager : MonoBehaviour
    {
        public GameObject pickupEffect;
        
        private GameObject _player;
        private PlayerHealth _playerHealth;

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _playerHealth = _player.GetComponent<PlayerHealth>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (_playerHealth.currentHealth < 100)
                {
                    HealthPickup();
                }
            }
        }

        private void HealthPickup()
        {
            //Effects
            Instantiate(pickupEffect, transform.position , transform.rotation);
            
            //Sound effect
            FindObjectOfType<AudioManager>().Play("Powerup");
            
            //Player Ability
            _playerHealth.GainHealth();
            
            //Destroy Powerup
            Destroy(gameObject);
        }
    }
}
