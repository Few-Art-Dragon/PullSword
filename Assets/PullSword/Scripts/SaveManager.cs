using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private float _volumeMainMusic;
    private bool _switchMainMusic;

    private void SerializeDate()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Save.dat");
        SaveDate data = new SaveDate();
        data.VolumeMainMusic = _volumeMainMusic;
        data.SwitchMainMusic = _switchMainMusic;
        formatter.Serialize(file, data);
        file.Close();
    }

    private void DerializeDate()
    {
        if (File.Exists(Application.persistentDataPath + "/Save.dat"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Save.dat", FileMode.Open);
            SaveDate data = (SaveDate)formatter.Deserialize(file);
            file.Close();
            _volumeMainMusic = data.VolumeMainMusic;
            _switchMainMusic = data.SwitchMainMusic;
        }
    }

    private void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.S))
        {
            SerializeDate();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            DerializeDate();
        }
    }
}

[Serializable]
class SaveDate
{
    public float VolumeMainMusic;
    public bool SwitchMainMusic;
}
