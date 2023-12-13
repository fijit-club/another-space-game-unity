using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private PlayingState playingState;

    public void PlayGame()
    {
        GameStateManager.ChangeState(playingState);
    }
}
