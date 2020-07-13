using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelPoint : MonoBehaviour
{
    public LevelManager levelManager;
    public string destnetionName;

    public void Start()
    {
        levelManager = GameObject.Find("LevelManger").GetComponent<LevelManager>();
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.W) && collision.CompareTag("MoveableObject"))
        {        
            levelManager.GetComponent<SceneTransitions>().StartLevelTimer();
            int nextLevelIndex, currentLevelIndex;
            levelManager.loadedLevelInfo.NeborsIndex.TryGetValue(destnetionName, out nextLevelIndex);
            currentLevelIndex = levelManager.loadedLevelInfo.GetLevelKey();
            //levelManager.GetComponent<LevelManager>().ChooseLevel(nextLevelIndex, currentLevelIndex, true);
        }
    }
}
