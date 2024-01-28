using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManagerTester : MonoBehaviour
{
    void Start()
    {
        PointManager.RoundStart();
        PointManager.StartTurn(100, 1, 1);
    }
}
