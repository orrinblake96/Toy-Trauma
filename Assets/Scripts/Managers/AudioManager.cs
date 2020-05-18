using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System;
using Managers;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    private string _currentSceneName;
    
    // Start is called before the first frame update
    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        
        foreach (Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();
           s.source.clip = s.clip;
           s.source.volume = s.volume;
           s.source.pitch = s.pitch;
           s.source.loop = s.loop;
           s.source.outputAudioMixerGroup = s.mixerGroup;
        }
    }

    private void Update()
    {
        if (_currentSceneName != SceneManager.GetActiveScene().name)
        {
            _currentSceneName = SceneManager.GetActiveScene().name;
            if (_currentSceneName == "MainLevel")
            {
                Play("Theme");
                Play("SurviveTheNight");
            }
            else
            {
                Stop("Theme");
            }
            
            if (_currentSceneName == "StartMenu")
            {
                Play("StartMenuBG");
            }
            else
            {
                Stop("StartMenuBG");
            }
            
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!" );
            return;
        }
        s.source.Play();
    }
    
    public void Stop (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!" );
            return;
        }
        s.source.Stop();
    }
}
