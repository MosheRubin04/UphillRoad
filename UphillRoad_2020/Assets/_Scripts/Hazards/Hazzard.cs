using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazzard : MonoBehaviour
{
    /*
    public GameObject playerSpawenLoction;
    //public GameObject levelManger;
    public float deathTimer;

    private void Start()
    {
        deathParticals = GetComponent<ParticleSystem>();
        //levelManger = GameObject.Find("LevelManger");
        playerSpawenLoction = GameObject.Find("LeftPortal(Clone)");
    }
    private void Update()
    {
        if(playerSpawenLoction == null)
        {
            playerSpawenLoction = GameObject.Find("LeftPortal(Clone)");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        IEnumerator DeathTimer()
        {
            yield return new WaitForSeconds(deathTimer);
            LevelManager.Instance.GetComponent<SceneTransitions>().isExiting = true;
            collision.collider.transform.position = playerSpawenLoction.transform.position;
            //collision.gameObject.SetActive(true);

        }

        if (collision.collider.tag == "MoveableObject")
        {
            LevelManager.Instance.GetComponent<AudioManager>().PlayDeathAudio();
            //collision.gameObject.SetActive(false);            
        
            collision.collider.GetComponentInChildren<Animator>().SetTrigger("isDead");
            LevelManager.Instance.GetComponent<SceneTransitions>().isEntring = true; ;
            deathParticals.Play();
            StartCoroutine(DeathTimer());
        }
    }

    */
}
