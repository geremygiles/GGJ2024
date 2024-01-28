using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }
    public GameManager GameManager { get; private set; }
    public MenuController MenuController { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        GameManager = GetComponentInChildren<GameManager>();
        MenuController = GetComponentInChildren<MenuController>();
    }

}


    