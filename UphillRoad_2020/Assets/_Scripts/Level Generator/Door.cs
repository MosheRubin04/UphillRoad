using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject topPart, bottomPart;
    public bool hasKey;
    public bool isFullyOpen;

    public float speed;
    public float fullyOpenDistance;

    [SerializeField] Sprite closedTop, closedBottom, openTop, openBottom;


    private void Awake()
    {
        closedBottom = LevelManager.Instance.loadedLevelInfo.closedDoorBottom;
        closedTop = LevelManager.Instance.loadedLevelInfo.closedDoorTop;
        openBottom = LevelManager.Instance.loadedLevelInfo.openDoorBottom;
        openTop = LevelManager.Instance.loadedLevelInfo.openDoorTop;
    }


    public void OpenDoor()
    {
        if (!isFullyOpen)
        {
            topPart.GetComponent<SpriteRenderer>().sprite = openTop;
            bottomPart.GetComponent<SpriteRenderer>().sprite = openBottom;
            topPart.transform.Translate(Vector3.up * speed * Time.deltaTime);
            bottomPart.transform.Translate(Vector3.down * speed * Time.deltaTime);
            if (Vector2.Distance(topPart.transform.position, bottomPart.transform.position) >= fullyOpenDistance)
            {
                isFullyOpen = true;
            }
        }
    }

    public void CloseDoor()
    {

        if (isFullyOpen)
        {

            topPart.GetComponent<SpriteRenderer>().sprite = closedTop;
            bottomPart.GetComponent<SpriteRenderer>().sprite = closedBottom;
            topPart.transform.Translate(Vector3.down * speed * Time.deltaTime);
            bottomPart.transform.Translate(Vector3.down * speed * Time.deltaTime);
            if (Vector2.Distance(topPart.transform.position, bottomPart.transform.position) >= 0)
            {
                isFullyOpen = false;
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(topPart.transform.position, bottomPart.transform.position);
    }


    private void Update()
    {
        if (hasKey)
        {
            OpenDoor();
        }
    }

}
