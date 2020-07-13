using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class GravityCreator : MonoBehaviour
{
    public GameObject pointTransform;
    public GameObject gravityField;
  
    public Camera myCamera;
    public float fieldCooldowen;
    public float cooldowenAmount = 10;
    public float maxCooldowen;
  


  
    void Start()
    {        
        fieldCooldowen = maxCooldowen;
        gameObject.tag = "MoveableObject";
        myCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        pointTransform = GameObject.Find("FieldCreationPoint");     
    }

    
    void Update()
    {
        this.GetComponent<LightColorController>().time -= (1/ maxCooldowen) * Time.deltaTime;
        if (fieldCooldowen <= 0)
        {            
            if (Input.GetMouseButton(1))
            {
                Ray castPoint = myCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                string a;
                if (Physics.Raycast(castPoint, out hit, 1 << LayerMask.NameToLayer("")))
                {
                    Debug.Log($"Ray for gravity field Hit: {a = hit.collider.tag} " );
                    pointTransform.transform.position = hit.point;
                    CreateField(pointTransform);
                    Debug.Log("FieldCreated");
                    this.GetComponent<LightColorController>().time = 1;
                }
                fieldCooldowen = maxCooldowen;                
                this.GetComponent<PlayerAudioManager>().PlayFieldSoutDowenAudio();
            }
            else if (Input.GetMouseButton(1) && fieldCooldowen >= maxCooldowen)
            {                
                Debug.Log("Cooldowen =" + fieldCooldowen);
                fieldCooldowen -= 1 * Time.deltaTime;
            }           
        }
        fieldCooldowen -= 1 * Time.deltaTime;        
    }

    public void CreateField(GameObject mosueLoction)
    {        
        Instantiate(gravityField, mosueLoction.transform.position, mosueLoction.transform.rotation);
    }
}
