using System;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public int startingHealth = 100;
        public int currentHealth;
        public Slider healthSlider;
        public Image damageImage;
        public AudioClip deathClip;
        public float flashSpeed = 5f;
        public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

        private Animator _anim;
        private AudioSource _playerAudio;
        private PlayerMovement _playerMovement;
        private bool _isDead;
        private bool _damaged;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _playerAudio = GetComponent<AudioSource>();
            _playerMovement = GetComponent<PlayerMovement>();
            currentHealth = startingHealth;
        }

        private void Update()
        {
            damageImage.color = _damaged ? flashColor : Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);

            _damaged = false;
        }

        public void TakeDamage(int amount)
        {
            _damaged = true;

            currentHealth -= amount;

            healthSlider.value = currentHealth;
            
            _playerAudio.Play();

            if (currentHealth <= 0 && !_isDead) Death();
        }

        private void Death()
        {
            _isDead = true;
            
            _anim.SetTrigger("Die");

            _playerAudio.clip = deathClip;
            _playerAudio.Play();

            _playerMovement.enabled = false;
        }
    }
}
