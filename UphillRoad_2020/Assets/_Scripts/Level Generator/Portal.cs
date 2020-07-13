using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    //public LevelManager levelManager;
    public string destnetionName;
    
    public bool isGoingRight = true;
    public PortalType myPortalType;

    public int conectedToLevel;

    public enum PortalType
    {
        LeftPortal,
        RightPortal,
        MidelPortal,        
    }

    private void Start()
    {
        if (LevelManager.Instance.loadedLevelInfo.isFirstRoom && myPortalType == PortalType.LeftPortal)
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    
    

    private void OnTriggerStay2D(Collider2D collision)
    {
       
        if (Input.GetKeyDown(KeyCode.W) && collision.CompareTag("MoveableObject"))
        {
            LevelManager.Instance.GetComponent<SceneTransitions>().StartLevelTimer();
            int  currentLevelIndex = LevelManager.Instance.loadedLevelInfo.GetLevelKey();
            LevelManager.Instance.loadedLevelInfo.NeborsIndex.TryGetValue(destnetionName, out conectedToLevel);
            if (myPortalType == PortalType.LeftPortal)
            {
                isGoingRight = false;
            }
            else if (myPortalType == PortalType.RightPortal)
            {
                isGoingRight = true;
            }
            if (myPortalType == PortalType.MidelPortal)
            {
                isGoingRight = true;
            }
            //LevelManager.Instance.SetPlayerSpawenPoint(isGoingRight, LevelManager.Instance.loadedLevelInfo.isSideRoom);

            LevelManager.Instance.ChooseLevel(conectedToLevel, currentLevelIndex, isGoingRight);

        }
    }

    public PortalType GetMyPortalType()
    {
        return myPortalType;
    }

}
