using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 6.0f;
        public GameObject speedPickupEffect;
        public PlayerShooting playerShooting;

        private Vector3 _movement;
        private Animator _anim;
        private Rigidbody _playerRigidbody;
        private int _floorMask;
        private float _camRayLength = 100.0f;
        private GameObject _speedEffect;

        private void Awake()
        {
            _floorMask = LayerMask.GetMask("Floor");
            _anim = GetComponent<Animator>();
            _playerRigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Move(h, v);
            Turning();
            Animating(h, v);
        }

        private void Move(float h, float v)
        {
            _movement.Set(h, 0.0f, v);
            _movement = Time.deltaTime * speed * _movement.normalized;

            _playerRigidbody.MovePosition(transform.position + _movement);
        }

        private void Turning()
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit floorHit;
            if (Physics.Raycast(camRay, out floorHit, _camRayLength, _floorMask))
            {
                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0.0f;

                Quaternion newRot = Quaternion.LookRotation(playerToMouse);
                _playerRigidbody.MoveRotation(newRot);
            }
        }

        private void Animating(float h, float v)
        {
            bool _walking = h != 0f || v != 0f;

            _anim.SetBool("IsWalking", _walking);
        }

        public void StartSpeedPowerupTimer(GameObject powerupBox)
        {
            //Effects
            _speedEffect = Instantiate(speedPickupEffect, transform.position, transform.rotation);
            StartCoroutine(SpeedPowerup(powerupBox));
        }

        private IEnumerator SpeedPowerup(GameObject powerupBox)
        {

            FindObjectOfType<AudioManager>().Play("Powerup");

            //Player Ability
            speed += 8.0f;
            playerShooting.IncreaseDamageAmount();
            
            //Destroy Powerup
            Destroy(powerupBox);
            
            Destroy(_speedEffect, 2f);
            
            //Wait for set time, then return to normal
            yield return new WaitForSeconds(10);
            speed -= 8.0f;
            playerShooting.DecreaseDamageAmount();
        }
    }
}
