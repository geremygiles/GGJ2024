using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    private void Start()
    {

    }

    public void JokeButtonClick(int index)
    {
        ButtonHandling.ReceiveJokeData(index);
    }

    public void CategoryButtonClick(int index)
    {
        ButtonHandling.ReceiveIndexData(index);
    }

    public void ConfirmButtonClick()
    {
        ButtonHandling.Confirm();
    }

    public void CancelButtonClick()
    {
        ButtonHandling.Cancel();
    }
}
