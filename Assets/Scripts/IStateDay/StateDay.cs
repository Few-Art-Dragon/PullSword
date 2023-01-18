using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDay : IStateDay
{
    public Color ColorDirectionLight { get;}
    public Color ColorSettingsLight { get;}

    public StateDay()
    {
        ColorDirectionLight = new Color32(195, 195, 195 ,0);
        ColorSettingsLight = Color.white;
    }

    public void SetStateDay(Light sun)
    {
        RenderSettings.ambientLight = ColorSettingsLight;
        sun.color = ColorDirectionLight;
    }


}
