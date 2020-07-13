using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class Parallax : MonoBehaviour
{
    private float startPos;
    private float textureUnitSizeX;
    private float textureUnitSizeY;
    [SerializeField] private int textureWidth;
    [SerializeField] private int textureHeight;
    [SerializeField] float direction; // 1 for right -1 for left


    private Vector3 lastCameraPosition;
    public Transform cam;

    //public GameObject mainSprite;

    public Vector2 parallaxEffectValue;

    public bool infiniteHorizontal;
    public bool infiniteVertical;

    public SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        cam = GameObject.Find("CM vcam1").transform;

    }

    private void Awake()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        cam = GameObject.Find("CM vcam1").transform;
    }


    public void SetTexture(Texture2D tex)
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        Rect rect = new Rect(spriteRenderer.sprite.pivot, new Vector2(tex.width, tex.height));
        Sprite s = Sprite.Create(tex, rect, spriteRenderer.sprite.pivot);
        this.GetComponent<SpriteRenderer>().sprite = s;
        
        lastCameraPosition = cam.position;
        startPos = transform.position.x;
        
        Texture2D texture = s.texture;
        textureWidth = texture.width;
        textureHeight = texture.height;
        textureUnitSizeX = texture.width / s.pixelsPerUnit;
        textureUnitSizeY = texture.height / s.pixelsPerUnit;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }


    private void FixedUpdate()
    {    
        Vector3 deltaMovement = cam.position - lastCameraPosition;
        transform.position += new Vector3 (deltaMovement.x * parallaxEffectValue.x * direction, deltaMovement.y * parallaxEffectValue.y);
        lastCameraPosition = cam.position;
        /*
        if (infiniteHorizontal)
        {
            if (Mathf.Abs(cam.position.x - transform.position.x) >= textureUnitSizeX)
            {
                float offsetPositionX = ((cam.position.x - transform.position.x) * direction) % textureUnitSizeX;
                transform.position = new Vector3(cam.position.x + offsetPositionX, transform.position.y);
            }
        }
        if (infiniteVertical)
        {
            if (Mathf.Abs(cam.position.y - transform.position.y) >= textureUnitSizeY)
            {
                float offsetPositionX = (cam.position.y - transform.position.y) % textureUnitSizeY;
                transform.position = new Vector3(cam.position.y + offsetPositionX, transform.position.y);
            }
        }*/

        /*
        float dist = (cam.position.x * parallaxEffect);
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
        */
    }
}
