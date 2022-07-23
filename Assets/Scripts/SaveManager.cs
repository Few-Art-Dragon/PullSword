using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveManager : MonoBehaviour
{
    Cell cell;
    private void Start()
    {
        cell = new Cell();
        string json = JsonUtility.ToJson(cell);
        //cell = JsonUtility.FromJson<Cell>(json);
    }




}

[Serializable]
    public class Cell
{
    public uint Id;
    public bool IsLock;

}
