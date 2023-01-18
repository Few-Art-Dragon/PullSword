using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainSlider : MonoBehaviour, ISlider
{
    public event ISlider.MoveSliderHandler OnMoveEvent;
    private Slider _slider;

    public void MoveSlider()
    {
        OnMoveEvent?.Invoke(_slider.value);
    }
    private void Start()
    {
        _slider = GetComponent<Slider>();
    }
}
