using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionSwords : MonoBehaviour
{
    public delegate void SpawnSwordOnCollectionHandler(GameObject sword);
    public static event SpawnSwordOnCollectionHandler OnSpawnSwordOnCollectionEvent;

    

    [SerializeField]
    private List<GameObject> _swords = new List<GameObject>();
    private GameObject _currentActiveSwordOnCollection;
    private GameObject _currentActiveSwordOnStone;
    private int _indexSword;

    private void OnEnable()
    {
        
        GameManager.OnSetPlayStateEvent += GetRandomSwordForSpawnOnStone;
    }

    private void Start()
    {
        CheckSwordOnLock();
        _indexSword = 0;
    }

    private void GetRandomSwordForSpawnOnStone()
    {
        _currentActiveSwordOnStone = _swords[Random.Range(0, _swords.Count)];
        OnSpawnSwordOnCollectionEvent?.Invoke(_currentActiveSwordOnStone);
    }

    private void CheckSwordOnLock()
    {
        for (int i = _indexSword; i < _swords.Count; i++)
        {
            if (_swords[i].GetComponent<Sword>().isLockIt == false)
            {
                _currentActiveSwordOnCollection = _swords[i];
                OnSpawnSwordOnCollectionEvent?.Invoke(_currentActiveSwordOnCollection);
                break;
            }
        }
    }

    public void SwitchNextSword()
    {
        _indexSword++;
        CheckIndexSword();        
    }

    public void SwitchBackSword()
    {
        _indexSword--;
        CheckIndexSword();
    }

    private void CheckIndexSword()
    {
        if (_indexSword == -1)
        {
            _indexSword = _swords.Count - 1;
        }
        else if (_indexSword == _swords.Count)
        {
            _indexSword = 0;
        }
        CheckSwordOnLock();
    }
}
