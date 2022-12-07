using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuSelectionTarget : MonoBehaviour
{
    public UnityEvent OnSelect = new UnityEvent();

    public void Select()
    {
        OnSelect?.Invoke();
    }
}
