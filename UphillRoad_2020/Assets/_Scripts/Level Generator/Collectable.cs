using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    public enum CollectableType
    {
        Key,
        Photo
    }

    public CollectableType myType;

    public Sprite mySprite;

    public bool collected = false;
    public bool isUsed;
    public int collectableID;
    [SerializeField] private Transform originalPoistion;

    private void Start()
    {
        //originalPoistion = gameObject.transform;
        if(myType == CollectableType.Key)
        {
            mySprite = LevelManager.Instance.loadedLevelInfo.GetKeySprite();
        }
        else if(myType == CollectableType.Photo)
        {
            mySprite = LevelManager.Instance.loadedLevelInfo.GetCollectableSprite();
            switch (LevelManager.Instance.loadedLevelInfo.GetLevelKey())
            {
                case 101:
                    collectableID = 0;
                    return;
                case 201:
                    collectableID = 1;
                    return;
                case 301:
                    collectableID = 2;
                    return;                    
            }
        }
    }

    public void CarryMe(Transform folowPoint)
    {
        transform.position =  folowPoint.position;
    }

    public void DropMe()
    {
        collected = false;
        transform.position = originalPoistion.position;
    }


    public void AddPhotoToCollection()
    {

    }
        
    public void Update()
    {
        if (isUsed)
        {
            //this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }



}
