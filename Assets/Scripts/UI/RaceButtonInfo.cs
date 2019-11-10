using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;
using UnityEngine.UI;

public class RaceButtonInfo : MonoBehaviour
{
    public Sprite returnButtonXbox;
    public Sprite moveButtonXbox;
    public Sprite acceptButtonXbox;

    public Sprite returnButtonKeyboard;
    public Sprite moveButtonKeyboard;
    public Sprite acceptButtonKeyboard;

    public Image acceptButton;
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
        acceptButton.sprite = acceptButtonXbox;
        
        foreach (var button in returnButton)
        {
            button.sprite = returnButtonXbox;
        }
        
        foreach (var button in moveButton)
        {
            button.sprite = moveButtonXbox;
        }
    }
    
    private void ChangeToKeyboard()
    {
        acceptButton.sprite = acceptButtonKeyboard;
        
        foreach (var button in returnButton)
        {
            button.sprite = returnButtonKeyboard;
        }
        
        foreach (var button in moveButton)
        {
            button.sprite = moveButtonKeyboard;
        }
    }


}
