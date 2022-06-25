using System;
using UnityEngine;
using UnityEngine.Events;


public class Controller : MonoBehaviour
{
    //Swipe

    private Vector2 _tapPosition;
    private Vector2 _swipeDelta;

    private float _deadZone = 80;

    private bool _isSwiping;
    private bool _isMobile;

    private Sword sword;

    [SerializeField] private Score score;


    private Vector3 _startPos;

    private TimeSpan _nowTimeTouch;

    private bool _isPlaying = true;
    private bool _isFirstClick = true;

    private UnityEvent AddScoreEvent = new UnityEvent();

    private void Start()
    {
        SetStandartParam();
    }

    private void Update()
    {
        ShakeSword();
        CheckIntervalBetweenTouches();
        GetTouchPosition();
    }

    private void OnMouseDown()
    {
        if (_isPlaying == true)
        {
            _nowTimeTouch = DateTime.Now.TimeOfDay;
            if (_isFirstClick == true)
            {
                _isFirstClick = false;
                AddScoreEvent.Invoke();
            }
            MoveUpSword();
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
        AddScoreEvent.RemoveListener(score.StartAddScore);
        AddScoreEvent.AddListener(score.StopAddScore);
        _isPlaying = false;
        AddScoreEvent.Invoke();
    }

    private void CheckIntervalBetweenTouches()
    {
        if (_isPlaying == true & _startPos.y < transform.position.y)
        {
            TimeSpan intervalTime;
            intervalTime = DateTime.Now.TimeOfDay - _nowTimeTouch;
            if (intervalTime.TotalMilliseconds > 1000f | intervalTime.TotalMilliseconds == 0f)
            {
                MoveDownSword();
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
        AddScoreEvent.AddListener(score.StartAddScore);
    }

    private void MoveUpSword()
    {
        transform.Translate(new Vector3(0f, sword.GetPowerUp, 0f), Space.World);
    }

    private void MoveDownSword()
    {
        transform.Translate(new Vector3(0f, -sword.GetPowerDown, 0f) * Time.deltaTime, Space.World);
    }

    private void ShakeSword()
    {
        // transform.rotation = (new Vector3(Mathf.Lerp(180, 90, 1), 0f, 0f),Quaternion.identity);
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
}
