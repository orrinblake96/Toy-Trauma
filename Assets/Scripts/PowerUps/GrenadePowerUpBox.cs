using UnityEngine;
using System.Collections;
using Player;

namespace PowerUps
{
    public class GrenadePowerUpBox : MonoBehaviour
    {
        public GameObject pickupEffect;

        private GameObject _player;
        private PlayerShooting _playerShooting;

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _playerShooting = _player.GetComponentInChildren<PlayerShooting>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player") && _playerShooting.grenadeAmount < 3)
            {
                _playerShooting.grenadeAmount += 1;
                Destroy(gameObject);
            }
        }
    }
}
