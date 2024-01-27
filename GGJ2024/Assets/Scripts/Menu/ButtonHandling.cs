using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ButtonHandling
{
    private static int playerNumber = -1;
    private static int indexNumber = -1;

    private static bool playerNumRecieved = false;
    private static bool indexNumRecieved = false;

   public static void ReceivePlayerData(int playerNum)
    {
        playerNumber = playerNum;
        playerNumRecieved = true;

        if (indexNumRecieved)
        {
            SendData();
        }
    }

    public static void ReceiveIndexData(int indexNum) {
        indexNumber = indexNum;
        indexNumRecieved = true;

        if (playerNumRecieved)
        {
            SendData();
        }
    }

    private static void SendData()
    {
        Debug.Log("Data Sending: Player = " + playerNumber + ", Index = " + indexNumber);
        // Send to MenuController
        GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>().ButtonClick(playerNumber, indexNumber);

        // Clear Data
        playerNumber = -1;
        indexNumber = -1;

        playerNumRecieved = false;
        indexNumRecieved = false;
}
}
