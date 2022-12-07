using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

public class Sword : MonoBehaviour, IControllable
{
    public float powerUp => _powerUp;
    public float powerDown => _powerDown;
    public bool isLockIt { get; private set; }
    public bool highScore { get; private set; }

    [SerializeField]
    private float _speedRotate = 10f;
    [SerializeField]
    private float _powerUp;
    [SerializeField]
    private float _powerDown;
    [SerializeField]
    private int _idSword;
    private Vector3 _startSwordPosition;
    private Collider _colider;
    private Bounds _startBoundsSword;
    private float _targetBoundsYSword;
    private bool _iRotateSwordOnZeroRunnig;
    private bool _startShakeSwordRunnig;
    private bool _shakeNow;
    Side side;

    public void MoveUpSword(float powerUp)
    {
        transform.Translate(new Vector3(0f, powerUp, 0f), Space.World);
        CheckBoundsSword();
    }

    public void MoveDownSword(float powerDown)
    {
        if (transform.position.y > _startSwordPosition.y)
        {
            transform.Translate(new Vector3(0f, -powerDown, 0f) * Time.deltaTime, Space.World);
        }

        CheckBoundsSword();
    }

    public void ResetPositionSword()
    {
        transform.position = _startSwordPosition;
    }

    private void ShakeSword()
    {
        if (_shakeNow)
        {
            switch (side)
            {
                case Side.Right:
                    transform.Rotate(new Vector3(0, 0, _speedRotate * Time.deltaTime));
                    break;

                case Side.Left:
                    transform.Rotate(new Vector3(0, 0, -_speedRotate * Time.deltaTime));
                    break;
            }
        }
    }

    private void StopIRotateSwordOnZero()
    {
        _iRotateSwordOnZeroRunnig = false;
        StopCoroutine("IRotateSwordOnZero");
    }

    private void CheckBoundsSword()
    {
        _targetBoundsYSword = (_startBoundsSword.center.y + _startBoundsSword.max.y) / 2;

        if (_colider.bounds.center.y >= _targetBoundsYSword)
        {
            if (!_startShakeSwordRunnig)
            {
                _startShakeSwordRunnig = true;
                StartShakeSword();
            }
        }
        else
        {
            if (!_iRotateSwordOnZeroRunnig)
            {
                StartCoroutine("IRotateSwordOnZero");
                _iRotateSwordOnZeroRunnig = true;
            }
            StopShakeSword();
            _startShakeSwordRunnig = false;
        }
    }

    private void StartShakeSword()
    {
        _shakeNow = true;
        StartCoroutine("ISwapSide");
    }

    private void StopShakeSword()
    {
        _shakeNow = false;
        StopCoroutine("ISwapSide");
    }

    private void OnEnable()
    {
        SwordController.OnLastClickEvent += StopShakeSword;
        isLockIt = true;
    }

    private void Start()
    {
        _colider = GetComponent<Collider>();
        _startBoundsSword = _colider.bounds;
        _startSwordPosition = transform.position;
        ResetPositionSword();
    }

    private void Update()
    {
        ShakeSword();
    }

    private void OnDisable()
    {
        SwordController.OnLastClickEvent -= StopShakeSword;
    }

    IEnumerator ISwapSide()
    {
        while (true)
        {
            side = (side == Side.Left) ? (Side.Right) : (Side.Left);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator IRotateSwordOnZero()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            if (transform.eulerAngles.z != 0.0f)
            {
                transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(transform.eulerAngles.z, 0, 1));
            }
            else if (transform.eulerAngles.z == 0.0f)
            {
                StopIRotateSwordOnZero();
            }
        }
    }
}

enum Side : byte
{
    Left,
    Right
}
