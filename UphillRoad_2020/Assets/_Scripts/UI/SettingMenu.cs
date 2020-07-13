using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingMenu : MonoBehaviour
{
    Resolution[] resolutions;

    //public Dropdown resolutionDropDowen;
    public TMPro.TMP_Dropdown resolutionDropDowen;
    public void Start()
    {
        SetVolume(0.2f);
        resolutions = Screen.resolutions;
        resolutionDropDowen.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropDowen.AddOptions(options);
        resolutionDropDowen.value = currentResolutionIndex;
        resolutionDropDowen.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float newVolume)
    {
        //Debug.Log(newVolume);
        //this.GetComponent<MusicManager>().globalVolumeModifaier = newVolume;
        this.GetComponent<AudioManager>().UpdateVolume(newVolume);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen( bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

}
