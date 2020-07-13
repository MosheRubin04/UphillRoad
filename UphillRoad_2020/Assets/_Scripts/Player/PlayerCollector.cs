using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCollector : MonoBehaviour
{


    public Collectable onPlayer;
    public Collectable[] collectedPhotos = new Collectable[3];
    public int collectedPhotosCount = 0;

    public Transform colectedItemTransform;

    public GameObject collectablePanel;
    public Image[] photosInPanel;

    
    public void Start()
    {
        //photosInPanel = collectablePanel.GetComponentsInChildren<Image>();
    }



    public void Update()
    {
        if(onPlayer != null && onPlayer.collected)
        {
            onPlayer.CarryMe(colectedItemTransform);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            collectablePanel.SetActive(value: collectablePanel.active ? false : true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectable")
        {
            if (collision.GetComponent<Collectable>().myType == Collectable.CollectableType.Key)
            {
                onPlayer = collision.GetComponent<Collectable>();
                onPlayer.collected = true;
            }
            else if (collision.GetComponent<Collectable>().myType == Collectable.CollectableType.Photo)
            {
                onPlayer = collision.GetComponent<Collectable>();
                onPlayer.collected = true;
            }
        }

        else if (collision.tag == "Portal" && onPlayer.myType == Collectable.CollectableType.Photo)
        {
            collectedPhotos[onPlayer.collectableID] = onPlayer;
            collectablePanel.SetActive(true);
            for (int i = 0; i < photosInPanel.Length; i++)
            {
                photosInPanel[i].gameObject.SetActive(false);
            }
            photosInPanel[onPlayer.collectableID].gameObject.SetActive(true);
            onPlayer.isUsed = true;
            onPlayer = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Door" && onPlayer.myType == Collectable.CollectableType.Key)
        {
            collision.collider.GetComponentInParent<Door>().hasKey = true;
            onPlayer.isUsed = true;
        }
    }


}
