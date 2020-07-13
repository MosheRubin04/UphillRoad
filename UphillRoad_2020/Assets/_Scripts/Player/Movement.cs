using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    Collision coll;

    [Header("Stats")]
    [Range(1,20)] public float speed = 10;
    [Range(1, 20)] public float jumpForce = 7;
    [Range(1, 20)] public float jumpVelocity = 5;
    [Range(1, 20)] public float slideSpeed = 10;

    public float dashSpeed = 20;
    public float wallJumpLerp = 0.5f;

    public float maxGrabCooldown, currentGrabColldown;

    [Header("Partical System")]
    public ParticleSystem wallGrabParitcalSys;
    public ParticleSystem dashOnCooldownParitaclSys;
    public GameObject particalSystemPlacment;

    [Header("Mouse Diretction Objects")]
    public Camera myCamera;
    public GameObject dashDirection;
    public Vector3 clickPosition = -Vector3.one;

    [Header("Animerions")]
    public Animator animator;

    public bool isWalking = false;
    public bool canMove = true;
    public bool isDashing;
    [SerializeField] bool wallGrab;
    [SerializeField] bool wallSlide;
    [SerializeField] bool wallJump;
    [SerializeField] bool hasDashed;
    [SerializeField] bool groundTouch;
    [SerializeField] bool isGoingLeft;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collision>();
        dashDirection = GameObject.Find("DashDirectionPoint");        
    }

    void Update()
    {        
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y);

        Walk(direction);
        FlipSprite(xRaw,GetComponent<Collision>().onRightWall);

        if (Input.GetButtonDown("Jump"))
        {
           if(coll.onGround)
           {
                Jump(Vector2.up, false);
           }
            if (coll.nearWall && !coll.onGround)
            {
                WallJump();
            }
        }

        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if (!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }

        if (coll.onGround && !isDashing)
        {
            wallJump = false;
            GetComponent<Jump>().enabled = true;
        }

        wallGrab = coll.nearWall && Input.GetAxis("Fire3") == 1;
        if(wallGrab)
        {            
            wallGrabParitcalSys.enableEmission = true;
            rb.velocity =  new Vector2(0, y * speed); // new Vector2(rb.velocity.x, y * speed); 
            currentGrabColldown -= Time.deltaTime;
        }
        else
        {            
            wallGrabParitcalSys.enableEmission = false;
        }

        if (coll.nearWall && !coll.onGround && !wallGrab)
        {
            wallSlide = true;
            WallSlide();         
        }

        if (!coll.nearWall || coll.onGround)
            wallSlide = false;

        if (Input.GetButtonDown("Fire1") && !hasDashed)
        {
            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                clickPosition = hit.point;
                clickPosition -= transform.position;
                Dash(clickPosition.x, clickPosition.y);  //fixed dash direction
            }
        }        
        
        if (wallGrab && !isDashing)
        {
            rb.gravityScale = 0;
            if (x > .2f || x < -.2f)
                rb.velocity = new Vector2(rb.velocity.x, 0);

            float speedModifier = y > 0 ? .5f : 1;

            rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));

        }
        else
        {
            rb.gravityScale = 1;
        }

        animator.SetBool("isDashing", isDashing);
        animator.SetBool("wallSlide", wallSlide);
        animator.SetBool("wallGrab", wallGrab);
        animator.SetBool("canMove", canMove);
        animator.SetBool("isGoingLeft", isGoingLeft);

        //anim.SetBool("jump",)
        animator.SetFloat("HorizontalAxis",x );
        animator.SetFloat("VerticalAxis", y);
        animator.SetFloat("VerticalVelocity", rb.velocity.y);
        this.GetComponent<PlayerAudioManager>().PlayWalkAudio();
    }


    public void FlipSprite(float x, bool onRightWall)
    {
        if (x < 0 )
        {
            isGoingLeft = true;
            transform.localScale = new Vector2(-1f, 1f);
        }

        else if(x > 0 || onRightWall)
        {
            isGoingLeft = false;
            transform.localScale = new Vector2(1f, 1f);
        }
    }

    private void Walk(Vector2 direction)
    {
        if (!canMove)
            return;
        if (wallGrab)
            return;
        /*
        if (coll.nearWall && !coll.onGround && !wallGrab)        
            return;
        */
        this.GetComponent<PlayerAudioManager>().MuteWalkAudio(false);

        if (!wallJump && !wallGrab)
        {
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(direction.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
            this.GetComponent<PlayerAudioManager>().MuteWalkAudio(true);
        }

    }

    private void WallSlide()
    {
        if (!canMove)
            return;

        rb.velocity = new Vector2(rb.velocity.x, -slideSpeed); //new Vector2(rb.velocity.x, -slideSpeed);
    }

    private void Jump(Vector2 direction, bool wall)
    {
        this.GetComponent<PlayerAudioManager>().PlayJumpAudio();
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += direction * jumpForce;
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    private void WallJump()
    {
        this.GetComponent<PlayerAudioManager>().PlayJumpAudio();
        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / 1.5f + wallDir / 1.5f), true);

        wallJump = true;
    }

    private void Dash(float x, float y)
    {
        
        hasDashed = true;
        particalSystemPlacment.GetComponent<ParticleSystem>().Play();
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb.velocity += dir.normalized * dashSpeed; 
        StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {
        StartCoroutine(GroundDash());
        rb.gravityScale = 0;
        GetComponent<Jump>().enabled = false;
        wallJump = true;
        isDashing = true;

        yield return new WaitForSeconds(.3f);

        rb.gravityScale = 2; //the gravity scale drags the chartcher down at the end of the dash. (original was 3)
        GetComponent<Jump>().enabled = true;
        wallJump = false;
        isDashing = false;
        dashOnCooldownParitaclSys.enableEmission = true;
    }

    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (coll.onGround)
            hasDashed = false;
    }

    void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;
        currentGrabColldown = maxGrabCooldown;
        dashOnCooldownParitaclSys.enableEmission = false;


    }
}
