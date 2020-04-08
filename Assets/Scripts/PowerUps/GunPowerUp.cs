using System;
using UnityEngine;

namespace PowerUps
{
    public class GunPowerUp : MonoBehaviour
    {
        public GameObject baseGun;

        public GameObject heavyGun;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Transform basegunTransform = baseGun.transform;
                
                Destroy(baseGun);
                

                GameObject clone = Instantiate(heavyGun, basegunTransform.position, Quaternion.identity);
                clone.transform.SetParent(basegunTransform);
                
            }
        }
    }
}
