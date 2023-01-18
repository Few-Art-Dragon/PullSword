using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateDay
{
    Color ColorDirectionLight { get;}
    Color ColorSettingsLight { get;}
    void SetStateDay(Light sun);
}
