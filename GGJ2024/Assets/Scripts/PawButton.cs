using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PawButton : MonoBehaviour
{
    // Button Stuff:
    private float buttonHeldThreshold = 0.2f;
    //private float buttonPressPauseThreshold = 1f;
    private float buttonTimer = 0f;
    private float pauseTimer = 0;

    private bool canPress = true;
    private bool tileInBox = false;
    private bool isPressed = false;
    

    private float[] defaultAlphas = new float[2];
    private Component[] images = new Component[2];

    [SerializeField]
    KeyCode key;

    void Start()
    {
        images = this.GetComponentsInChildren<Image>();
        int index = 0;
        foreach (Image image in images)
        {
            defaultAlphas[index] = image.color.a;
            index++;
        }
    }

    void FixedUpdate()
    {
        // Honest to God I need to refactor the shit out of this
        if(Input.GetKey(key) && buttonTimer < buttonHeldThreshold && canPress)
        {   
            ButtonPress();     
            buttonTimer += Time.deltaTime;
        }
        else if(buttonTimer >= buttonHeldThreshold)
        {
            canPress = false;
            ButtonRelease();
            buttonTimer = 0; 
        }
        else if(!Input.GetKey(key))
        {
            canPress = true;
            ButtonRelease();  
            buttonTimer = 0; 
        }

        if(tileInBox && canPress && isPressed && pauseTimer == 0)
        {
            Destroy(tileToKill);
            RhythmGame.instance.AddScore(1);
        }
    }

    private void ButtonPress()
    {
        images = this.GetComponentsInChildren<Image>();
        foreach(Image image in images)
        {
            var temp = image.color;
            temp.a = 1f;
            image.color = temp;
        }

        isPressed = true;
    }

    private void ButtonRelease()
    {
        images = this.GetComponentsInChildren<Image>();
        foreach(Image image in images)
        {
            var temp = image.color;
            temp.a = defaultAlphas[0];
            image.color = temp;
        }

        isPressed = false;
    }

    // Collision Handling:
    private GameObject tileToKill;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("PawTile"))
        {
            tileToKill = other.gameObject;
            tileInBox = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("PawTile"))
        {
            tileToKill = null;
            tileInBox = false;
        }
    }
}
