using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public GameObject player;
    public float cameraDistance = -231.7f;
    public Vector3 offset;

    public void CameraMovment()
    {
        
        transform.position = player.transform.position + offset;
    }


    public void Start()
    {
        player = GameObject.Find("PlayerV.2");
        offset = transform.position - player.transform.position;
    }


    void LateUpdate()
    {
        CameraMovment();
    }
}
