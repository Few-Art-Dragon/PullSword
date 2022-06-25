using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum VectorMove : byte
{
    Left,
    Right
}

public class Cloud : MonoBehaviour
{
    private Vector3 _startVector;

    [Range(1, 10)]
    [SerializeField]
    private float _speed;

    [Range(1, 10)]
    [SerializeField]
    private float _minSpeed, _maxSpeed;

    [SerializeField]
    private bool _isRandomSpeed;

    private Vector3 _vectorMove;

    [SerializeField]
    VectorMove vectorMove;


    private void Start()
    {
        _startVector = transform.position;
        CheckVectorMove();
    }

    private void Update()
    {
        Move(_vectorMove);
    }

    private void OnTriggerEnter(Collider other)
    {
        ChangeVector();
        CheckVectorMove();
    }

    private void Move(Vector3 vector)
    {
        transform.Translate(vector * _speed * Time.deltaTime ,Space.World);
    }

    private void CheckVectorMove()
    {
        //RandomPosition();
        RandomSpeed();
        if (vectorMove == 0)
        {
            _vectorMove = Vector3.right;
        }
        else
        {
            _vectorMove = Vector3.left;
        }
    }

   

    private void RandomSpeed()
    {
        if (_isRandomSpeed)
        {
            _speed = Random.Range(_minSpeed, _maxSpeed);
        }
    }

    private void RandomPosition()
    {
        transform.position = new Vector3(_startVector.x, Random.Range(_startVector.y-3f, _startVector.y + 3f), _startVector.z);
        _startVector = transform.position;
    }

    private void ChangeVector()
    {
        if (vectorMove == 0)
        {
            vectorMove = (VectorMove)1;
        }
        else
        {
            vectorMove = 0;
        }
    }

    
}
