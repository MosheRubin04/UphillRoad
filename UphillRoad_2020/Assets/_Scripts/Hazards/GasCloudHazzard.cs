using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasCloudHazzard : Hazzard
{
    public float cludColldowen;
    private float sicelCounter;
    public Transform smallScal;
    public Transform bigScale;

    public void ChangeCloudSize()
    {
        for (sicelCounter = 0; sicelCounter < 3;)
        {
            StartCoroutine(GrowingSicel());
        }

        for (sicelCounter = 3; sicelCounter > 0;)
        {
            StartCoroutine(DefflatingSicel());
        }
    }





    private void Awake()
    {
        smallScal.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        bigScale.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    private void Start()
    {
        //levelManger = GameObject.Find("LevelManger");
        //playerSpawenLoction = LevelManager.Instance.playerSpawenPoint;        
        transform.localScale = smallScal.localScale;
    }

    public void Update()
    {
        for (sicelCounter = 0; sicelCounter <= 3;)
        {
            StartCoroutine(GrowingSicel());
            sicelCounter += Time.deltaTime;
        }

        for (sicelCounter = 3; sicelCounter >= 0;)
        {
            StartCoroutine(DefflatingSicel());
            sicelCounter -= Time.deltaTime;
        }
    }


    public IEnumerator GrowingSicel()
    {
        yield return new WaitForSeconds(cludColldowen);
        transform.localScale = Vector3.Lerp(smallScal.localScale , bigScale.localScale, cludColldowen);
        sicelCounter++;
    }

    public IEnumerator DefflatingSicel()
    {
        yield return new WaitForSeconds(cludColldowen);
        transform.localScale = Vector3.Lerp(bigScale.localScale, smallScal.localScale, cludColldowen);
        sicelCounter--;
    }


}
