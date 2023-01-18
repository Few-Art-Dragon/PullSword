using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable
{
    float powerUp { get;}
    float powerDown { get; }
    void MoveUpSword(float powerUp);
    void MoveDownSword(float powerDown);
    void ResetPositionSword();
    
}
