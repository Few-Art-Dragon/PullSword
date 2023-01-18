using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionCollection : ISelection
{
    private Camera _camera;

    public SelectionCollection(Camera camera)
    {
        _camera = camera;
    }

    public void Select()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit) &&
            raycastHit.transform.TryGetComponent(out MenuSelectionTarget target))
        {
            target.Select();
        }
    }

}
