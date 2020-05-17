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
        public GameObject grenadePowerup;
//        public EnemyHealthBar enemyHealthBar;
        public GameObject healthBar;

        private EnemyHealthBar _enemyHealthBar;
        private Animator _anim;
        private AudioSource _enemyAudio;
        private ParticleSystem _hitParticles;
        private CapsuleCollider _capsuleCollider;
        private bool _isDead;
        private bool _isSinking;
        private static readonly int Dead = Animator.StringToHash("Dead");
        private GameObject _player;
        private PlayerHealth _playerHealth;
        private PlayerMovement _playerMovement;
        private PlayerKillstreak _killstreak;
        private ScoreManager _scoreManager;
        private bool _isHealthDropGameObjectNull;
        private bool _isSpeedDropGameObjectNull;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _enemyAudio = GetComponent<AudioSource>();
            _hitParticles = GetComponentInChildren<ParticleSystem>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _player = GameObject.FindGameObjectWithTag("Player");
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _playerMovement = _player.GetComponent<PlayerMovement>();
            _enemyHealthBar = healthBar.GetComponent<EnemyHealthBar>();
            _killstreak = GameObject.Find("KillstreakCounter").GetComponent<PlayerKillstreak>();
            _scoreManager = GameObject.Find("UI/HUDCanvas/ScoreText").GetComponent<ScoreManager>();
            _isHealthDropGameObjectNull = GameObject.Find("healthDrop(Clone)") == null;
            _isSpeedDropGameObjectNull = GameObject.Find("speedDrop(Clone)") == null;
            
            
            _enemyHealthBar.SetMaxHealth(startingHealth);
            currentHealth = startingHealth;
        }

        private void Update()
        {
            if(_isSinking) transform.Translate(Time.deltaTime * sinkSpeed * -Vector3.up);
        }

        public void TakeDamage(int amount, Vector3 hitPoint)
        {
            if (_isDead) return;

            switch (gameObject.name)
            {
                case "Zombunny(Clone)":
                    FindObjectOfType<AudioManager>().Play("zombunnyHurt");
                    break;
                case "ZomBear(Clone)":
                    FindObjectOfType<AudioManager>().Play("zombearHurt");
                    break;
                case "Hellephant(Clone)":
                    FindObjectOfType<AudioManager>().Play("hellephantHurt");
                    break;
            }
            

            currentHealth -= amount;
            _enemyHealthBar.SetHealth(currentHealth);

            _hitParticles.transform.position = hitPoint;
            _hitParticles.Play();

            if (currentHealth <= 0) Death();
        }

         private void Death()
        {
            _isDead = true;

            _killstreak.KillstreakCounter();
 
            _capsuleCollider.isTrigger = true;

            _anim.SetTrigger(Dead);
            StartCoroutine(_scoreManager.PlayScoreKillAnim());
            
            
            //power-up drop chance
            if (gameObject.name=="Zombunny(Clone)" && (Random.Range(0, 10) > 5) && (_playerHealth.currentHealth < 50) && _isHealthDropGameObjectNull)
            {
                FindObjectOfType<AudioManager>().Play("zombunnyDeath");
                Instantiate(healthPowerUpPrefab, transform.position + Vector3.up, transform.rotation);
            } 
            
            if (gameObject.name == "ZomBear(Clone)" && (Random.Range(0, 10) > 5) && (_playerMovement.speed <= 6) && _isSpeedDropGameObjectNull)
            {
                FindObjectOfType<AudioManager>().Play("zombearDeath");
                Instantiate(speedPowerUpPrefab, transform.position + Vector3.up, transform.rotation);
            }
            
            if (gameObject.name == "Hellephant(Clone)" && (Random.Range(0, 10) > 5))
            {
                FindObjectOfType<AudioManager>().Play("hellephantDeath");
                Instantiate(grenadePowerup, transform.position + Vector3.up, transform.rotation);
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
