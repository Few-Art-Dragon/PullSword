using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ISlider
{
    public delegate void MoveSliderHandler(float value);
    public event MoveSliderHandler OnMoveEvent;

    void MoveSlider();
}
