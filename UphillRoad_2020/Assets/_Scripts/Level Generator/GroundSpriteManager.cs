using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpriteManager : TileStateMachine
{
    [SerializeField] TileStateMachine myState;


    public LayerMask groundLayer;
    public Sprite tileSprite;



    public int stateIndecitor;

    public bool bottomMidelTile = false;
    public bool bottomLeftTile = false;
    public bool bottomRightTile = false;

    public bool topMidelTile = false;
    public bool topLeftTile = false;
    public bool topRightTile = false;
    
    public bool rightMidelTile = false;
    public bool leftMidelTile = false;

    public bool sideTile = false;

    //private LevelManager levelManager;

    private bool[,] StatesIndex = {
        {false, false, false, false, true, false, true, true }, //TopLeft = 0
        {false,false,false,true,true,true,true,true }, //TopMidel = 1
        {false,false,false,true,false,true,true,false }, //TopRight = 2
        {false, true, true,false,true,false,true,true }, //MidelLeft = 3
        {true,true,false,true,false,true,true,false }, //MIdelRight = 4
        {false,true,true,false,true,false,false,false }, //BottomLeft = 5
        {true,true,true,true,true,false,false,false }, //BottomMidel = 6
        {true,true,false,true,false,false,false,false }, //BottomRight = 7
        {true,true,false,true,true,true,true,true }, //BottomLeftCorner = 8
        {false,true,true,true,true,true,true,true }, //BottomRightCorner = 9
        {true,true,true,true,true,true,true,false }, //TopLeftCorner = 10
        {true,true,true,true,true,false,true,true }, //TopRightCorner = 11
        {true,true,true,true,true,true,true,true }, //Center = 12
        {false,true,false,false,false,false,false,false }, //SingelBottom = 13
        {false,false,false,true,false,false,false,false }, //SingelRight = 14
        {false,false,false,false,true,false,false,false }, //SingleLeft = 15
        {false,false,false,false,false,false,true,false }, //SingleTop = 16
        {false,true,false,true,true,false,true,false }, //Cross = 17
        {false,false,false,false,false,false,false,false }, //Alone = 18
        {false,false,false,true,true,false,false,false }, //MidelHorizuntal = 19
        {false,true,false,false,false,false,true,false }, //MidelVertical = 20
        {false,false, true,true,true,true,true,true}, //LeftToBRcorner = 21
        {true,false,false,true,true,true,true,true }, //RightToBLcorner = 22
        {true,true,true,true,true,false,false,true }, //LeftToTRcorner = 23
        {true,true,true,true,true,true,false,false }, //RightToTLcorner = 24
        {false,true,true,false,true,true,true,true }, //TopToBRcorner = 25
        {true,true,false,true,false,true,true,true }, //TopToBLcorner = 26
        {true,true,true,false,true,false,true,true }, //BottomToTRcorner = 27
        {true,true,true,true,false,true,true,false }, //BottomToTLcorner = 28
        {true,true,true,true,false,true,false,false }, //StarirsTL = 29
        {true,true,true,false,true,false,false,true }, //StairsTR = 30
        {true,false,false,true,false,true,true,true }, //StairsBL = 31
        {false,false,true,false,true,true,true,true }, //StairsBR = 32
        {true,true,false,true,false,true,false,false }, //TLCvariantA = 33
        {false,true,true,false,true,false,false,true }, //TRCvariantA = 34
        {true,false,false,true,false,true,true,false}, //BLCvariantA = 35
        {false,false,true,false,true,false,true,true }, //BRCvariantA = 36
        {true,true,true,true,false,false,false,false }, //TLCvariantB = 37
        {true,true,true,false,true,false,false,false }, //TRCvariantB = 38
        {false,false,false,true,false,true,true,true }, //BLCvariantB = 39
        {false,false,false,false,true,true,true,true } //BRCvariantB = 40
    };



    public GameObject levelGenerator;

    //public Sprite[] tileSet;

    public float collisionRadius = 0.25f;
    public Vector2 bottomMidelOffset, bottomRightOffset, bottomLeftOffset, midelRightOffset,  midelLeftOffset, topMidelOffset, topLeftOffset, topRightOffset;
    private Color debugCollisionColor = Color.red;

    private void Awake()
    {
        State = new TopLeft();
        levelGenerator = GetComponentInParent<LevelGenerator>().gameObject;
        tileSprite = this.GetComponent<SpriteRenderer>().sprite;
        //levelManager = GameObject.Find("LevelManger").GetComponent<LevelManager>();
    }


    void Update()
    {
        if (GetComponentInParent<LevelGenerator>().finnishedBuild)
        {
            SetTileSprite(CheckSrounding());            
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomMidelOffset, midelRightOffset, midelLeftOffset,topMidelOffset };
        Gizmos.DrawWireSphere((Vector2)transform.position + topMidelOffset, collisionRadius); //TopMidel
        Gizmos.DrawWireSphere((Vector2)transform.position + topLeftOffset, collisionRadius); //TopLeft
        Gizmos.DrawWireSphere((Vector2)transform.position + topRightOffset, collisionRadius); //TopRight

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomMidelOffset, collisionRadius); //BottomMidel
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomRightOffset, collisionRadius); //BottomRight
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomLeftOffset, collisionRadius); //BottomLeft


        Gizmos.DrawWireSphere((Vector2)transform.position + midelRightOffset, collisionRadius); //MidelRight
        Gizmos.DrawWireSphere((Vector2)transform.position + midelLeftOffset, collisionRadius); //MidelLeft

    }

    public bool[] CheckSrounding()
    {

        bool[] checkingStateAR;       
        topMidelTile = Physics2D.OverlapCircle((Vector2)transform.position + topMidelOffset, collisionRadius, groundLayer);
        topRightTile = Physics2D.OverlapCircle((Vector2)transform.position + topRightOffset, collisionRadius, groundLayer);
        topLeftTile = Physics2D.OverlapCircle((Vector2)transform.position + topLeftOffset, collisionRadius, groundLayer);

        bottomMidelTile = Physics2D.OverlapCircle((Vector2)transform.position + bottomMidelOffset, collisionRadius, groundLayer);
        bottomLeftTile = Physics2D.OverlapCircle((Vector2)transform.position + bottomLeftOffset, collisionRadius, groundLayer);
        bottomRightTile = Physics2D.OverlapCircle((Vector2)transform.position + bottomRightOffset, collisionRadius, groundLayer);
               
        sideTile = Physics2D.OverlapCircle((Vector2)transform.position + midelRightOffset, collisionRadius, groundLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + midelLeftOffset, collisionRadius, groundLayer);

        rightMidelTile = Physics2D.OverlapCircle((Vector2)transform.position + midelRightOffset, collisionRadius, groundLayer);
        leftMidelTile = Physics2D.OverlapCircle((Vector2)transform.position + midelLeftOffset, collisionRadius, groundLayer);
        return checkingStateAR = new bool[8]{ topLeftTile, topMidelTile, topRightTile, leftMidelTile, rightMidelTile, bottomLeftTile, bottomMidelTile, bottomRightTile};

    }

    public void ChangeMySpriteTo(Sprite s)
    {
        this.GetComponent<SpriteRenderer>().sprite = s;
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
                else if (silgleTileCheck == StatesIndex.GetLength(1) -1 )
                {
                    State.Handle(this,  stateIndecitor);                    
                    ChangeMySpriteTo(LevelManager.Instance.loadedLevelInfo.GetTileSprite(stateIndecitor));
                    if(stateIndecitor == 20)
                    {
                        transform.Rotate(0,0,90);
                    }
                    break;
                }
            }
        }        
    }    
}
