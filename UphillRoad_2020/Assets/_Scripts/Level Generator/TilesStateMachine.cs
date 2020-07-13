using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileStateMachine : MonoBehaviour
{
    public StateBase State;
    
}

public enum TileState
{
    TopLeft,
    TopMidell,
    TopRight,

    BottomLeft,
    BottomMidell,
    BottomRight,

    MidellLeft,
    MidellRight,

    BottomLeftCorner,
    BottomRightCorner,
    TopLeftCorner,
    TopRightCorner,

    Center,

    SingleLeft, 
    SingleRight,
    SingleTop,
    SingleBottom,

    Cross,
    Alone,

    MidelVertical,
    MidelHorizuntal,

    LeftToBRcorner,
    RightToBLcorner,
    LeftToTRcorner,
    RightToTLcorner,
    TopToBRcorner,
    TopToBLcorner,
    BottomToTRcorner,
    BottomToTLcorner,    
}

public abstract class StateBase
{
    public abstract void Handle(TileStateMachine context, int tileIndex);
}


public class TopLeft : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new TopLeft(); 
    }
}


public class TopMidell : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new TopMidell();               
    }
}

public class TopRight : StateBase
{
    public override void Handle(TileStateMachine context,int tileIndex)
    {
        context.State = new TopRight();        
    }
}

public class MidellLeft : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new MidellLeft();
        context.transform.Rotate(new Vector3(0, 0, 90));
    }
}

public class MidellRight : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new MidellRight();     
        context.transform.Rotate(new Vector3(0, 0, 90));

    }
}

public class BottomLeft : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new BottomLeft();        
    }
}

public class BottomMidell : StateBase
{
    public override void Handle(TileStateMachine context,  int tileIndex)
    {
        context.State = new BottomMidell();                    
    }
}

public class BottomRight : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new BottomRight();        
    }
}

public class BottomLeftCorner : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new BottomLeftCorner();        
    }
}

public class BottomRightCorner : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new BottomRightCorner();        
    }
}

public class TopLeftCorner : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new TopLeftCorner();        
    }
}

public class TopRightCorner : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new TopRightCorner();        
    }
}

public class Centerr : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new Centerr();
    }
}

public class SingleBottom : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.transform.Rotate(Vector3.left);
        context.State = new SingleBottom();
    }
}

public class SingleRight : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new SingleRight();
    }
}

public class SingleLeft : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new SingleLeft();
    }
}

public class SingleTop : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.transform.Rotate(new Vector3(0, 0, 90));
        context.State = new SingleTop();
    }
}


public class Cross : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new Cross();
    }
}

public class Alone : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new Alone();
    }
}

public class MidelHorizuntal : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new MidelHorizuntal();
        context.transform.Rotate(new Vector3(0,0,90));
    }
}

public class MidelVertical : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new MidelVertical();
    }
}

public class LeftToBRcorner : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new LeftToBRcorner();
    }
}

public class RightToBLcorner : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new RightToBLcorner();
    }
}

public class LeftToTRcorner : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new LeftToTRcorner();
    }
}

public class RightToTLcorner : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new RightToTLcorner();
    }
}

public class TopToBRcorner : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new TopToBRcorner();
    }
}

public class TopToBLcorner : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new TopToBLcorner();
    }
}

public class BottomToTRcorner : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new BottomToTRcorner();
    }
}

public class BottomToTLcorner : StateBase
{
    public override void Handle(TileStateMachine context, int tileIndex)
    {
        context.State = new BottomToTLcorner();
    }
}

