using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;



public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _swords;
    private float _volumeMainMusic;
    private bool _switchMainMusic;
    private Dictionary<int, int> _infoSwords = new Dictionary<int, int>();

    private void Start()
    {
        DerializeDate();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            DerializeDate();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SerializeDate();
        }
    }

    private void GetIdAndHighscore()
    {
        for (int i = 0; i < _swords.Length; i++)
        {
            _infoSwords.Add(_swords[i].GetComponent<Sword>().GetIdSword(), _swords[i].GetComponent<ScoreSword>().highScore);
        }
    }

    private void SerializeDate()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Save.dat");
        SaveDate data = new SaveDate();
        GetIdAndHighscore();
        data.volumeMainMusic = _volumeMainMusic;
        data.switchMainMusic = _switchMainMusic;
        data.infoSwords = _infoSwords;
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
            _volumeMainMusic = data.volumeMainMusic;
            _switchMainMusic = data.switchMainMusic;
            _infoSwords = data.infoSwords;

            foreach(var sword in _infoSwords)
            {
                Debug.Log(sword.Key + " " + sword.Value);
            }
        }
    }
}

[Serializable]
class SaveDate
{
    public float volumeMainMusic;
    public bool switchMainMusic;

    public Dictionary<int, int> infoSwords;
}
