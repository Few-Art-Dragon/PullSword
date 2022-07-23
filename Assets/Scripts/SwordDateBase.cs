using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwordDateBase : MonoBehaviour
{
    public static UnityEvent PutSwordOnTribuneEvent = new UnityEvent();

    public static UnityEvent PutSwordInStoneEvent = new UnityEvent();

    private readonly Vector3 _positionStone = new Vector3(0.78f, -0.71f, 1.194f);
    private readonly Vector3 _rotationStone = new Vector3(0f,0f,0f);
    private readonly Vector3 _positionTribune = new Vector3(-14.689f, 0.4855798f, -20.13664f);
    private readonly Vector3 _rotationTribune = new Vector3(0f, 0f, 0f);


    [SerializeField] private GameObject[] _swords;

    private GameObject _activeSwordInStone;
    private GameObject _prevActiveSwordInStone;

    private GameObject _activeSwordOnTribune;
    private GameObject _prevActiveSwordOnTribune;

    private void Awake()
    {
        SetStandartParam();
    }


    private void SetStandartParam()
    {
        PutSwordInStone();
        PutSwordInStoneEvent.AddListener(PutSwordInStone);
        PutSwordOnTribuneEvent.AddListener(PutSwordOnTribune);
    }

    private void PutSwordInStone()
    {
        foreach (var sword in _swords)
        {
            if (sword.GetComponent<Sword>().GetIsLock)
            {
                if (_prevActiveSwordInStone == null)
                {
                    _prevActiveSwordInStone = sword;
                }
                else 
                {
                    _prevActiveSwordInStone.SetActive(false);
                    _prevActiveSwordInStone = _activeSwordInStone;
                }
                _activeSwordInStone = sword;
                sword.transform.position = _positionStone;
                sword.transform.eulerAngles = _rotationStone;
                _activeSwordInStone.SetActive(true);
                break;
            }
        }
    }

    private void PutSwordOnTribune()
    {
        foreach (var sword in _swords)
        {
            if (!sword.GetComponent<Sword>().GetIsLock)
            {
                if (_prevActiveSwordOnTribune == null)
                {
                    _prevActiveSwordOnTribune = sword;
                }
                else
                {
                    _prevActiveSwordOnTribune.GetComponent<Tribune>().enabled = false;
                    _prevActiveSwordOnTribune.SetActive(false);
                    _prevActiveSwordOnTribune = _activeSwordOnTribune;
                }
                _activeSwordOnTribune = sword;
                sword.transform.position = _positionTribune;
                sword.transform.eulerAngles = _rotationTribune;
                
                _activeSwordOnTribune.GetComponent<Tribune>().enabled = true;
                _activeSwordOnTribune.SetActive(true);
                break;
            }
        }
    }

    private void HideSwordOnTribune()
    {
        if(_activeSwordOnTribune != null)
        {
            _activeSwordOnTribune.GetComponent<Tribune>().enabled = false;
            _activeSwordOnTribune.SetActive(false);
        }
        
    }

}
