using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ButtonsManager : MonoBehaviour
{
    public static UnityEvent<byte> ChangeSpritesButtonEvent = new UnityEvent<byte>();

    [SerializeField] private ButtonMain[] _buttons;

    private void Start()
    {
        SetStandartParam();
    }

    private void SetStandartParam()
    {
        ChangeSpritesButtonEvent.AddListener(ChangeSpritesButton);
       // _buttons = new Button[4];
    }

    private void ChangeSpritesButton(byte id)
    {
        foreach (var button in _buttons)
        {
            if (id != button.Id)
            {
                button.IsActiveButton = false;
                button._imageButton.sprite = button._buttonIsNotActive;
            }
        }
    }
    
}
