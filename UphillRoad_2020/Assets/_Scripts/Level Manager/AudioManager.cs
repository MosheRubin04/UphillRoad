using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public float pauseMenuVol, mainMenuVol, levelVol;
    public AudioSource backgroundMusic;
    public AudioSource death;

    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateVolume(float newVolume)
    {
        audioMixer.SetFloat("Volume", newVolume);
        //SetMainMenuVolume();
        //SetPauseMenuVolume();
        //SetlevelVolume();
    }

    public void PlayBackgroundMusic()
    {
        backgroundMusic.Play();
    }

    public void SetMainMenuVolume()
    {
        backgroundMusic.volume = mainMenuVol;
    }

    public void SetPauseMenuVolume()
    {
        backgroundMusic.volume = pauseMenuVol;
    }

    public void SetlevelVolume()
    {
        backgroundMusic.volume = levelVol;
        LevelManager.Instance.player.GetComponent<PlayerAudioManager>().SetPlayerAudio(levelVol);       
    }

    public void ChangeBackgroundMusicVolume(float newVolume)
    {
        backgroundMusic.volume = newVolume;
    }

    public void PlayDeathAudio()
    {
        death.Play();
    }
}
