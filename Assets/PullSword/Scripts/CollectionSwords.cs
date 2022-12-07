using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionSwords : MonoBehaviour
{
    public delegate void SpawnSwordOnCollectionHandler(GameObject sword);
    public static event SpawnSwordOnCollectionHandler OnSpawnSwordOnCollectionEvent;

    

    [SerializeField]
    private List<GameObject> _swords = new List<GameObject>();
    private GameObject _currentActiveSword;
    private int _indexSword;


    private void Start()
    {
        _indexSword = 0;
        OnSpawnSwordOnCollectionEvent?.Invoke(_swords[_indexSword]);
    }

    private void SetActiveSword(bool state)
    {
        _currentActiveSword?.SetActive(state);
    }

    private void CheckSwordOnLock()
    {
        for (int i = _indexSword; i < _swords.Count; i++)
        {
            if (_swords[i].GetComponent<Sword>().isLockIt == false)
            {
                _currentActiveSword = _swords[i];
                //SetActiveSword(false);
                //SetActiveSword(true);
                OnSpawnSwordOnCollectionEvent?.Invoke(_swords[_indexSword]);
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
