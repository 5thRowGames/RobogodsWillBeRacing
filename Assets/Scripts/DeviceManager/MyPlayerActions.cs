using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase heredada de InControl que permite unificar controles de mando y teclado y darles un nombre característico
public class MyPlayerActions : PlayerActionSet
{
    public PlayerAction Up;
    public PlayerAction Right;
    public PlayerAction Down;
    public PlayerAction Left;

    public PlayerOneAxisAction Horizontal;
    public PlayerOneAxisAction Vertical;

    public PlayerTwoAxisAction Move;

    public PlayerAction Gas;
    public PlayerAction Fire;
    public PlayerAction Action;
    public PlayerAction Special;

    public PlayerAction LeftBumper;
    public PlayerAction RightBumper;
    public PlayerAction LeftTrigger;
    public PlayerAction RightTrigger;

    public PlayerAction Start;

    public PlayerAction VerticalSecondary;
    public PlayerAction HorizontalSecondary;

    public PlayerAction Submit;
    public PlayerAction Cancel;

    public MyPlayerActions()
    {
        Up = CreatePlayerAction("Move Up");
        Right = CreatePlayerAction("Move Right");
        Down = CreatePlayerAction("Move Down");
        Left = CreatePlayerAction("Move Left");

        Horizontal = CreateOneAxisPlayerAction(Left, Right);
        Vertical = CreateOneAxisPlayerAction(Down, Up);
        Move = CreateTwoAxisPlayerAction(Left, Right, Down, Up);

        Gas = CreatePlayerAction("Jump");
        Fire = CreatePlayerAction("Fire");
        Action = CreatePlayerAction("Action");
        Special = CreatePlayerAction("Special");

        LeftBumper = CreatePlayerAction("Left bumper");
        RightBumper = CreatePlayerAction("Right bumper");
        LeftTrigger = CreatePlayerAction("Left trigger");
        RightTrigger = CreatePlayerAction("Right trigger");

        Submit = CreatePlayerAction("Submit");
        Cancel = CreatePlayerAction("Cancel");

        Start = CreatePlayerAction("Start");

    }

    public static MyPlayerActions BindControls()
    {
        var myPlayerActions = new MyPlayerActions();

        myPlayerActions.Up.AddDefaultBinding(InputControlType.LeftStickUp);
        myPlayerActions.Right.AddDefaultBinding(InputControlType.LeftStickRight);
        myPlayerActions.Down.AddDefaultBinding(InputControlType.LeftStickDown);
        myPlayerActions.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
        
        myPlayerActions.Up.AddDefaultBinding(InputControlType.DPadUp);
        myPlayerActions.Right.AddDefaultBinding(InputControlType.DPadRight);
        myPlayerActions.Down.AddDefaultBinding(InputControlType.DPadDown);
        myPlayerActions.Left.AddDefaultBinding(InputControlType.DPadLeft);

        myPlayerActions.Gas.AddDefaultBinding(InputControlType.Action1);
        myPlayerActions.Fire.AddDefaultBinding(InputControlType.Action3);
        myPlayerActions.Action.AddDefaultBinding(InputControlType.Action2);
        myPlayerActions.Special.AddDefaultBinding(InputControlType.Action4);

        myPlayerActions.LeftBumper.AddDefaultBinding(InputControlType.LeftBumper);
        myPlayerActions.RightBumper.AddDefaultBinding(InputControlType.RightBumper);
        myPlayerActions.LeftTrigger.AddDefaultBinding(InputControlType.LeftTrigger);
        myPlayerActions.RightTrigger.AddDefaultBinding(InputControlType.RightTrigger);

        myPlayerActions.Submit.AddDefaultBinding(InputControlType.Action1);
        myPlayerActions.Cancel.AddDefaultBinding(InputControlType.Action2);

        return myPlayerActions;
    }

    public static MyPlayerActions BindKeyboard()
    {
        var myPlayerActions = new MyPlayerActions();

        myPlayerActions.Up.AddDefaultBinding(Key.UpArrow);
        myPlayerActions.Down.AddDefaultBinding(Key.DownArrow);
        myPlayerActions.Right.AddDefaultBinding(Key.RightArrow);
        myPlayerActions.Left.AddDefaultBinding(Key.LeftArrow);
        
        myPlayerActions.Up.AddDefaultBinding(Key.W);
        myPlayerActions.Down.AddDefaultBinding(Key.S);
        myPlayerActions.Right.AddDefaultBinding(Key.D);
        myPlayerActions.Left.AddDefaultBinding(Key.A);

        myPlayerActions.Gas.AddDefaultBinding(Key.Space);
        myPlayerActions.Fire.AddDefaultBinding(Key.Key1);
        myPlayerActions.Action.AddDefaultBinding(Key.Key3);
        myPlayerActions.Special.AddDefaultBinding(Key.Key2);

        myPlayerActions.LeftBumper.AddDefaultBinding(Key.S);
        myPlayerActions.RightBumper.AddDefaultBinding(Key.F);
        myPlayerActions.LeftTrigger.AddDefaultBinding(Key.G);
        myPlayerActions.RightTrigger.AddDefaultBinding(Key.W);

        myPlayerActions.Submit.AddDefaultBinding(Key.Return);
        myPlayerActions.Cancel.AddDefaultBinding(Key.Escape);

        return myPlayerActions;
    }
    
