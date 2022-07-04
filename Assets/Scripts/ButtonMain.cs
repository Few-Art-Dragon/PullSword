using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMain : MonoBehaviour
{
    public bool IsActiveButton;
    public byte Id;
    
    public Image _imageButton;
    public Sprite _buttonIsActive;
    public Sprite _buttonIsNotActive;

    private void Start()
    {
        SetStandartParam();
    }


    public void SetActiveButton()
    {
        IsActiveButton = true;
        _imageButton.sprite = _buttonIsActive;
        ButtonsManager.ChangeSpritesButtonEvent.Invoke(Id);
    }



    private void SetStandartParam()
    {
        _imageButton = GetComponent<Image>();
    }
}
