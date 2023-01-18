using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateNight : IStateDay
{
    public Color ColorDirectionLight { get; }
    public Color ColorSettingsLight { get; }

    public StateNight()
    {
        ColorDirectionLight = new Color32(34,34,34,0);
        ColorSettingsLight = Color.black;
    }

    public void SetStateDay(Light sun)
    {
        RenderSettings.ambientLight = ColorSettingsLight;
        sun.color = ColorDirectionLight;
    }
}
