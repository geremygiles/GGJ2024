using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManagerTester : MonoBehaviour
{
    void Start()
    {
        PointManager.RoundStart();
        PointManager.StartTurn(100, 1, 1);
        PointManager.SetRebuttleChoice(0);

        for (int i = 0; i < 50; i++)
        {
            PointManager.TileHit();
        }
        
        PointManager.RoundEnded();

        Debug.Log("1: " + PointManager.Player1TotalPoints);
        Debug.Log("2: " + PointManager.Player2TotalPoints);
    }
}
