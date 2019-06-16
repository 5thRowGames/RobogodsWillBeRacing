﻿using InControl;
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

    public PlayerAction Gas;
    public PlayerAction Fire;
    public PlayerAction Action;
    public PlayerAction Special;

    public PlayerAction LeftBumper;
    public PlayerAction RightBumper;
    public PlayerAction LeftTrigger;
    public PlayerAction RightTrigger;

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

        /*myPlayerActions.Up.AddDefaultBinding(Key.W);
        myPlayerActions.Down.AddDefaultBinding(Key.S);*/
        myPlayerActions.Right.AddDefaultBinding(Key.D);
        myPlayerActions.Left.AddDefaultBinding(Key.A);

        myPlayerActions.Gas.AddDefaultBinding(Key.Space);
        myPlayerActions.Fire.AddDefaultBinding(Key.S);
        myPlayerActions.Action.AddDefaultBinding(Key.F);
        myPlayerActions.Special.AddDefaultBinding(Key.G);

        myPlayerActions.LeftBumper.AddDefaultBinding(Key.Key1);
        myPlayerActions.RightBumper.AddDefaultBinding(Key.Key2);
        myPlayerActions.LeftTrigger.AddDefaultBinding(Key.Key3);
        myPlayerActions.RightTrigger.AddDefaultBinding(Key.W);

        myPlayerActions.Submit.AddDefaultBinding(Key.Return);
        myPlayerActions.Cancel.AddDefaultBinding(Key.RightAlt);

        return myPlayerActions;
    }
    
    //Solo de prueba
    public static MyPlayerActions BindBoth()
    {
        var myPlayerActions = new MyPlayerActions();

        /*myPlayerActions.Up.AddDefaultBinding(Key.W);
        myPlayerActions.Down.AddDefaultBinding(Key.S);*/
        myPlayerActions.Right.AddDefaultBinding(Key.D);
        myPlayerActions.Left.AddDefaultBinding(Key.A);

        myPlayerActions.Gas.AddDefaultBinding(Key.Space);
        myPlayerActions.Fire.AddDefaultBinding(Key.S);
        myPlayerActions.Action.AddDefaultBinding(Key.F);
        myPlayerActions.Special.AddDefaultBinding(Key.G);

        myPlayerActions.LeftBumper.AddDefaultBinding(Key.Key1);
        myPlayerActions.RightBumper.AddDefaultBinding(Key.Key2);
        myPlayerActions.LeftTrigger.AddDefaultBinding(Key.Key3);
        myPlayerActions.RightTrigger.AddDefaultBinding(Key.W);

        myPlayerActions.Submit.AddDefaultBinding(Key.Return);
        myPlayerActions.Cancel.AddDefaultBinding(Key.RightAlt);
        
        //Mando
        myPlayerActions.Up.AddDefaultBinding(InputControlType.LeftStickUp);
        myPlayerActions.Right.AddDefaultBinding(InputControlType.LeftStickRight);
        myPlayerActions.Down.AddDefaultBinding(InputControlType.LeftStickDown);
        myPlayerActions.Left.AddDefaultBinding(InputControlType.LeftStickLeft);

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
}
