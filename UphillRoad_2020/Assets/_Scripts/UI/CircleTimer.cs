using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CircleTimer : MonoBehaviour
{

    Image filling;
    public float timeAmt = 10;
    public float time = 0;
    public float maxColldown = 1;
    public TMP_Text timeText;
    //public GameObject fieldCretionPoint;

    // Start is called before the first frame update
    void Start()
    {
        filling = this.GetComponent<Image>();
        time = timeAmt;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Input.mousePosition;
        
        if(time < maxColldown)
        {
            time += Time.deltaTime;
            filling.fillAmount = time / timeAmt;
            timeText.text = "Time: "+ time.ToString("F");
        }
    }
}
