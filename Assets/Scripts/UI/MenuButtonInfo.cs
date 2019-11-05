using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonInfo : MonoBehaviour
{
    public Sprite acceptKeyboard;
    public Sprite returnKeyboard;
    public Sprite moveKeyboard;
    public Sprite acceptXbox;
    public Sprite returnXbox;
    public Sprite moveXbox;

    public List<Image> acceptButton;
    public List<Image> returnButton;
    public List<Image> moveButton;

    private bool isKeyboard;

    private void Start()
    {
        isKeyboard = false;
    }

    private void Update()
    {
        InputDevice device = InputManager.ActiveDevice;
        
        if (!isKeyboard && Input.anyKeyDown)
        {
            ChangeToKeyboard();
            isKeyboard = true;
        }
        else if (isKeyboard && device.AnyButton.WasPressed)
        {
            ChangeToXbox();
            isKeyboard = false;
        }
    }

    private void ChangeToXbox()
    {
        foreach (var button in acceptButton)
        {
            button.sprite = acceptXbox;
        }
        
        foreach (var button in returnButton)
        {
            button.sprite = returnXbox;
        }
        
        foreach (var button in moveButton)
        {
            button.sprite = moveXbox;
        }
    }
    
    private void ChangeToKeyboard()
    {
        foreach (var button in acceptButton)
        {
            button.sprite = acceptKeyboard;
        }
        
        foreach (var button in returnButton)
        {
            button.sprite = returnKeyboard;
        }
        
        foreach (var button in moveButton)
        {
            button.sprite = moveKeyboard;
        }
    }
    
}
