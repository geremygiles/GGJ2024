using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMenuNavigation : MonoBehaviour
{
    [SerializeField] private int playerNum;
    MenuController menuController;
    PlayerInput playerInput;
    int activeActionMap = 0;
    private void Awake()
    {
        menuController = GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>();
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
        menuController.Back(playerNum);
    }

    private void OnConfirm()
    {
        //Debug.Log("PlayerNum: " + playerNum);
        ButtonHandling.ReceivePlayerData(playerNum);
    }

    private void OnHeckle()
    {
        Debug.Log("Heckle!");
    }
}
