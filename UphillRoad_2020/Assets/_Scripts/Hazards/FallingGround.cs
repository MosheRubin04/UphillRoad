using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGround : MonoBehaviour
{

    public Transform originalPosition;
    public GameObject platform;
    [SerializeField] float restTime = 2f;

    public void Start()
    {
        //platform = gameObject.GetComponentInChildren<GameObject>();
        originalPosition = transform;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(FallingCicel());
    }

    public IEnumerator FallingCicel()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        platform.GetComponent<Rigidbody2D>().simulated = true;       
        yield return new WaitForSeconds(restTime);
        platform.GetComponent<Rigidbody2D>().simulated = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        platform.transform.position = originalPosition.position;
    }

    public void RestartFallingGround()
    {
        StopCoroutine(FallingCicel());
        platform.transform.position = originalPosition.position;
    }

}
