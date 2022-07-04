using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //Lighting
    public static UnityEvent ChangeDayAndNightEvent = new UnityEvent();
    
    [SerializeField] private Color _colorDirectionLightDay;
    [SerializeField] private Color _colorDirectionLightNight;
    [SerializeField] private Color _colorSettingsLightDay;
    [SerializeField] private Color _colorSettingsLightNight;
    [SerializeField] private Light _sun;


    



    [SerializeField] private GameObject _scoreGameObject;
    public static UnityEvent SetGameOverEvent = new UnityEvent();
    public static UnityEvent SetGameEvent = new UnityEvent();
    public static UnityEvent SetMainMenuEvent = new UnityEvent();

    public enum GameMode:byte
    {
        MainMenu,
        Game,
        GameOver
    }
    public static GameMode gameMode;

    private void Start()
    {
        SetStandartParam();
    }


    private void Update()
    {
        
        //Debug.Log(gameMode);
    }

    private void SetStandartParam()
    {
        ChangeDayAndNightEvent.AddListener(SwitchDayAndNight);
        SetGameOverEvent.AddListener(SetModeGameOver);
        SetGameEvent.AddListener(SetModeGame);
        SetMainMenuEvent.AddListener(SetModeMainMenu);
        gameMode = GameMode.MainMenu;

    }
    private void SetModeGame()
    {
        SetVisibleScore(true);
        gameMode = GameMode.Game;
    }

    private void SetModeGameOver()
    {
        gameMode = GameMode.GameOver;
    }

    private void SetModeMainMenu()
    {
        Score.FinishAddScoreEvent.Invoke(false);
        Controller.ResetPositionSwordEvent.Invoke();
        SetVisibleScore(false);
        gameMode = GameMode.MainMenu;
    }

    private void SetVisibleScore(bool active)
    {
        _scoreGameObject.SetActive(active);
    }

    private void SwitchDayAndNight()
    {
        RenderSettings.ambientLight = RenderSettings.ambientLight == _colorSettingsLightNight ?  _colorSettingsLightDay : _colorSettingsLightNight;
        _sun.color = _sun.color == _colorDirectionLightNight ? _colorDirectionLightDay : _colorDirectionLightNight;
    }

}
