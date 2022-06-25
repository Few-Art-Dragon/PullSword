using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    private uint _id;
    [SerializeField]private float _powerUp;
    [SerializeField]private float _powerDown;

    public float GetPowerUp
    {
        get { return _powerUp; }
    }

    public float GetPowerDown
    {
        get { return _powerDown; }
    }

}
