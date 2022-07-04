using UnityEngine;
using UnityEngine.Events;

public class Sword : MonoBehaviour
{
    public static UnityEvent<int> SetHighScoreEvent = new UnityEvent<int>();
    enum Side : byte
    {
        Center,
        Left,
        Right
    }

    Side side;

    [SerializeField] private float _speedRotate;
    private uint _id;
    [SerializeField] private float _powerUp;
    [SerializeField] private float _powerDown;

    [SerializeField] private Vector3 _startRotate;
    [SerializeField] private Vector3 _targetRotate;

   [SerializeField] private int _highScore;

    private bool _isLock;

    private void Start()
    {
        _startRotate = transform.eulerAngles.normalized;
        SetHighScoreEvent.AddListener(SetHighScore);
    }

    private void Update()
    {
        //ShakeSword();

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
        //if (side == Side.Left)
        //{
        if (transform.rotation.x > 150f && transform.rotation.x < 220f)
        {

        }
        transform.Rotate(Vector3.right * _speedRotate * Time.deltaTime);
        //new Vector3(Mathf.Lerp(_startRotate, _startRotate + 30f, 1 * Time.deltaTime), 0, 0);
        //}

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



}