    //Solo de prueba
    public static MyPlayerActions BindBoth()
    {
        var myPlayerActions = new MyPlayerActions();

        myPlayerActions.Up.AddDefaultBinding(Key.UpArrow);
        myPlayerActions.Down.AddDefaultBinding(Key.DownArrow);
        myPlayerActions.Right.AddDefaultBinding(Key.RightArrow);
        myPlayerActions.Left.AddDefaultBinding(Key.LeftArrow);
        
        myPlayerActions.Up.AddDefaultBinding(Key.W);
        myPlayerActions.Down.AddDefaultBinding(Key.S);
        myPlayerActions.Right.AddDefaultBinding(Key.D);
        myPlayerActions.Left.AddDefaultBinding(Key.A);

        myPlayerActions.Gas.AddDefaultBinding(Key.Space);
        myPlayerActions.Fire.AddDefaultBinding(Key.Key1);
        myPlayerActions.Action.AddDefaultBinding(Key.Key3);
        myPlayerActions.Special.AddDefaultBinding(Key.Key2);

        myPlayerActions.LeftBumper.AddDefaultBinding(Key.S);
        myPlayerActions.RightBumper.AddDefaultBinding(Key.F);
        myPlayerActions.LeftTrigger.AddDefaultBinding(Key.G);
        myPlayerActions.RightTrigger.AddDefaultBinding(Key.W);

        myPlayerActions.Submit.AddDefaultBinding(Key.Return);
        myPlayerActions.Cancel.AddDefaultBinding(Key.Escape);

        myPlayerActions.Start.AddDefaultBinding(Key.Escape);
        
        //Mando
        myPlayerActions.Up.AddDefaultBinding(InputControlType.LeftStickUp);
        myPlayerActions.Right.AddDefaultBinding(InputControlType.LeftStickRight);
        myPlayerActions.Down.AddDefaultBinding(InputControlType.LeftStickDown);
        myPlayerActions.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
        
        myPlayerActions.Up.AddDefaultBinding(InputControlType.DPadUp);
        myPlayerActions.Right.AddDefaultBinding(InputControlType.DPadRight);
        myPlayerActions.Down.AddDefaultBinding(InputControlType.DPadDown);
        myPlayerActions.Left.AddDefaultBinding(InputControlType.DPadLeft);

        myPlayerActions.Gas.AddDefaultBinding(InputControlType.Action1);
        myPlayerActions.Fire.AddDefaultBinding(InputControlType.Action3);
        myPlayerActions.Action.AddDefaultBinding(InputControlType.Action2);
        myPlayerActions.Special.AddDefaultBinding(InputControlType.Action4);

        myPlayerActions.LeftBumper.AddDefaultBinding(InputControlType.LeftBumper);
        myPlayerActions.RightBumper.AddDefaultBinding(InputControlType.RightBumper);
        myPlayerActions.LeftTrigger.AddDefaultBinding(InputControlType.LeftTrigger);
        myPlayerActions.RightTrigger.AddDefaultBinding(InputControlType.RightTrigger);

        myPlayerActions.Submit.AddDefaultBinding(InputControlType.Action1);
        myPlayerActions.Cancel.AddDefaultBinding(InputControlType.Action2);
        
        myPlayerActions.Start.AddDefaultBinding(InputControlType.Command);

        return myPlayerActions;
    }
    
    public static MyPlayerActions Eduardo1()
    {
        var myPlayerActions = new MyPlayerActions();
        
        myPlayerActions.Right.AddDefaultBinding(Key.D);
        myPlayerActions.Left.AddDefaultBinding(Key.A);
        myPlayerActions.Fire.AddDefaultBinding(Key.S);
        myPlayerActions.RightTrigger.AddDefaultBinding(Key.W);
        myPlayerActions.Gas.AddDefaultBinding(Key.X);
        myPlayerActions.Submit.AddDefaultBinding(Key.X);

        return myPlayerActions;
    }

    public static MyPlayerActions Eduardo2()
    {
        var myPlayerActions = new MyPlayerActions();
        
        myPlayerActions.Right.AddDefaultBinding(Key.K);
        myPlayerActions.Left.AddDefaultBinding(Key.H);
        myPlayerActions.Fire.AddDefaultBinding(Key.J);
        myPlayerActions.RightTrigger.AddDefaultBinding(Key.U);
        myPlayerActions.Gas.AddDefaultBinding(Key.N);
        myPlayerActions.Submit.AddDefaultBinding(Key.N);

        return myPlayerActions;
    }
    
    public static MyPlayerActions Eduardo3()
    {
        var myPlayerActions = new MyPlayerActions();
        
        myPlayerActions.Right.AddDefaultBinding(Key.RightArrow);
        myPlayerActions.Left.AddDefaultBinding(Key.LeftArrow);
        myPlayerActions.Fire.AddDefaultBinding(Key.DownArrow);
        myPlayerActions.RightTrigger.AddDefaultBinding(Key.UpArrow);
        myPlayerActions.Gas.AddDefaultBinding(Key.M);

        return myPlayerActions;
    }
    
    public static MyPlayerActions Eduardo4()
    {
        var myPlayerActions = new MyPlayerActions();
        
        myPlayerActions.Right.AddDefaultBinding(Key.Pad6);
        myPlayerActions.Left.AddDefaultBinding(Key.Pad4);
        myPlayerActions.Fire.AddDefaultBinding(Key.Pad2);
        myPlayerActions.RightTrigger.AddDefaultBinding(Key.Pad8);
        myPlayerActions.Gas.AddDefaultBinding(Key.Pad0);

        return myPlayerActions;
    }
}
