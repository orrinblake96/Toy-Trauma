using System;
using System.Collections;
using Enemy;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        public float timeBetweenBullets = 0.15f;
        public float timeBetweenGrenades = 1f;
        public float range = 100f;
        public GameObject grenadePrefab;
        public float throwForce = 40f;
        public int grenadeAmount = 0;
        public GameObject grenade1;
        public GameObject grenade2;
        public GameObject grenade3;
        public GameObject forceField;
        public bool setForceField = false;

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

            if (setForceField) StartCoroutine(TurnOnFrocefield());

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

        private IEnumerator TurnOnFrocefield()
        {
            setForceField = false;
            forceField.SetActive(true);
            yield return new WaitForSeconds(10f);
            forceField.SetActive(false);
        }

        private void Shoot()
        {
            _timer = 0f;
            
            FindObjectOfType<AudioManager>().Play("PlayerShoot");
            
            _gunLight.enabled = true;
            
            _gunParticles.Stop();
            _gunParticles.Play();

            _gunLine.enabled = true;
            _gunLine.SetPosition(0, transform.position);

            _shootRay.origin = transform.position;
            _shootRay.direction = transform.forward;
            
            CameraShaker.Instance.ShakeOnce(.7f, .9f, .1f, 1f);

            if (Physics.Raycast(_shootRay, out _shootHit, range, _shootableMask))
            {
                EnemyHealth enemyHealth = _shootHit.collider.GetComponent<EnemyHealth>();
                if(enemyHealth != null) enemyHealth.TakeDamage(Random.Range(15, 20), _shootHit.point);
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
