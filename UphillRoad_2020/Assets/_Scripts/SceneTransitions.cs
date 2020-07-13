using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{

    Material changingSceneMaterial, deathSceneMatirial;
    
    float fade = 1f;

    public GameObject player;
    public bool isDeathEntring, isDeathExiting, isLevelEntring, isLevelExiting;
    
    public float transtionTimer = 0.01f;
    [Range(0, 5)] public float exitSpeedModfair;
    [Range(0,5)] public float enterSpeedModfair;
    public GameObject ChangingScenePanel, deathScenePanel;

    


    IEnumerator SceneTransition(string sceneName)
    {
        yield return new WaitForSeconds(exitSpeedModfair);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator LevelTrasition()
    {        
        yield return new WaitForSeconds(exitSpeedModfair);
        //player.transform.position = GetComponentInParent<LevelManager>().playerSpawenPoint.transform.position;        
    }

    public void StartSceneTransition(string sceneName)
    {        
        StartCoroutine("SceneTransition", sceneName);        
    }

    public void StartLevelTimer()
    {
        StartCoroutine("LevelTrasition");
    }


    public void ChangigSceneEndEffect()
    {
        fade -= Time.deltaTime * exitSpeedModfair;
        if (fade <= 0f) 
        {
            fade = 0f;
            isLevelExiting = false;
        }
        changingSceneMaterial.SetFloat("_Fade", fade);        
    }

    public void ChangigSceneStartEffect()
    {
        fade += Time.deltaTime * enterSpeedModfair; //intansily much faster so player wont see the characther moveing
        if (fade >= 1f)
        {
            fade = 1f;
            isLevelEntring = false;
            isLevelExiting = true;
        }
        changingSceneMaterial.SetFloat("_Fade", fade);              
    }

    public void DeathSceneStartEffect()
    {
        fade += Time.deltaTime * enterSpeedModfair; //intansily much faster so player wont see the characther moveing
        if (fade >= 1f)
        {
            fade = 1f;
            isDeathEntring = false;
            isDeathExiting = true;
        }
        deathSceneMatirial.SetFloat("_Fade", fade);
    }


    public void DeathSceneEndEffect()
    {
        fade -= Time.deltaTime * exitSpeedModfair;
        if (fade <= 0f)
        {
            fade = 0f;
            isDeathExiting = false;
        }
        deathSceneMatirial.SetFloat("_Fade", fade);
    }

    public void Start()
    {
        player = GameObject.Find("PlayerV.2");
        changingSceneMaterial = ChangingScenePanel.GetComponent<SpriteRenderer>().material;
        deathSceneMatirial = deathScenePanel.GetComponent<SpriteRenderer>().material;
        isDeathEntring = false;
        isDeathExiting = false;
        isLevelExiting = false;
        isLevelEntring = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (isDeathEntring && !isDeathExiting)
        {
            DeathSceneStartEffect();
            //ChangigSceneStartEffect();
        }
        else if (isDeathExiting && !isDeathEntring)
        {
            DeathSceneEndEffect();
            //ChangigSceneEndEffect();
        }
        else if(isDeathExiting && isDeathExiting)
        {
            isDeathEntring = true;
            isDeathExiting = false;
        }
        if (isLevelEntring && !isLevelExiting)
        {
            //DeathSceneStartEffect();
            ChangigSceneStartEffect();
        }
        else if (isLevelExiting && !isLevelEntring)
        {
            //DeathSceneEndEffect();
            ChangigSceneEndEffect();
        }
        else if (isLevelEntring && isLevelExiting)
        {
            isLevelEntring = true;
            isLevelExiting = false;
        }
    }
}
