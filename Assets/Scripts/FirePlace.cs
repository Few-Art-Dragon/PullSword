using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlace : MonoBehaviour
{
    [SerializeField] private GameObject _fireLight;
    [SerializeField] private GameObject _fireSmoke;

    private void OnMouseDown()
    {
        SwitchFirePlace();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SwitchFirePlace()
    {
        _fireLight.SetActive(_fireLight.activeSelf ? false : true );
        _fireSmoke.SetActive(_fireSmoke.activeSelf ? false : true );
        GameManager.ChangeDayAndNightEvent.Invoke();
    }
}
