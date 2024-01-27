using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    MenuController menuController;

    private void Start()
    {
        menuController = GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>();
    }

    public void ButtonClick()
    {
        menuController.ButtonClick();
    }
}
