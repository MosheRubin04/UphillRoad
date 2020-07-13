using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.Universal;
//using UnityEngine.Windows;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#if UNITY_EDITOR
using UnityEditor.Experimental.SceneManagement;
#endif

public class LevelInfo : MonoBehaviour
{

    

    [SerializeField] int levelKey;
    
    [SerializeField] Texture2D levelMap;

    [SerializeField] Texture2D[] backgrounds;

    public int backgroundCount;

    [SerializeField] int midelNeborIndex, leftNeborIndex, rightNeborIndex;

    //TODO => Light info
    public float presetGlobalLightIntesity;

    [SerializeField] GameObject[] levelLights;

    [SerializeField] GameObject levelMiscHolder;

    [SerializeField]  AudioClip levelBackMusic;

    [SerializeField] Portal[] levelPortals;

    public Dictionary<string, int> NeborsIndex = new Dictionary<string, int>();

    public bool isSideRoom;
    public bool isFirstRoom;

    [SerializeField] Sprite[] groundTileSet;
    [SerializeField] Sprite[] hazzardTileSet;

    [SerializeField] Sprite keySprite;
    [SerializeField] Sprite collectableSprite;


    public Sprite closedDoorTop, closedDoorBottom, openDoorTop, openDoorBottom;




    private void Start()
    {

        EnsilayzeNeborDictenry(midelNeborIndex, leftNeborIndex, rightNeborIndex);     
    }

    public int GetLevelKey()
    {
        return levelKey;
    }

    public Sprite GetCollectableSprite()
    {
        return collectableSprite;
    }


    public Sprite GetKeySprite()
    {
        return keySprite;
    }  

    public int NormelizadTileSetIndicator(int i)
    {
        switch (i)
        {
            case 32:
                i = 0;
                break;
            case 36:
                i = 0;
                break;
            case 40:
                i = 0;
                break;
            case 31:
                i = 2;
                break;
            case 35:
                i = 2;
                break;
            case 39:
                i = 2;
                break;
            case 30:
                i = 5;
                break;
            case 34:
                i = 5;
                break;
            case 38:
                i = 5;
                break;
            case 29:
                i = 7;
                break;
            case 33:
                i = 7;
                break;
            case 37:
                i = 7;
                break;
            default:
                break;
        }
        return i;
    }

    public int NormelizadHazzardSetIndicator(int i)
    {
        switch (i)
        {
            case 32:
                i = 1;
                break;
            case 36:
                i = 1;
                break;
            case 40:
                i = 1;
                break;
            case 41:
                i = 1;
                break;
            case 31:
                i = 1;
                break;
            case 35:
                i = 1;
                break;
            case 39:
                i = 1;
                break;
            case 30:
                i = 6;
                break;
            case 34:
                i = 6;
                break;
            case 38:
                i = 6;
                break;
            case 29:
                i = 6;
                break;
            case 33:
                i = 6;
                break;
            case 37:
                i = 6;
                break;
            default:
                break;
        }
        return i;
    }
    public Sprite GetTileSprite(int i)
    {
        i = NormelizadTileSetIndicator(i);

        return groundTileSet[i];
    }

    public Sprite GetHazzardSprite(int i)
    {
        i = NormelizadHazzardSetIndicator(i);

        return hazzardTileSet[i];
    }

    public Portal GetLevelPortal(int i)
    {
        return levelPortals[i];
    }

    public void TurnMiscsOn()
    {
        levelMiscHolder.SetActive(true);
    }

    public void TurnMiscsOff()
    {
        levelMiscHolder.SetActive(false);
    }

    public void EnsilayzeNeborDictenry(int midel, int left, int right)
    {
        NeborsIndex.Add("MidelNebor", midel);
        NeborsIndex.Add("RightNebor", right);
        NeborsIndex.Add("LeftNebor", left);
        //NeborsIndex.Add("BottomNebor", bottom);
    }

    public int GetNeborKey(string neborName)
    {
        int index;
        NeborsIndex.TryGetValue(neborName, out index);
        return index;
    }

    public Texture2D[] GetBackgrounds(Texture2D[] currentBackgrounds)
    {
        for (int i = 0; i < backgroundCount; i++)
        {
            currentBackgrounds[i] = backgrounds[i];
        }
        return currentBackgrounds;
    }

    public Texture2D GetLevelMap()
    {
        return levelMap;
    }


    public AudioClip GetBackgroundMusic()
    {
        return levelBackMusic;
    }

    static string GetMapPath(int lvl_num, string mainPath)
    {
        string path =  mainPath + "/LevelInfoFiles/" + lvl_num + "/level" + lvl_num + "Map.png"; //level100Map.png
        return path;
    }

    static string GetBackgroundPath(int lvl_num,int layerIndex, string mainPath)
    {
        string path = mainPath + "/LevelInfoFiles/" + lvl_num + "/Backgrounds/" + layerIndex + "layer.png";
        return path;
    }

    public void FindMyPortals()
    {
        levelPortals[0] = GameObject.Find("LeftPortal(Clone)").GetComponent<Portal>(); //find portal
        levelPortals[0].conectedToLevel = leftNeborIndex;                       //Set To Level

        if (isSideRoom)
        {
             return;
        }

        if (isFirstRoom) //turial level is the only level with left and right portal but no midel portal
        {
            levelPortals[1] = GameObject.Find("RightPortal(Clone)").GetComponent<Portal>();
            levelPortals[1].conectedToLevel = rightNeborIndex;

            return;
        }
        levelPortals[1] = GameObject.Find("MidelPortal(Clone)").GetComponent<Portal>();
        levelPortals[1].conectedToLevel = midelNeborIndex;
        
        levelPortals[2] = GameObject.Find("RightPortal(Clone)").GetComponent<Portal>();
        levelPortals[2].conectedToLevel = rightNeborIndex;       
    }
    
    public static Texture2D LoadPNG(string filePath)     
    {
        Debug.Log(filePath);


        Texture2D tex = null;        
        byte[] fileData;

        bool doesExsit = File.Exists(filePath);
        Debug.Log(doesExsit);
        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(152, 60);
            ImageConversion.LoadImage(tex,fileData,false); //..this will auto-resize the texture dimensions.
          
        }
        return tex;
    }

    public GameObject[] GetLevelLights()
    {
        return levelLights;
    }
    


}
