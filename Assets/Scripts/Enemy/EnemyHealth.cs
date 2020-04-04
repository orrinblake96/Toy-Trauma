using System.Collections;
using Player;
using Managers;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        public int startingHealth = 100;
        public int currentHealth;
        public float sinkSpeed = 2.5f;
        public int scoreValue = 10;
        public AudioClip deathClip;
        public GameObject healthPowerUpPrefab;
        public GameObject speedPowerUpPrefab;

        private Animator _anim;
        private AudioSource _enemyAudio;
        private ParticleSystem _hitParticles;
        private CapsuleCollider _capsuleCollider;
        private bool _isDead;
        private bool _isSinking;
        private static readonly int Dead = Animator.StringToHash("Dead");
        private GameObject _player;
        private PlayerHealth _playerHealth;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _enemyAudio = GetComponent<AudioSource>();
            _hitParticles = GetComponentInChildren<ParticleSystem>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _player = GameObject.FindGameObjectWithTag("Player");
            _playerHealth = _player.GetComponent<PlayerHealth>();

            currentHealth = startingHealth;
        }

        private void Update()
        {
            if(_isSinking) transform.Translate(Time.deltaTime * sinkSpeed * -Vector3.up);
        }

        public void TakeDamage(int amount, Vector3 hitPoint)
        {
            if (_isDead) return;
            
            _enemyAudio.Play();

            currentHealth -= amount;

            _hitParticles.transform.position = hitPoint;
            _hitParticles.Play();

            if (currentHealth <= 0) Death();
        }

         private void Death()
        {
            _isDead = true;

            _capsuleCollider.isTrigger = true;

            _anim.SetTrigger(Dead);

            _enemyAudio.clip = deathClip;
            _enemyAudio.Play();
            
            //power-up drop chance
            if ( (Random.Range(0, 10) > 0) && (_playerHealth.currentHealth < 40) )
            {
                Instantiate(speedPowerUpPrefab, transform.position + Vector3.up, transform.rotation);
            }
        }
        
        public void StartSinking()
        {
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            _isSinking = true;
            ScoreManager.score += scoreValue;
            Destroy(gameObject, 2f);
        }
    }
}
