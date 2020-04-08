using System;
using UnityEngine;

namespace Enemy
{
    public class BillboardEnemyHealth : MonoBehaviour
    {
        public Transform camera;
        private Camera mainCam;

        private void Start()
        {
            mainCam = FindObjectOfType<Camera>();
            camera = mainCam.transform;
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            transform.LookAt(transform.position + camera.forward);
        }
    }
}
