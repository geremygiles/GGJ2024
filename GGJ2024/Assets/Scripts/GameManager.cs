using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
}
