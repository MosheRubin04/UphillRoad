using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadMeController: MonoBehaviour  
{

    public Readme readme = new Readme();





    private void Start()
    {

        readme.title = "Uphill Road ReadMe";
        Debug.Log(readme.title);
    }
}
