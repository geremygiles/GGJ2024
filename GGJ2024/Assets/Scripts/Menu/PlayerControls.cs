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

    [SerializeField] PawButton[] pawButtons;

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
        // TEMP FIX FOR A BUG
        playerInput = GetComponent<PlayerInput>();
        //

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

    private void OnProceed()
    {
        if (Singleton.Instance.MenuController.player1Ready == Singleton.Instance.MenuController.player2Ready)
        {
            Singleton.Instance.TextController.ContinueText();
        }
    }

    private void OnRhythm1()
    {
        pawButtons[0].buttonPressed = true;
    }

    private void OnRhythm2()
    {
        pawButtons[1].buttonPressed = true;
    }

    private void OnRhythm3()
    {
        pawButtons[2].buttonPressed = true;
    }

    private void OnRhythm4()
    {
        pawButtons[3].buttonPressed = true;
    }
}
