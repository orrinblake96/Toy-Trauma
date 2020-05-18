﻿using System;
using System.Collections;
using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerKillstreak : MonoBehaviour
    {
        public int currentForcefieldKillStreak = 1;
        public int currentSlowtimeKillStreak = 1;
        private PlayerShooting _playerShooting;
        private SlowTimeManager _slowTimeManager;

        private void Start()
        {
            _playerShooting = GameObject.Find("GunBarrelEnd").GetComponent<PlayerShooting>();
            _slowTimeManager = GameObject.Find("SlowtimeManager").GetComponent<SlowTimeManager>();
        }

        public void KillstreakCounter()
        {
            currentForcefieldKillStreak += 1;
            currentSlowtimeKillStreak += 1;
            if (currentForcefieldKillStreak == 41 && !_playerShooting.setForceField)
            {
                _playerShooting.setForceField = true;
                currentForcefieldKillStreak = 1;
            }
            
            if (currentSlowtimeKillStreak == 46 && _slowTimeManager.slowtime == false)
            {
                _slowTimeManager.slowtime = true;
                _slowTimeManager.ShowHourglassUi();
                currentSlowtimeKillStreak = 1;
            }
        }

    }
}
