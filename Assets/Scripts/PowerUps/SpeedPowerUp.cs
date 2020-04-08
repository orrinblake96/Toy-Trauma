using System.Collections;
using Player;
using UnityEngine;

namespace PowerUps
{
    public class SpeedPowerUp : MonoBehaviour
    {
    
        private GameObject _player;
        private PlayerMovement _playerMovement;
        public GameObject pickupEffect;
        private MeshRenderer _meshRenderer;
        private SphereCollider _sphereCollider;
        
        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _playerMovement = _player.GetComponent<PlayerMovement>();
            _sphereCollider = GetComponent<SphereCollider>();
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && _playerMovement.speed <= 6)
            {
            
                StartCoroutine(SpeedPickup());
                
            }
        }

        private IEnumerator SpeedPickup()
        {
            //Effects
            Instantiate(pickupEffect, transform.position , transform.rotation);
            
            //Player Ability
            _playerMovement.speed += 10.0f;
            
            //Disable so power-up is hidden
            _sphereCollider.enabled = false;
            _meshRenderer.enabled = false;

            //Wait for set time, then return to normal
            yield return new WaitForSeconds(10);
            _playerMovement.speed -= 10.0f;
            
            //Destroy Powerup
            Destroy(gameObject);
        }
    }
}
