using Enemy;
using UnityEngine;

namespace PowerUps
{
    public class GrenadePowerUp : MonoBehaviour
    {
        public float delay = 2f;
        public GameObject explosionEffect;
        public float blastRadius = 5.0f;
        public float blastForce = 700;
        private float _countdown;
        private bool _hasExploded;
        // Start is called before the first frame update
        void Start()
        {
            _countdown = delay;
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
                    enemyHealth.TakeDamage(40, nearbyObject.transform.position);
                }
            }
            
            FindObjectOfType<AudioManager>().Play("Grenade");
            
            Destroy(gameObject);
        }
    }
}
