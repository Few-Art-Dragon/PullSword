using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Events;
using System.Diagnostics;

public class GameManager : MonoBehaviour
{
    public delegate void SetMainMenuStateHandler();
    public static event SetMainMenuStateHandler OnSetMainMenuStateEvent;

    public delegate void SetCollectionStateHandler();
    public static event SetCollectionStateHandler OnSetCollectionStateEvent;

    public delegate void SetSettingsStateHandler();
    public static event SetSettingsStateHandler OnSetSettingsStateEvent;

    public delegate void SetPlayStateHandler();
    public static event SetPlayStateHandler OnSetPlayStateEvent;

    public static UnityEvent<IStateDay> OnChangeDayAndNightEvent = new UnityEvent<IStateDay>();

    [SerializeField]
    private Light _sun;

    public static GameState gameState;

    private IStateDay stateDay;

    private void OnEnable()
    {
        MainMenu.OnGetGameStateEvent += SetGameState;
        OnChangeDayAndNightEvent.AddListener(SwitchDayAndNight);
    }

    private void OnDisable()
    {
        MainMenu.OnGetGameStateEvent -= SetGameState;
        OnChangeDayAndNightEvent.RemoveListener(SwitchDayAndNight);
    }

    private void SetGameState(GameState state)
    {
        gameState = state;
        CheckGameState();
    }

    public void CheckGameState()
    {
        switch (gameState)
        {
            case GameState.Game:
                OnSetPlayStateEvent?.Invoke();
                break;
            case GameState.MainMenu:
                OnSetMainMenuStateEvent?.Invoke();
                break;
            case GameState.Collection:
                OnSetCollectionStateEvent?.Invoke();
                break;
            case GameState.Settings:
                OnSetSettingsStateEvent?.Invoke();
                break;
            case GameState.GameOver:
                
                break;
        }
    }

    private void SwitchDayAndNight(IStateDay stateDay)
    {
        this.stateDay = stateDay;
        this.stateDay.SetStateDay(_sun);
    }

    public enum GameState : byte
    {
        Game,
        MainMenu,
        Collection,
        Settings,
        GameOver
    }
}
