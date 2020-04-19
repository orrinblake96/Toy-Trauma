using System;
using Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        public int damagePerShot = 20;
        public float timeBetweenBullets = 0.15f;
        public float timeBetweenGrenades = 1f;
        public float range = 100f;
        public GameObject grenadePrefab;
        public float throwForce = 40f;
        public int grenadeAmount = 0;
        public GameObject grenade1;
        public GameObject grenade2;
        public GameObject grenade3;

        private float _timer;
        private float _grenadeTimer;
        private Ray _shootRay;
        private RaycastHit _shootHit;
        private int _shootableMask;
        private ParticleSystem _gunParticles;
        private LineRenderer _gunLine;
        private AudioSource _gunAudio;
        private Light _gunLight;
        private float _effectsDisplayTime = 0.2f;

        private void Awake()
        {
            _shootableMask = LayerMask.GetMask("Shootable");
            _gunParticles = GetComponent<ParticleSystem>();
            _gunLine = GetComponent<LineRenderer>();
            _gunAudio = GetComponent<AudioSource>();
            _gunLight = GetComponent<Light>();
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            _grenadeTimer += Time.deltaTime;

            if (Input.GetButton("Fire1") && _timer >= timeBetweenBullets) Shoot();

            if (_timer >= timeBetweenBullets * _effectsDisplayTime) DisableEffects();

            if (Input.GetKey(KeyCode.E) && _grenadeTimer >= timeBetweenGrenades && grenadeAmount > 0) ThrowGrenade();
            
            switch (grenadeAmount)
            {
                case 3:
                    grenade1.SetActive(true);
                    grenade2.SetActive(true);
                    grenade3.SetActive(true);
                    break;
                case 2:
                    grenade1.SetActive(true);
                    grenade2.SetActive(true);
                    grenade3.SetActive(false);
                    break;
                case 1:
                    grenade1.SetActive(true);
                    grenade2.SetActive(false);
                    grenade3.SetActive(false);
                    break;
                default:
                    grenade1.SetActive(false);
                    grenade2.SetActive(false);
                    grenade3.SetActive(false);
                    break;
            }
        }

        private void Shoot()
        {
            _timer = 0f;
            
            _gunAudio.Play();

            _gunLight.enabled = true;
            
            _gunParticles.Stop();
            _gunParticles.Play();

            _gunLine.enabled = true;
            _gunLine.SetPosition(0, transform.position);

            _shootRay.origin = transform.position;
            _shootRay.direction = transform.forward;

            if (Physics.Raycast(_shootRay, out _shootHit, range, _shootableMask))
            {
                EnemyHealth enemyHealth = _shootHit.collider.GetComponent<EnemyHealth>();
                if(enemyHealth != null) enemyHealth.TakeDamage(damagePerShot, _shootHit.point);
                _gunLine.SetPosition(1, _shootHit.point);
                
            }
            else
            {
                _gunLine.SetPosition(1, _shootRay.origin + _shootRay.direction * range);
            }


        }

        private void ThrowGrenade()
        {
            _grenadeTimer = 0;
            grenadeAmount -= 1;
            
            GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * throwForce);
        }
        
        public void DisableEffects()
        {
            _gunLine.enabled = false;
            _gunLight.enabled = false;
        }
    }
}
