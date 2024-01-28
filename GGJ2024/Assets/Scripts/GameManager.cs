using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool inRound = false;
    private bool player1Turn = false;
    private bool player2Turn = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Starts the joke selection phase
    /// </summary>
    public void StartJokeSelectionPhase()
    {
        Singleton.Instance.MenuController.enabled = true;
    }

    public void StartRound()
    {
        PointManager.RoundStart();
        inRound = true;
        player1Turn = true;

        // call rhythm sequence for player 1
    }

    // NEEDS TO BE CALLED BY THE RHYTHM GAME WHEN THE PLAYER IS DONE
    public void TurnEnded()
    {
        if (inRound && player1Turn)
        {
            player1Turn = false;
            player2Turn = true;

            // call next rhythm sequence for player 2
        }
        else if (inRound && player2Turn)
        {
            player2Turn = false;
            inRound = false;
            PointManager.RoundEnded();
        }
        else if (!inRound) 
        {
            StartJokeSelectionPhase();
        }
    }

    public void PlayerWins(int playerNum)
    {
        if (playerNum == 1)
        {
            // display win screen for player 1
            UnityEngine.Debug.Log("Player 1 WINS!");
        }
        else
        {
            // display win screen for player 2
            UnityEngine.Debug.Log("Player 2 WINS!");
        }

        PointManager.Restart();
        StartJokeSelectionPhase();
    }


}
