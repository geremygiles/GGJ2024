using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuNavigation : MonoBehaviour
{
    [SerializeField] private int playerNum;
    MenuController menuController;
    private void Awake()
    {
        menuController = GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>();
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
}
