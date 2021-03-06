﻿using System;
using Enemy;
using UnityEngine;
using EZCameraShake;

namespace PowerUps
{
    public class GrenadePowerUp : MonoBehaviour
    {
        public float delay = 2f;
        public GameObject explosionEffect;
        public float blastRadius = 6.0f;
        public float blastForce = 700;
        private float _countdown;
        private bool _hasExploded;
        private CameraFollow _camera;
        private GameObject _forcefield;

        // Start is called before the first frame update
        void Start()
        {
            _countdown = delay;
            _forcefield = GameObject.Find("HexgonSphere/HexgonSphereInner");
            if (_forcefield != null)
            {
                Physics.IgnoreCollision(_forcefield.GetComponent<Collider>(), GetComponent<Collider>());   
            }
        }

        // Update is called once per frame
        void Update()
        {
            _countdown -= Time.deltaTime;
            if (_countdown <= 0 && !_hasExploded)
            {
                Explode();
                _hasExploded = true;
            }
        }

        private void Explode()
        {
            var gameObjectTransform = transform;
            Instantiate(explosionEffect, gameObjectTransform.position, gameObjectTransform.rotation);

            Collider[] blastColliders = Physics.OverlapSphere(transform.position, blastRadius);

            foreach (Collider nearbyObject in blastColliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(blastForce, transform.position, blastRadius);
                }

                EnemyHealth enemyHealth = nearbyObject.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(60, nearbyObject.transform.position);
                }
            }

            CameraShaker.Instance.ShakeOnce(2f, 1.5f, .1f, 1f);
            FindObjectOfType<AudioManager>().Play("Grenade");
            
            Destroy(gameObject);
        }
    }
}
