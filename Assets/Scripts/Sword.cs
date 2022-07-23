using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Sword : MonoBehaviour
{
    private Collider _colider;
    private Bounds _startBoundsSword;
    private float _targetBoundsYSword;

    public static UnityEvent<int> SetHighScoreEvent = new UnityEvent<int>();

    public static UnityEvent StartShakeSwordEvent = new UnityEvent();
    public static UnityEvent StopShakeSwordEvent = new UnityEvent();

    public static UnityEvent CheckBoundsEvent = new UnityEvent();

    private bool _IRotateSwordOnZeroRunnig;
    private bool _startShakeSwordRunnig;

    enum Side : byte
    {
        Left,
        Right
    }

    Side side;

    private bool _shakeNow;

    [SerializeField] private float _speedResetRotate = 0.8f;

    [SerializeField] private float _speedRotate = 10f;
    private uint _id;
    [SerializeField] private float _powerUp;
    [SerializeField] private float _powerDown;

    [SerializeField] private int _highScore;

    [SerializeField]private bool _isLock;

    private void Start()
    {
        SetStandartParam();
    }

    private void Update()
    {
        
        //Debug.Log("Center " + _colider.bounds.center);
        //Debug.Log("Extents " + _colider.bounds.extents);
        //Debug.Log("Size " + _colider.bounds.size);
        //Debug.Log("Min " + _colider.bounds.min);
        //Debug.Log("Max " + _colider.bounds.max);
        ShakeSword();
    }

    private void SetStandartParam()
    {
        StartShakeSwordEvent.AddListener(StartShakeSword);
        StopShakeSwordEvent.AddListener(StopShakeSword);
        SetHighScoreEvent.AddListener(SetHighScore);
        _colider = GetComponent<Collider>();
        _startBoundsSword = _colider.bounds;
        CheckBoundsEvent.AddListener(CheckBoundsSword);
    }

    public float GetPowerUp
    {
        get { return _powerUp; }
    }

    public float GetPowerDown
    {
        get { return _powerDown; }
    }

    public bool GetIsLock
    {
        get { return _isLock; }
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
                    transform.Rotate(new Vector3(0, 0, - _speedRotate * Time.deltaTime));
                    break;
            }
        }

    }

    public int GetHighScore
    {
        get { return _highScore; }
    }

    public void SetHighScore(int value)
    {
        if (_highScore > value && value != 0 || _highScore == 0)
        {
            _highScore = value;
        }
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
               // transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(transform.eulerAngles.z, 0, _speedResetRotate));
            }
            else if (transform.eulerAngles.z == 0.0f)
            {
                StopIRotateSwordOnZero();
            }

        }
    }
    private void StopIRotateSwordOnZero()
    {
        _IRotateSwordOnZeroRunnig = false;
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
            if (!_IRotateSwordOnZeroRunnig)
            {
                StartCoroutine("IRotateSwordOnZero");
                _IRotateSwordOnZeroRunnig = true;
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

}
