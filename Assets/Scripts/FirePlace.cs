using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlace : MonoBehaviour
{
    [SerializeField]
    private GameObject _fireLight;
    [SerializeField]
    private GameObject _fireSmoke;

    private void SwitchFirePlace()
    {
        if (CheckActiveFirePlace())
        {
            SetActiveFirePlace(false);
            GameManager.OnChangeDayAndNightEvent.Invoke(new StateDay());    
        }
        else 
        {
            SetActiveFirePlace(true);
            GameManager.OnChangeDayAndNightEvent.Invoke(new StateNight());
        } 
    }

    private bool CheckActiveFirePlace()
    {
        if (_fireLight.activeSelf & _fireSmoke.activeSelf)
            return true;

        return false;
    }

    private void SetActiveFirePlace(bool check)
    {
        _fireLight.SetActive(check);
        _fireSmoke.SetActive(check);
    }

    private void OnMouseDown()
    {
        SwitchFirePlace();
    }
}
