using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazzardsSpriteManager : TileStateMachine
{
    public Sprite mySprite;
    public LayerMask hazzardsLayer;
    public int stateIndecitor;


    public bool bottomMidelTile = false;
    //public bool bottomLeftTile = false;
    //public bool bottomRightTile = false;
    
    public bool topMidelTile = false;
    //public bool topLeftTile = false;
    //public bool topRightTile = false;
    
    public bool rightMidelTile = false;
    public bool leftMidelTile = false;

    //public bool sideTile = false;


    public GameObject levelGenerator;

    public Sprite[] tileSet;

    public float collisionRadius = 0.25f;
    public Vector2 bottomMidelOffset, bottomRightOffset, bottomLeftOffset, midelRightOffset, midelLeftOffset, topMidelOffset, topLeftOffset, topRightOffset;
    private Color debugCollisionColor = Color.red;

    private void Start()
    {
        State = new TopLeft();
        mySprite = GetComponentInChildren<SpriteRenderer>().sprite;
        levelGenerator = GetComponentInParent<LevelGenerator>().gameObject;
    }


    void Update()
    {
        if (GetComponentInParent<LevelGenerator>().finnishedBuild)
        {
            CheckSrounding();
            SetTileSprite(CheckSrounding());
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomMidelOffset, midelRightOffset, midelLeftOffset, topMidelOffset };
        Gizmos.DrawWireSphere((Vector2)transform.position + topMidelOffset, collisionRadius); //TopMidel
        //Gizmos.DrawWireSphere((Vector2)transform.position + topLeftOffset, collisionRadius); //TopLeft
        //Gizmos.DrawWireSphere((Vector2)transform.position + topRightOffset, collisionRadius); //TopRight

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomMidelOffset, collisionRadius); //BottomMidel
        //Gizmos.DrawWireSphere((Vector2)transform.position + bottomRightOffset, collisionRadius); //BottomRight
        //Gizmos.DrawWireSphere((Vector2)transform.position + bottomLeftOffset, collisionRadius); //BottomLeft


        Gizmos.DrawWireSphere((Vector2)transform.position + midelRightOffset, collisionRadius); //MidelRight
        Gizmos.DrawWireSphere((Vector2)transform.position + midelLeftOffset, collisionRadius); //MidelLeft

    }

    public bool[] CheckSrounding()
    {

        bool[] checkingStateAR;

        topMidelTile = Physics2D.OverlapCircle((Vector2)transform.position + topMidelOffset, collisionRadius, hazzardsLayer);
        //topRightTile = Physics2D.OverlapCircle((Vector2)transform.position + topRightOffset, collisionRadius, hazzardsLayer);
        //topLeftTile = Physics2D.OverlapCircle((Vector2)transform.position + topLeftOffset, collisionRadius, hazzardsLayer);

        bottomMidelTile = Physics2D.OverlapCircle((Vector2)transform.position + bottomMidelOffset, collisionRadius, hazzardsLayer);
        //bottomLeftTile = Physics2D.OverlapCircle((Vector2)transform.position + bottomLeftOffset, collisionRadius, hazzardsLayer);
        //bottomRightTile = Physics2D.OverlapCircle((Vector2)transform.position + bottomRightOffset, collisionRadius, hazzardsLayer);

        //sideTile = Physics2D.OverlapCircle((Vector2)transform.position + midelRightOffset, collisionRadius, hazzardsLayer)
        //    || Physics2D.OverlapCircle((Vector2)transform.position + midelLeftOffset, collisionRadius, hazzardsLayer);

        rightMidelTile = Physics2D.OverlapCircle((Vector2)transform.position + midelRightOffset, collisionRadius, hazzardsLayer);
        leftMidelTile = Physics2D.OverlapCircle((Vector2)transform.position + midelLeftOffset, collisionRadius, hazzardsLayer);
        return checkingStateAR = new bool[8] { false, topMidelTile, false, leftMidelTile, rightMidelTile, false, bottomMidelTile, false };

    }
    private bool[,] StatesIndex = {
        {false,true,false,false,false,false,false,false }, //top
        {false,false,false,true,false,false,false,false }, //left
        {false,false,false,false,true,false,false,false }, //right        
        {false,false,false,false,false,false,true,false }, //bottom
    };

    public void ChangeMySpriteTo(Sprite s)
    {
        this.GetComponentInChildren<SpriteRenderer>().sprite = s;
    }


    public void SetTileSprite(bool[] checkingStateAR)
    {
        //Sprite s = GetComponent<SpriteRenderer>().sprite;
        for (stateIndecitor = 0; stateIndecitor < StatesIndex.GetLength(0); stateIndecitor++)
        {
            for (int silgleTileCheck = 0; silgleTileCheck < StatesIndex.GetLength(1); silgleTileCheck++)
            {
                if (checkingStateAR[silgleTileCheck] != StatesIndex[stateIndecitor, silgleTileCheck])
                {
                    break;
                }
                else if (silgleTileCheck == StatesIndex.GetLength(1) - 1)
                {
                    State.Handle(this,  stateIndecitor);
                    //ChangeMySpriteTo(LevelManager.Instance.loadedLevelInfo.GetHazzardSprite(stateIndecitor));

                    break;
                }
            }
        }
    }
    
}
