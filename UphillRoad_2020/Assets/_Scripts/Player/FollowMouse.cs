using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public Camera myCamera;    
    public LayerMask backgroundLayer;
    void Start()
    {
        //myCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray castPoint = myCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        string a;
        if (Physics.Raycast(castPoint, out hit)) //public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance, int layerMask);
        {
            a = hit.collider.tag;
            //Debug.Log(a);
            transform.position = hit.point;
        }        
    }
}
