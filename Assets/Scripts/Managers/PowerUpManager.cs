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
        private MeshRenderer _meshRenderer;
        private SphereCollider _sphereCollider;
        
        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _sphereCollider = GetComponent<SphereCollider>();
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log(_playerHealth.currentHealth);
                if (_playerHealth.currentHealth < 100)
                {
//                    StartCoroutine();
                    HealthPickup();
                }
            }
        }

        private void HealthPickup()
        {
            //Effects
            Instantiate(pickupEffect, transform.position, transform.rotation);
            
            //Player Ability
            _playerHealth.GainHealth();
            
            //Disable so power-up is hidden
//            _sphereCollider.enabled = false;
//            _meshRenderer.enabled = false;

            //Wait for set time, then return to normal
//            yield return new WaitForSeconds(2);
//            playerHealth.TakeDamage(10);
            
            //Destroy Powerup
            Destroy(gameObject);
        }
    }
}
