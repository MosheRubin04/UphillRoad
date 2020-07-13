using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    public float maxGravDist;
    public float maxGravity;
    public float fieldDuration;
     public GameObject[] moveableObject;

    void OnEnable()
    {
        moveableObject = GameObject.FindGameObjectsWithTag("MoveableObject");
        StartCoroutine(FieldDurtion());
    }

    void FixedUpdate()
    {        
        foreach (GameObject @object in moveableObject)
        {            
            float dist = Vector3.Distance(@object.transform.position, transform.position);            
            if (dist <= maxGravDist)
            {
                Vector3 v = transform.position - @object.transform.position;
                @object.GetComponent<Rigidbody2D>().velocity +=          //was using RB2d.AddForce, changed to adding velocity
                    new Vector2(                                         //movement is much sharper now.
                        v.normalized.x * (dist / maxGravDist) * maxGravity,
                        v.normalized.y * (dist / maxGravDist) * maxGravity); 
            }                                                                                                                                                                       
        }
        if (Input.GetMouseButtonDown(1))
        {

            Destroy(gameObject);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position, maxGravDist);
    }



    IEnumerator FieldDurtion()
    {
        Debug.Log("Field Timer Started");
        yield return new WaitForSeconds(fieldDuration);
        Destroy(gameObject);
    }
}

