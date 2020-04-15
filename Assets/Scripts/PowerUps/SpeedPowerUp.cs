using Player;
using UnityEngine;

namespace PowerUps
{
    public class SpeedPowerUp : MonoBehaviour
    {
        public GameObject pickupEffect;

        private GameObject _player;
        private PlayerMovement _playerMovement;
        private MeshRenderer _meshRenderer;
        private BoxCollider _boxCollider;
        
        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _playerMovement = _player.GetComponent<PlayerMovement>();
            _boxCollider = GetComponent<BoxCollider>();
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && _playerMovement.speed <= 6)
            {
                _playerMovement.StartSpeedPowerupTimer(gameObject);
            }
        }
    }
}
