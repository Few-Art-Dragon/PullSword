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
    [SerializeField]
    VectorMove vectorMove;

    private Vector3 _startVector;
    private Vector3 _vectorMove;

    [Range(1, 10)]
    [SerializeField]
    private float _speed;
    [Range(1, 10)]
    [SerializeField]
    private float _minSpeed;
    [Range(1, 10)]
    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private bool _isRandomSpeed;

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
    private void RandomPosition()
    {
        transform.position = new Vector3(_startVector.x, Random.Range(_startVector.y - 3f, _startVector.y + 3f), _startVector.z);
        _startVector = transform.position;
    }

    private void RandomSpeed()
    {
        if (_isRandomSpeed)
        {
            _speed = Random.Range(_minSpeed, _maxSpeed);
        }
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
}
