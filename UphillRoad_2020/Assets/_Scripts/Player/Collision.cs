using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask groundLayer;

    [Space]

    public bool onGround;
    public bool nearWall;
    public bool onRightWall;
    public bool onLeftWall;
    public int wallSide;

    [Space]

    [Header("Collision")]
    public CapsuleCollider2D myBoxCollider2D;
    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    private Color debugCollisionColor = Color.red;
    public Animator anim;

    [Header("Last Portal")]
    public Portal lastPortal;

    [Header("Death Partical")]
    public ParticleSystem deathParticals;
    public Animator animator;

    [Header("Collectabales")]
    public PlayerCollector playerCollector;

    private void Start()
    {
        myBoxCollider2D = this.GetComponentInChildren<CapsuleCollider2D>();
        playerCollector = this.GetComponent<PlayerCollector>();
    }

    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        nearWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer) && Input.GetKey(KeyCode.LeftShift);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer) && Input.GetKey(KeyCode.LeftShift);

        wallSide = onRightWall ? -1 : 1;
        anim.SetBool("onGround", onGround);
        anim.SetBool("onRightWall", onRightWall);
      
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Hazzards")
        {
            if(playerCollector.onPlayer != null)
            {
                playerCollector.onPlayer.DropMe();
                playerCollector.onPlayer = null;
            }
            Debug.Log("Killed by: " + collision.collider.name);
            animator.SetTrigger("isDead");
            deathParticals.Play();
            LevelManager.Instance.GetComponent<AudioManager>().PlayDeathAudio();
            RestartLevel(LevelManager.Instance.GetDeathTimer());
        }
    }
    


    public void RestartLevel(float deathTimer)
    {
        IEnumerator DeathTimer()
        {
            yield return new WaitForSeconds(deathTimer);
            transform.position = lastPortal.transform.position;
            myBoxCollider2D.isTrigger = false;
            LevelManager.Instance.GetComponent<SceneTransitions>().isDeathExiting = true;
        }

        myBoxCollider2D.isTrigger = true;
        LevelManager.Instance.GetComponent<SceneTransitions>().isDeathEntring = true;
        StartCoroutine(DeathTimer());
        
    }
}