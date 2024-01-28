using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private int playerNum;
    PlayerInput playerInput;
    int activeActionMap = 0;
    private void Awake()
    {
        //menuController = GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>();
        playerInput = GetComponent<PlayerInput>();
    }


    /// <summary>
    /// Changes the player's input action map.
    /// </summary>
    /// <param name="actionMapIndex">0 = Rhythm, 1 = Menu</param>
    public void ChangePlayerState(int actionMapIndex)
    {
        playerInput.actions.actionMaps[activeActionMap].Disable();
        activeActionMap = actionMapIndex;
        playerInput.actions.actionMaps[activeActionMap].Enable();
    }

    private void OnBack()
    {
        Singleton.Instance.MenuController.Back(playerNum);
    }

    private void OnSubmit()
    {
        ButtonHandling.ReceivePlayerData(playerNum);
    }

    private void OnHeckle()
    {
        Debug.Log("Heckle!");
    }

    private void OnStartMenu()
    {
        Singleton.Instance.GameManager.StartJokeSelectionPhase();
    }
}
