using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;


public class IncontrolProvider : MonoBehaviour, IDevice
{
    public MyPlayerActions myPlayerActions = new MyPlayerActions();

    public enum ControlType
    {
        None = 1,
        Gamepad = 2,
        Keyboard = 3
    };

    public ControlType controlType;
    
    private string meta;
    public string Meta
    {
        get => meta;
        set => meta = value;
    }

    Guid guid = new Guid();
    public Guid Id { get { return guid; } }

    private InputDevice inputDevice;

    public InputDevice InputDevice
    {
        get => inputDevice;
        set
        {
            if(myPlayerActions != null)
                myPlayerActions.Device = value;
            else
                Debug.Log("Estoy seteando un inputDevice teniendo los playerActions a null");
            
            inputDevice = value;
        }
    }
    
    private IPlayer iPlayer;
    public IPlayer Owner { get => iPlayer; set => iPlayer = value;}

    private string deviceName;
    public string DeviceName { get => deviceName; set => deviceName = value; }

    private List<IControllable> slaves = new List<IControllable>();
    public List<IControllable> Slaves { get => slaves; set => slaves = value; }

    private DeviceState state;
    public DeviceState State { get => state; private set => state = value; }
    DeviceState previousState;

    private void Awake()
    {
        controlType = ControlType.None;
    }

    void Update ()
    {
        UpdateState();
        ControlSlaves();
    }

    //Lleva el control de todos los botones o teclas que pulsamos, actualizando sus estados y valores
    public void UpdateState()
    {
        previousState = State;
        State = new DeviceState();

        UpdateAxis(AxisType.Vertical, myPlayerActions.Vertical);
        UpdateAxis(AxisType.Horizontal, myPlayerActions.Horizontal);
        UpdateAxis(AxisType.Jump, myPlayerActions.Gas);
        UpdateAxis(AxisType.Action, myPlayerActions.Action);
        UpdateAxis(AxisType.Special, myPlayerActions.Special);
        UpdateAxis(AxisType.Fire, myPlayerActions.Fire);
        UpdateAxis(AxisType.LeftTrigger, myPlayerActions.LeftTrigger);
        UpdateAxis(AxisType.RightTrigger, myPlayerActions.RightTrigger);
        UpdateAxis(AxisType.LeftBumper, myPlayerActions.LeftBumper);
        UpdateAxis(AxisType.RightBumper, myPlayerActions.RightBumper);
        UpdateAxis(AxisType.Cancel, myPlayerActions.Cancel);
        UpdateAxis(AxisType.Submit, myPlayerActions.Submit);
        
    }

    private void UpdateAxis(AxisType axisType, float newValue)
    {
        AxisState previousAxisState = new AxisState(0, InputState.Idle);
        if (previousState != null && previousState.axisList.ContainsKey(axisType))
        {
            previousAxisState = previousState.axisList[axisType];
        }
        AxisState newAxisState = new AxisState(newValue, InputState.Idle);
        newAxisState.SetInputState(GetNewInputState(previousAxisState, newAxisState));
        State.axisList.Add(axisType, newAxisState);
    }

    public void ControlSlaves()
    {
        foreach (var slave in Slaves)
        {
            slave.Control(this);
        }
    }

    public void AddSlave(IControllable controllable)
    {
        Slaves.Add(controllable);
    }

    float OneKeyCodedAxis(KeyCode positive)
    {
        float result = 0;
        if (Input.GetKey(positive))
        {
            result = 1f;
        }
        else
        {
            result = 1f;
        }
        return result;
    }

    float TwoKeyCodedAxis(KeyCode positive, KeyCode negative)
    {
        float result = 0;
        if (Input.GetKey(positive))
        {
            result += 1f;
        }
        if (Input.GetKey(negative))
        {
            result -= 1f;
        }
        return result;
    }

    InputState GetNewInputState(AxisState previousState, AxisState currentState)
    {
        InputState result = InputState.Undefined;
        if (previousState.Value == 0 && currentState.Value != 0)
        {
            result = InputState.Pressed;
        }
        else if (previousState.Value != 0 && currentState.Value == 0)
        {
            result = InputState.Released;
        }
        else if (previousState.Value != 0 && currentState.Value != 0)
        {
            result = InputState.Held;
        }
        else
        {
            result = InputState.Idle;
        }
        return result;
    }
}
