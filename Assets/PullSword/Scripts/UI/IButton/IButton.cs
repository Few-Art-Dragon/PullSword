using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IButton 
{
    public delegate void ClickHandler(IButton button);
    public event ClickHandler OnClickEvent;

    
    void OnClick();
    void SetSpriteActiveButton();

    void SetSpriteNotActiveButton();
}
