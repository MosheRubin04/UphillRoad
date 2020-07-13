using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.Universal;
//using UnityEditor.Experimental.SceneManagement;


public class LevelManager : MonoBehaviour
{

    private static LevelManager _instance;

    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("The LevelManager is NULL.");
                _instance = new LevelManager();
            }
            return _instance;
                
        }
    }

    [SerializeField] private int currentLevel = 100;

    string mainPath;


    [Header("Managers")]
    private AudioManager audioManager;
    public BackgroundManager backgroundManager;
    public Dictionary<int, LevelInfo> levelInfoDic = new Dictionary<int, LevelInfo>();

    [Header("Level Info")]
    [SerializeField] LevelInfo lvl100Info, lvl200Info, lvl300Info, lvl101Info, lvl201Info, lvl301Info, tuturialLvlInfo;
    public LevelInfo loadedLevelInfo;

    //public Texture2D[] LevelMaps = new Texture2D[8];

    [Header("Player")]
    public GameObject player;
    public Portal playerSpawenPoint;
    public float deathTimer = 2;
    public Transform playerLoction;
    public Camera myCamera;


    [Header("Level Generator")]
    public LevelGenerator levelGenerator;
    public int[] generatedLevels = new int[3];
    public int genretedLevelsCount;

    [Header("Lights")]
    public LightManager lightManager;
    public GameObject GlobalLevelLight;

    [Header("Background")]
    public GameObject raycastBackground;
    public Transform backgroundTransform;
    public float backgroundAudioVolume;

    [Header("UI")]
    public GameObject endGamePanel;


    private void Awake()
    {
        _instance = this;         
        mainPath = GetDataPath();
        backgroundManager = this.GetComponent<BackgroundManager>();        
        audioManager = this.GetComponent<AudioManager>();
        lightManager = this.GetComponent<LightManager>();
        player = GameObject.Find("PlayerV.2");
        levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
        myCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        if (player != null)
        {            
            playerLoction = player.transform;
        }       

        GeneratInfoDictenry();
        audioManager.SetMainMenuVolume();
        audioManager.PlayBackgroundMusic();
        if (levelGenerator != null)
        {
            InisilaizeLevelOne();
        }
    }

    public float GetDeathTimer()
    {
        return deathTimer;
    }
        
    public bool ARinclude(int levelIndex, int[] intAR)
    {
        for (int i = 0; i < intAR.Length; i++)
        {
            if(intAR[i] == levelIndex)
            {
                return true;
            }            
        }
        return false;
    }
    
    public void InisilaizeLevelOne()
    {
        levelInfoDic.TryGetValue(001, out loadedLevelInfo); //lvl 1 index        

        GetComponent<SceneTransitions>().isLevelEntring = true;

        levelGenerator.map = loadedLevelInfo.GetLevelMap();
        levelGenerator.GenerateLevel(loadedLevelInfo.GetLevelKey());

        generatedLevels[genretedLevelsCount] = loadedLevelInfo.GetLevelKey();
        genretedLevelsCount++;
        loadedLevelInfo.FindMyPortals();

        backgroundManager.LoadBackgrounds(loadedLevelInfo);

        lightManager.LevelLights = loadedLevelInfo.GetLevelLights();
        lightManager.TurnAllLightsOn();

        playerSpawenPoint = GameObject.Find("LeftPortal(Clone)").GetComponent<Portal>();
        player.GetComponent<Collision>().lastPortal = playerSpawenPoint;
        player.transform.position = playerSpawenPoint.transform.position;

        if (!GameObject.Find("RaycastBackground(Clone"))
        {
            Instantiate(raycastBackground, backgroundTransform.position, backgroundTransform.rotation);
        }        
        GetComponent<SceneTransitions>().isLevelExiting = true;
        raycastBackground.GetComponent<ClickPositionManager>().myCamera = myCamera;
    }

    public void RestartThisLevel()
    {
        SetPlayerSpawenPoint(true, loadedLevelInfo.isSideRoom);
        ChooseLevel(loadedLevelInfo.GetLevelKey(), loadedLevelInfo.GetLevelKey(), true);
        FallingGround[] fallingPlatforms = FindObjectsOfType<FallingGround>();
        if(fallingPlatforms[0] != null)
        {
            foreach (FallingGround platform in fallingPlatforms)
            {
                platform.RestartFallingGround();
            }
        }
    }

    public void ChooseLevelWithButton(int i)
    {
        switch (i)
        {
            case 1:
                ChooseLevel(100, loadedLevelInfo.GetLevelKey(), true);                
                GetComponent<PauseMenu>().Resume();
                return;
            case 2:
                ChooseLevel(200, loadedLevelInfo.GetLevelKey(), true);
                GetComponent<PauseMenu>().Resume();
                return;
            case 3:
                ChooseLevel(300, loadedLevelInfo.GetLevelKey(), true);
                GetComponent<PauseMenu>().Resume();
                return;
            case 4:
                ChooseLevel(101, loadedLevelInfo.GetLevelKey(), true);
                GetComponent<PauseMenu>().Resume();
                return;
            case 5:
                ChooseLevel(201, loadedLevelInfo.GetLevelKey(), true);
                GetComponent<PauseMenu>().Resume();
                return;
            case 6:
                ChooseLevel(301, loadedLevelInfo.GetLevelKey(), true);
                GetComponent<PauseMenu>().Resume();
                return;
        }
    }



    
    public void ChooseLevel(int nextLevelIndex, int currnetLevelIndex, bool goingRight)
    {

        GetComponent<SceneTransitions>().isLevelEntring = true;

        if ( nextLevelIndex == 999)
        {
            endGamePanel.SetActive(true);
            GetComponent<PauseMenu>().Pause();
            GetComponent<PauseMenu>().pauseMenuUI.SetActive(false);

            return;
        }
        lightManager.TurnAllLightsOff();
        bool comingFromSideRoom = loadedLevelInfo.isSideRoom;

        loadedLevelInfo.TurnMiscsOff();


        levelInfoDic.TryGetValue(nextLevelIndex, out loadedLevelInfo); //loading next level to loaded level componnent

        if (loadedLevelInfo == null)
        {
            Debug.Log("No level info loaded!");
            Debug.Log("Loading Tuturial Level");
            nextLevelIndex = tuturialLvlInfo.GetLevelKey();
        }

        if (player != null)
        {
            player.GetComponent<CapsuleCollider2D>().enabled = false;
        }
        Debug.Log("is next level viable - " + levelInfoDic.TryGetValue(nextLevelIndex, out loadedLevelInfo));
        backgroundManager.LoadBackgrounds(loadedLevelInfo);        
        LevelGenerationLogic(nextLevelIndex, currnetLevelIndex, goingRight, comingFromSideRoom);
        

        if (player != null)
        {
            player.GetComponent<CapsuleCollider2D>().enabled = true;
        }
        if (!GameObject.Find("RaycastBackground(Clone"))
        {
            Instantiate(raycastBackground, backgroundTransform.position, backgroundTransform.rotation);
        }

        GetComponent<SceneTransitions>().isLevelExiting = true;

        raycastBackground.GetComponent<ClickPositionManager>().myCamera = myCamera;
        SetCurrentLevel(nextLevelIndex);
        lightManager.LevelLights = loadedLevelInfo.GetLevelLights();
        lightManager.TurnAllLightsOn();
    }
    
    public void QuitGame()
    {
         Application.Quit();
    }

    public void ChangeToScene(string sceneName)
    {
        this.GetComponent<SceneTransitions>().StartSceneTransition(sceneName);
    }

    public void SetCurrentLevel(int newLvl)
    {
        this.currentLevel = newLvl;
    }    

    public void GeneratInfoDictenry()
    {
        levelInfoDic.Add(100, lvl100Info);
        levelInfoDic.Add(200, lvl200Info);
        levelInfoDic.Add(300, lvl300Info);
        levelInfoDic.Add(101, lvl101Info);
        levelInfoDic.Add(201, lvl201Info);
        levelInfoDic.Add(301, lvl301Info);
        levelInfoDic.Add(001, tuturialLvlInfo);

    }



    private void LevelGenerationLogic(int nextLevelIndex, int currnetLevelIndex, bool _goingRight, bool _comingFromSideRoom)
    {
        

        if (ARinclude(nextLevelIndex, generatedLevels))
        {
            levelGenerator.CloaseGeneratedLevel(currnetLevelIndex);
            levelGenerator.OpenGeneratedLevel(nextLevelIndex);
            loadedLevelInfo.FindMyPortals();
            SetPlayerSpawenPoint(_goingRight, _comingFromSideRoom);
            SpawenPlayer();
            
        }
        else
        {
            levelGenerator.CloaseGeneratedLevel(currnetLevelIndex);

            levelGenerator.map = loadedLevelInfo.GetLevelMap();
            levelGenerator.GenerateLevel(nextLevelIndex);
            generatedLevels[genretedLevelsCount] = nextLevelIndex;
            loadedLevelInfo.FindMyPortals();
            SetPlayerSpawenPoint(_goingRight, _comingFromSideRoom);
            SpawenPlayer();
            genretedLevelsCount++;
        }
    }
    void SpawenPlayer()
    {
        player.GetComponent<Collision>().lastPortal = playerSpawenPoint;
        player.GetComponent<Collision>().RestartLevel(deathTimer);
    }


    public void SetPlayerSpawenPoint(bool goingRight, bool comingFromSideRoom)
    {        
        if (goingRight || loadedLevelInfo.GetLevelKey() == 001)
        {
            playerSpawenPoint = loadedLevelInfo.GetLevelPortal(0);
        }
        else if (!goingRight && loadedLevelInfo.GetLevelKey() != 001)
        {            
            playerSpawenPoint = loadedLevelInfo.GetLevelPortal(2);
        }
        if (comingFromSideRoom)
        {
            playerSpawenPoint = loadedLevelInfo.GetLevelPortal(1);
        }
    }

    string GetDataPath()
    {
        string m_Path;
        m_Path = Application.dataPath;

        //Output the Game data path to the console
        Debug.Log("dataPath : " + m_Path);


        //Get the path of the Game data folder
        return m_Path;
    }



}
