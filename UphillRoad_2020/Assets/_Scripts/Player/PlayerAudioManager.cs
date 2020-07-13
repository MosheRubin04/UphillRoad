using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{

    public AudioSource jump, walk, fieldSoutDowen;


    public void SetPlayerAudio(float playerVolume)
    {
        jump.volume = playerVolume;
        walk.volume = playerVolume;
        //death.volume = playerVolume; // Moved to MusicManager
        fieldSoutDowen.volume = playerVolume;
    }

    public void PlayJumpAudio()
    {
        jump.Play();
    }

    public void PlayWalkAudio()
    {
        walk.Play();
    }

    public void MuteWalkAudio(bool changeTo)
    {
        walk.mute = changeTo;
    }


    public void PlayFieldSoutDowenAudio()
    {
        fieldSoutDowen.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
