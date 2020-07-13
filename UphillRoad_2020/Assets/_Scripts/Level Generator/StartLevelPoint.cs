using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelPoint : MonoBehaviour
{
    //public LevelManager levelManager;
    public string destnetionName;
    public void Start()
    {
        //levelManager = GameObject.Find("LevelManger").GetComponent<LevelManager>();
        if (LevelManager.Instance.loadedLevelInfo.GetLevelKey() == 100)
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.W) && collision.CompareTag("MoveableObject"))
        {
            LevelManager.Instance.GetComponent<SceneTransitions>().StartLevelTimer();
            int nextLevelIndex, currentLevelIndex;
            LevelManager.Instance.loadedLevelInfo.NeborsIndex.TryGetValue(destnetionName, out nextLevelIndex);
            currentLevelIndex = LevelManager.Instance.loadedLevelInfo.GetLevelKey();
           // levelManager.GetComponent<LevelManager>().ChooseLevel(nextLevelIndex, currentLevelIndex, false);
        }
    }
}
