using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPositionManager : MonoBehaviour
{
    public Camera myCamera;
    public Vector3 clickPosition = -Vector3.one;
    
    
    void Start()
    {
        myCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray,out hit))
            {
                clickPosition = hit.point;
            }
        }
    }
}
