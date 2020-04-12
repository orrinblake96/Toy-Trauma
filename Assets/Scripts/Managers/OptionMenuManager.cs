using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Managers
{
    public class OptionMenuManager : MonoBehaviour
    {

        public AudioMixer bgMusicMixer;
        public AudioMixer sfxMixer;

        public void SetMusicVolume(float volume)
        {
            bgMusicMixer.SetFloat("musicVol", volume);
        }
        
        public void SetSfxVolume(float volume)
        {
            sfxMixer.SetFloat("sfxVol", volume);
        }
    }
}
