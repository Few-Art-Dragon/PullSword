using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IToggle
{
    public delegate void ClickHandler();
    public event ClickHandler OnClickEvent;
    void OnClick();
}
