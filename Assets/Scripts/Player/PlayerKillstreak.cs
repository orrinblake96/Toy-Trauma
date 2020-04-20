using System;
using System.Collections;
using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerKillstreak : MonoBehaviour
    {
        public int currentKillStreak = 1;
        private PlayerShooting _playerShooting;
        private SlowTimeManager _slowTimeManager;

        private void Start()
        {
            _playerShooting = GameObject.Find("GunBarrelEnd").GetComponent<PlayerShooting>();
            _slowTimeManager = GameObject.Find("AudioManager").GetComponent<SlowTimeManager>();
        }

        public void KillstreakCounter()
        {
            currentKillStreak += 1;
            if (currentKillStreak % 15 == 0 && !_playerShooting.setForceField) _playerShooting.setForceField = true;
            if (currentKillStreak % 20 == 0 && !_slowTimeManager.slowtime) _slowTimeManager.slowtime = true;
            Debug.Log("Kill" + currentKillStreak);
        }

    }
}
