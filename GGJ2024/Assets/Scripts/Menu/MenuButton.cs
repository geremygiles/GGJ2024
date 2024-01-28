using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    private void Start()
    {

    }

    public void ButtonClick(int index)
    {
        ButtonHandling.ReceiveIndexData(index);
    }
}
