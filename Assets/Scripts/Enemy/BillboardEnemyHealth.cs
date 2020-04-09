using System;
using UnityEngine;

namespace Enemy
{
    public class BillboardEnemyHealth : MonoBehaviour
    {
        public Transform cameraTransform;
        private Camera _mainCam;

        private void Start()
        {
            _mainCam = FindObjectOfType<Camera>();
            cameraTransform = _mainCam.transform;
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            transform.LookAt(transform.position + cameraTransform.forward);
        }
    }
}
