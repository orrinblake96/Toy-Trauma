﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using EZCameraShake;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public int startingHealth = 100;
        public int currentHealth;
        public PlayerHealthBar playerHealthBar;
        public Image damageImage;
        public AudioClip deathClip;
        public float flashSpeed = 3f;
        public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

        private Animator _anim;
        private AudioSource _playerAudio;
        private PlayerMovement _playerMovement;
        private PlayerShooting _playerShooting;
        private bool _isDead;
        private bool _damaged;
        private static readonly int Die = Animator.StringToHash("Die");
        private ParticleSystem _hitParticles;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _playerAudio = GetComponent<AudioSource>();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerShooting = GetComponentInChildren<PlayerShooting>();
            _hitParticles = GameObject.Find("PlayerHitParticles").GetComponent<ParticleSystem>();
            currentHealth = startingHealth;
            playerHealthBar.SetMaxHealth(currentHealth);
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
            playerHealthBar.SetHealth(currentHealth);
            

            CameraShaker.Instance.ShakeOnce(1f, 3f, .1f, .4f);
            
            _hitParticles.Play();
            
            FindObjectOfType<AudioManager>().Play("PlayerHurt");

            if (currentHealth <= 0 && !_isDead) Death();
        }
        
        public void GainHealth()
        {

            currentHealth += 10;
            playerHealthBar.SetHealth(currentHealth);
        }

        private void Death()
        {
            _isDead = true;

            _playerShooting.DisableEffects();

            _anim.SetTrigger(Die);
            
            FindObjectOfType<AudioManager>().Play("PlayerDeath");

            _playerMovement.enabled = false;
        }
    }
}
