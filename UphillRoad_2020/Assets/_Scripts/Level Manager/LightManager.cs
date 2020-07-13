using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.Universal;

public class LightManager : MonoBehaviour
{

    public float presetGlobalLightIntesity;

    public GameObject GlobalLevelLight;
    public GameObject[] LevelLights;
    public GameObject playerLight;
    // Start is called before the first frame update
    public void Start()
    {
        GlobalLevelLight = GameObject.Find("GlobalLevelLight");
        //SetLevelGlobalLight(globalLightIntesity);
    }
    


    public void SetLevelGlobalLight(float inputIntensity)
    {
        GlobalLevelLight.GetComponent<Light2D>().intensity = inputIntensity;
    }

    public void SetDeffualtLevelGlobalLight()
    {
        GlobalLevelLight.GetComponent<Light2D>().intensity = presetGlobalLightIntesity;
    }

    public void TurnAllLightsOff()
    {
        for (int i = 0; i < LevelLights.Length; i++)
        {
            LevelLights[i].SetActive(false);
        }
    }

    public void TurnAllLightsOn()
    {
        for (int i = 0; i < LevelLights.Length; i++)
        {
            LevelLights[i].SetActive(true);
        }
    }

    public void TurnALightOn(int lightID)
    {
        LevelLights[lightID].SetActive(true);
    }

    public void TurnALightOff(int lightID)
    {
        LevelLights[lightID].SetActive(false);
    }
}
