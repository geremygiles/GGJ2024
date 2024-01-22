using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    [SerializeField] Text currentTextInputObject;
    private string currentOutputString;
    private int index = 0;
    private string textInputString;
    private string[] substrings;
    private int substringIndex;

    [SerializeField] private bool playing = true;

    [SerializeField] private GameObject UI_TextBox;
    [SerializeField] private Image UI_Border;
    [SerializeField] private Image UI_Box;
    [SerializeField] private TextMeshProUGUI UI_Text;


    // Start is called before the first frame update
    void Start()
    {
        ExtractString();
        StartCoroutine(TimedPrint());
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!playing) {
                IterateSubstring();
                playing = true;

                StartCoroutine(TimedPrint());
            }
            
            else
            {
                playing = false;
                currentOutputString = substrings[substringIndex];
                UpdateText();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    /// <summary>
    /// Pulls the string from the Text object
    /// </summary>
    private void ExtractString()
    {
        textInputString = currentTextInputObject.textString;

        substrings = textInputString.Split("\n\n");
    }

    private IEnumerator TimedPrint()
    {
        while (index < substrings[substringIndex].Length - 1 && playing)
        {
            AddCharacter();
            yield return new WaitForSeconds(0.1f / currentTextInputObject.charSpeed);
        }
    }

    /// <summary>
    /// Adds one character to the output string
    /// </summary>
    private void AddCharacter()
    {
        char currentChar = substrings[substringIndex][index];

        switch (currentChar)
        {
            case ' ':
                currentOutputString += substrings[substringIndex][index];
                index++;
                break;

            default:
                break;
        }

        currentOutputString += substrings[substringIndex][index];
        index++;
        

        UpdateText();
    }

    // Show updated text to the player
    private void UpdateText()
    {
        UI_Text.text = currentOutputString;
    }

    private void IterateSubstring()
    {
        substringIndex++;
        index = 0;

        // Clear current text
        currentOutputString = "";
        UpdateText();
    }

    /// <summary>
    /// Resets all variables
    /// </summary>
    private void Reset()
    {
        currentTextInputObject = null;
        currentOutputString = "";
        index = 0;
        textInputString = "";
        substringIndex = 0;
    }
}
