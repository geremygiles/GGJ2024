using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerActionController
{
    /// <summary>
    /// Changes the player's input action map.
    /// </summary>
    /// <param name="newActionMapIndex">0 = Rhythm, 1 = Menu</param>
    public static void ChangePlayerState(int playerNum, int newActionMapIndex)
    {
        GameObject.Find("Player " + playerNum).GetComponent<PlayerMenuNavigation>().ChangePlayerState(newActionMapIndex);
    }
}
