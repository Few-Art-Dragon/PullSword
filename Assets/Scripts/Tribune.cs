using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tribune : MonoBehaviour
{
    [SerializeField]
    private float _speedRotate;

    private void Update()
    {
        RotateTribune();
    }

    private void RotateTribune()
    {
        transform.Rotate(new Vector3(0, _speedRotate * Time.deltaTime, 0));
    }
}
