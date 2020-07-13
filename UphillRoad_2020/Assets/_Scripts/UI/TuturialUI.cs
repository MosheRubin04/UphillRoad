using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TuturialUI : MonoBehaviour
{

    public float speed;
    [SerializeField] Animator anim;
    

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FadeIn(float speed)
    {
        anim.SetBool("IsFading", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FadeIn(speed);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.SetBool("IsFading", false);
    }

    private void Update()
    {
        if (LevelManager.Instance.loadedLevelInfo.GetLevelKey() != 1)
        {
            Destroy(gameObject);
        }
    }


}
