using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerKillstreak : MonoBehaviour
    {
        public int currentKillStreak = 1;
        private PlayerShooting _playerShooting;

        private void Start()
        {
            _playerShooting = GameObject.Find("GunBarrelEnd").GetComponent<PlayerShooting>();
        }

        public void KillstreakCounter()
        {
            currentKillStreak += 1;
            if (currentKillStreak % 15 == 0 && !_playerShooting.setForceField) _playerShooting.setForceField = true;
            Debug.Log("Kill" + currentKillStreak);
        }

    }
}
