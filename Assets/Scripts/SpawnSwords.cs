using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSwords : MonoBehaviour
{
    public delegate void GetScoreHandler(int num);
    public static event GetScoreHandler OnGetScoreEvent;
    [SerializeField]
    private float _offsetYPosition;
    [SerializeField]
    private Transform _stonePosition;
    [SerializeField]
    private Transform _tribunePosition;
    [SerializeField]
    private float _speedMoveDown;
    private GameObject _currentSword;

    private void OnEnable()
    {     
        CollectionSwords.OnSpawnSwordOnCollectionEvent += GetSwordFromCollection;
        
        
    }

    private void Start()
    {
        GameManager.OnSetPlayStateEvent += SpawnSwordOnStone;
        GameManager.OnSetCollectionStateEvent += SpawnSwordOnCollection;
    }

    private void OnDisable()
    {
        CollectionSwords.OnSpawnSwordOnCollectionEvent -= GetSwordFromCollection;
        GameManager.OnSetPlayStateEvent -= SpawnSwordOnStone;
        GameManager.OnSetCollectionStateEvent -= SpawnSwordOnCollection;
    }

    private void GetSwordFromCollection(GameObject sword)
    {
        SetActiveSword(false);
        _currentSword = sword;
        //OnGetScoreEvent?.Invoke(sword.GetComponent<ScoreSword>().highScore);
    }

    private void SetSwordPosition(Transform transform)
    {
        _currentSword.transform.position = new Vector3(transform.position.x, transform.localPosition.y, transform.position.z);
        SetActiveSword(true);
        MoveSwordToTargetPosition();
    }

    private void SetActiveSword(bool state)
    {
        _currentSword?.SetActive(state);
    }

    private void MoveSwordToTargetPosition()
    {
        StartCoroutine("IMoveSword", _tribunePosition);
    }

    private void FinishMoveSwordToTargetposition()
    {
        StopCoroutine("IMoveSword");
    }

    private void SpawnSwordOnStone()
    {
        
        SetSwordPosition(_stonePosition);
        _currentSword.GetComponent<SwordController>().enabled = true;
    }

    private void SpawnSwordOnCollection()
    {
        _currentSword.GetComponent<SwordController>().enabled = false;
        SetSwordPosition(_tribunePosition);
    }

    IEnumerator IMoveSword(Transform targetTransform)
    {
        while(_currentSword.transform.position.y > targetTransform.position.y)
        {
            _currentSword.transform.position = new Vector3(_currentSword.transform.position.x, Mathf.Lerp(_currentSword.transform.position.y, targetTransform.position.y, _speedMoveDown), _currentSword.transform.position.z);
            yield return null;        
        }
        FinishMoveSwordToTargetposition();
    }
}
