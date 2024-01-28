using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ButtonHandling
{
    private static int playerNumber = -1;
    private static int categoryIndexNumber = -1;
    private static int jokeIndexNumber = -1;

    private static bool playerNumRecieved = false;
    private static bool categoryIndexNumRecieved = false;
    private static bool jokeNumRecieved = false;
    private static bool confirmed = false;

   public static void ReceivePlayerData(int playerNum)
    {
        playerNumber = playerNum;
        playerNumRecieved = true;

        if (categoryIndexNumRecieved)
        {
            SendData();
        }

        if (jokeNumRecieved)
        {
            Singleton.Instance.MenuController.StoreJokeObject(playerNumber, jokeIndexNumber);

            // Clear Data
            jokeIndexNumber = -1;
            playerNumber = -1;

            playerNumRecieved = false;
            jokeNumRecieved = false;
        }

        if (confirmed)
        {
            Singleton.Instance.MenuController.Confirm(playerNum);

            // Clear Data
            playerNumber = -1;

            playerNumRecieved = false;
            confirmed = false;
        }
    }

    public static void ReceiveIndexData(int indexNum) {
        categoryIndexNumber = indexNum;
        categoryIndexNumRecieved = true;

        if (playerNumRecieved)
        {
            SendData();
        }
    }

    private static void SendData()
    {
        // Send to MenuController
        Singleton.Instance.MenuController.ButtonClick(playerNumber, categoryIndexNumber);

        // Clear Data
        playerNumber = -1;
        categoryIndexNumber = -1;

        playerNumRecieved = false;
        categoryIndexNumRecieved = false;
    }

    public static void ReceiveJokeData(int index)
    {
        jokeIndexNumber = index;
        jokeNumRecieved = true;

        if (playerNumRecieved)
        {
            Singleton.Instance.MenuController.StoreJokeObject(playerNumber, jokeIndexNumber);

            // Clear Data
            jokeIndexNumber = -1;
            playerNumber = -1;

            playerNumRecieved = false;
            jokeNumRecieved = false;
        }
    }

    public static void Confirm()
    {
        confirmed = true; 

        if (playerNumRecieved)
        {
            Singleton.Instance.MenuController.Confirm(playerNumber);

            // Clear Data
            playerNumber = -1;

            playerNumRecieved = false;
            confirmed = false;
        }
    }

    public static void Cancel()
    {
        if (playerNumRecieved)
        {
            Singleton.Instance.MenuController.Back(playerNumber);

            // Clear Data
            playerNumber = -1;

            playerNumRecieved = false;
        }

        
    }
}
