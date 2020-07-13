using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public Texture2D map;
    public bool finnishedBuild = false;
    public ColorToPrefab[] colorMapping;
    public GameObject[] LevelChilderns;
    public float buildingTimer;
    public int genretedTilesCount;

    public int ConvertDictineryIndexToGeneratorIndex(int dicIndex)
    {
        int geneIndex = 999;

        switch (dicIndex)
        {
            case 100:
                geneIndex = 0;
                return geneIndex;
            case 200:
                geneIndex = 1;
                return geneIndex;
            case 300:
                geneIndex = 2;
                return geneIndex;
            case 101:
                geneIndex = 3;
                return geneIndex;
            case 201:
                geneIndex = 4;
                return geneIndex;
            case 301:
                geneIndex = 5;
                return geneIndex;
            case 1:
                geneIndex = 8;
                return geneIndex;
            default:
                return geneIndex;                

        }        
        
    }


    public void GenerateLevel(int currentLevelID)
    {
        genretedTilesCount = 0;
        currentLevelID = ConvertDictineryIndexToGeneratorIndex(currentLevelID);

        if(currentLevelID == 999)
        {
            Debug.Log("Index isnt converted! Add More Level Children in Level Generator");
            return;
        }
        Debug.Log("currentLevelID = " + currentLevelID);
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y,LevelChilderns[currentLevelID]);                
                //genretedTilesCount++;
            }
        }
        
        finnishedBuild = true;
        Debug.Log("Generated " + genretedTilesCount + " tiles for this level");
        StartCoroutine(WaitForLevelToLoad());
        LevelManager.Instance.loadedLevelInfo.TurnMiscsOn();


    }


    public static bool ColorEquals(Color a, Color b, float tolerance = 0.04f)
    {
        if (a.r > b.r + tolerance) return false;
        if (a.g > b.g + tolerance) return false;
        if (a.b > b.b + tolerance) return false;
        if (a.r < b.r - tolerance) return false;
        if (a.g < b.g - tolerance) return false;
        if (a.b < b.b - tolerance) return false;

        return true;
    }



    void GenerateTile(int x, int y, GameObject currentLevel)
    {
        Color pixelColor = map.GetPixel(x, y);
        if (pixelColor.a == 0)
        {
            return;
        }
        pixelColor.a = 1;
        
        foreach (ColorToPrefab item in colorMapping)
        {
            if (ColorEquals(item.color,pixelColor))
            {
                Vector2 position = new Vector2(x, y);
                Instantiate(item.prefab, position, Quaternion.identity, currentLevel.transform);
                genretedTilesCount++;
                break;
            }
        }
    }

    IEnumerator WaitForLevelToLoad()
    {
        yield return new WaitForSeconds(buildingTimer);
        finnishedBuild = false;
    }

    public void CloaseGeneratedLevel(int levelIndex)
    {

        levelIndex = ConvertDictineryIndexToGeneratorIndex(levelIndex);
        LevelChilderns[levelIndex].SetActive(false);

    }


    public void OpenGeneratedLevel(int levelIndex)
    {
        levelIndex = ConvertDictineryIndexToGeneratorIndex(levelIndex);
        LevelChilderns[levelIndex].SetActive(true);
        LevelManager.Instance.loadedLevelInfo.TurnMiscsOn(); // open all miscs
    }
    

}
