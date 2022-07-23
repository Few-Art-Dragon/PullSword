using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class Controller : MonoBehaviour
{
    public static UnityEvent ResetPositionSwordEvent = new UnityEvent();
    //Swipe

    private Vector2 _tapPosition;
    private Vector2 _swipeDelta;

    private float _deadZone = 80;

    private bool _isSwiping;
    private bool _isMobile;

    private int _counterForSwipe;

    private Sword sword;

    [SerializeField] private Score score;


    private Vector3 _startPos;

    private TimeSpan _nowTimeTouch;

    private bool _isPlaying = true;
    private bool _isFirstClick = true;

    

    private void Start()
    {
        SetStandartParam();
    }

    private void Update()
    {
        if (GameManager.gameMode == GameManager.GameMode.Game)
        {
            CheckIntervalBetweenTouches();
            GetTouchPosition();
        } 
    }

    private void OnMouseDown()
    {
        if (CheckGameMode(GameManager.GameMode.Game) && _isPlaying == true)
        {
            _nowTimeTouch = DateTime.Now.TimeOfDay;
            if (_isFirstClick == true)
            {
                _isFirstClick = false;
                Score.AddScoreEvent.Invoke();
            }
            MoveUpSword();
            AddCounterForSwipe();
            Sword.CheckBoundsEvent.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (gameObject.GetComponent<BoxCollider>().bounds.center.y - other.bounds.center.y >= 1.5)
        {

        }

    }

    private void OnTriggerExit(Collider other)
    {
        _isPlaying = false;
        Score.FinishAddScoreEvent.Invoke(true);
        GameManager.SetGameOverEvent.Invoke();
    }

    private void CheckIntervalBetweenTouches()
    {
        if (CheckGameMode(GameManager.gameMode) & _isPlaying == true & _startPos.y < transform.position.y)
        {
            TimeSpan intervalTime;
            intervalTime = DateTime.Now.TimeOfDay - _nowTimeTouch;
            if (intervalTime.TotalMilliseconds > 1000f | intervalTime.TotalMilliseconds == 0f)
            {
                MoveDownSword();
                Sword.CheckBoundsEvent.Invoke();
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

    private void SetStandartParam()
    {
        _isMobile = Application.isMobilePlatform;
        sword = GetComponent<Sword>();
        _startPos = transform.position;
        ResetPositionSwordEvent.AddListener(ResetPositionSword);
        _counterForSwipe = 0;
    }

    private void MoveUpSword()
    {
        transform.Translate(new Vector3(0f, sword.GetPowerUp, 0f), Space.World);
    }

    private void MoveDownSword()
    {
        transform.Translate(new Vector3(0f, -sword.GetPowerDown, 0f) * Time.deltaTime, Space.World);
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
            Debug.Log(" X: " +Mathf.Abs(_swipeDelta.x) + " Y: " + Mathf.Abs(_swipeDelta.y));
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
                        MoveUpSword();
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

    private bool CheckGameMode(GameManager.GameMode gameMode)
    {
        if (GameManager.gameMode == gameMode)
        {
            return true;
        }
        return false;
    }

    private void ResetPositionSword()
    {
        _isFirstClick = true;
        gameObject.transform.position = _startPos;
    }

    private void AddCounterForSwipe()
    {
        _counterForSwipe++;
    }

    private void ResetCounterForSwipe()
    {
        _counterForSwipe = 0;
    }

    private bool CheckCounterForSwipe()
    {
        if (_counterForSwipe >= 10)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
