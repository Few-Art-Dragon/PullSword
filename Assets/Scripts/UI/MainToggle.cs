using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainToggle : MonoBehaviour, IToggle
{
    public event IToggle.ClickHandler OnClickEvent;

    public void OnClick()
    {
        OnClickEvent?.Invoke();
    }

   
}
