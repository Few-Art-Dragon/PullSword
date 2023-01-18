using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelection : MonoBehaviour
{
    private Camera _camera;
    private ISelection _selection;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(_selection);
            _selection?.Select();
        }
    }

    public void SelectCollection()
    {
        _selection = new SelectionCollection(_camera);
    }

    public void SelectEmpty()
    {
        _selection = null;
    }

}
