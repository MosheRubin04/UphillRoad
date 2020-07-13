using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject[] activeBackgrounds = new GameObject[7];
    public GameObject[] activeBackgrounds2 = new GameObject[7];

    public void LoadBackgrounds(LevelInfo levelInfo)
    {
        Texture2D[] backGrounds = new Texture2D[7];
        levelInfo.GetBackgrounds(backGrounds);

        for (int i = 0; i < activeBackgrounds.Length; i++)
        {
            activeBackgrounds[i].gameObject.SetActive(false); //Turn all backgrounds off
            activeBackgrounds2[i].gameObject.SetActive(false);
        }


        for (int i = 0; i < levelInfo.backgroundCount; i++)
        {
            activeBackgrounds[i].gameObject.SetActive(true); //Turn only the neaded backgrounds on.
            activeBackgrounds[i].GetComponent<SpriteRenderer>().material.SetTexture("_MainTex", backGrounds[i]);
            activeBackgrounds[i].GetComponent<Parallax>().SetTexture(backGrounds[i]);

            activeBackgrounds2[i].gameObject.SetActive(true);
            activeBackgrounds2[i].GetComponent<SpriteRenderer>().material.SetTexture("_MainTex", backGrounds[i]);
            activeBackgrounds2[i].GetComponent<Parallax>().SetTexture(backGrounds[i]);
            
        }
    }
}
