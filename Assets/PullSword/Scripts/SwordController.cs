using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class SwordController : MonoBehaviour
{
    public delegate void FirstClickHandler();
    public static event FirstClickHandler OnFirstClickEvent;
    public delegate void LastClickHandler();
    public static event LastClickHandler OnLastClickEvent;

    private IControllable _sword;
    private TimeSpan _nowTimeTouch;

    private Vector2 _tapPosition;
    private Vector2 _swipeDelta;

    private float _deadZone = 80;

    private bool _isSwiping;
    private bool _isMobile;

    [SerializeField]
    private int _targetCounterForSwipe;
    private int _counterForSwipe;

    private bool _isPlaying = true;
    private bool _isFirstClick;

    private void CheckIntervalBetweenTouches()
    {
        if (CheckGameMode(GameManager.GameState.Game) /*& _isPlaying == true & _startPos.y < transform.position.y*/)
        {
            TimeSpan intervalTime;
            intervalTime = DateTime.Now.TimeOfDay - _nowTimeTouch;
            if (intervalTime.TotalMilliseconds > 1000f | intervalTime.TotalMilliseconds == 0f)
            {
                _sword.MoveDownSword(_sword.powerDown);
            }
        }
    }

    private void GetTouchPosition()
    {
        if (!_isMobile)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isSwiping = true;
                _tapPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ResetSwipe();
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _isSwiping = true;
                    _tapPosition = Input.GetTouch(0).position;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Canceled
                    || Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    ResetSwipe();
                }
            }
        }
        CheckSwipe();
    }

    private void CheckSwipe()
    {
        if (_isSwiping)
        {
            if (!_isMobile && Input.GetMouseButton(0))
                _swipeDelta = (Vector2)Input.mousePosition - _tapPosition;
            else if (Input.touchCount > 0)
                _swipeDelta = Input.GetTouch(0).position - _tapPosition;
        }

        if (_swipeDelta.magnitude > _deadZone)
        {
            Debug.Log(" X: " + Mathf.Abs(_swipeDelta.x) + " Y: " + Mathf.Abs(_swipeDelta.y));
            if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
            {
                if (_swipeDelta.x > 0)
                {

                    Debug.Log("Right");
                }
                else
                {
                    Debug.Log("Left");
                }
            }
            else
            {
                if (_swipeDelta.y > 0)
                {
                    if (CheckCounterForSwipe())
                    {
                        _sword.MoveUpSword(_sword.powerUp * 5f);
                        ResetCounterForSwipe();
                    }
                    Debug.Log("Up");
                }
                else
                {
                    Debug.Log("Down");
                }
            }
            ResetSwipe();
        }
    }

    private void ResetSwipe()
    {
        _isSwiping = false;
        _tapPosition = Vector2.zero;
        _swipeDelta = Vector2.zero;
    }

    private bool CheckGameMode(GameManager.GameState gameMode)
    {
        if (GameManager.gameState == gameMode)
        {
            return true;
        }
        return false;
    }

    private void ResetCounterForSwipe()
    {
        _counterForSwipe = 0;
    }

    private void AddCounterForSwipe()
    {
        _counterForSwipe++;
    }

    private bool CheckCounterForSwipe()
    {
        if (_counterForSwipe >= _targetCounterForSwipe)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnEnable()
    {
        _isFirstClick = true;
    }

    private void Start()
    {
        _isMobile = Application.isMobilePlatform;
        _sword = GetComponent<IControllable>();

        _counterForSwipe = 0;
    }

    private void Update()
    {
        CheckIntervalBetweenTouches();
        GetTouchPosition();
    }

    private void OnMouseDown()
    {
        //if (CheckGameMode(GameManager.GameState.Game) /*&& _isPlaying == true*/)
        //{
            _nowTimeTouch = DateTime.Now.TimeOfDay;
            if (_isFirstClick == true)
            {
                _isFirstClick = false;
                OnFirstClickEvent?.Invoke();
            }
            _sword.MoveUpSword(_sword.powerUp);
            AddCounterForSwipe();
        //}
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (gameObject.GetComponent<BoxCollider>().bounds.center.y - other.bounds.center.y >= 1.5)
    //    {

    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        OnLastClickEvent?.Invoke();
    }
}
