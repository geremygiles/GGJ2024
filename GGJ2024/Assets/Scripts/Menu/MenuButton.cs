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

    public void ButtonClick(int index)
    {
        Debug.Log("Supplying Index: " + index);
        ButtonHandling.ReceiveIndexData(index);
        //menuController.ButtonClick(index);
    }
}
