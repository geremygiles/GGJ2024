using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIElements
{
    public GameObject UI_TextBox;
    public Image UI_Border;
    public Image UI_Box;
    public TextMeshProUGUI UI_Text;

    public GameObject UI_FaceBox;
    public Image UIFace_Border;
    public Image UIFace_Box;
    public Image UIFace_Face;
    public Image UIName_Border;
    public Image UIName_Box;
    public TextMeshProUGUI UIName_Text;
}

public class TextController : MonoBehaviour
{
    [SerializeField] Text currentTextInputObject;
    private string currentOutputString;
    private int index = 0;
    private string textInputString;
    private string[] substrings;
    private int substringIndex;
    private float addtYield = 0;

    [SerializeField] private float addtYieldValue = 0.5f;

    [SerializeField] private bool playing = true;

    [SerializeField] UIElements uiElements;


    // Start is called before the first frame update
    void OnEnable()
    {
        ExtractString();
        LoadSpeakerData();
        StartCoroutine(TimedPrint());
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!playing) {
                IterateSubstring();
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

        for (int i = 0; i < substrings.Length; i++)
        {
            substrings[i] = substrings[i].Trim();
        }
    }


    private void LoadSpeakerData()
    {
        if (!(currentTextInputObject.speakerName == ""))
        {
            uiElements.UIName_Text.text = currentTextInputObject.speakerName;
        }
        
        if (!(currentTextInputObject.face == null))
        {
            uiElements.UIFace_Face.sprite = currentTextInputObject.face;
        }
        
    }

    private IEnumerator TimedPrint()
    {
        while (index < substrings[substringIndex].Length && playing)
        {
            AddCharacter();
            yield return new WaitForSeconds(0.1f / currentTextInputObject.charSpeed + addtYield);
            addtYield = 0;
        }
        playing = false;
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

            case '.':
            case '!':
            case '?':
                if (!(index == substrings[substringIndex].Length - 1))
                {
                    addtYield = addtYieldValue;
                }
                break;

            case ',':
                addtYield = addtYieldValue/8;
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
        uiElements.UI_Text.text = currentOutputString;
    }

    private void IterateSubstring()
    {
        if (substringIndex < substrings.Length - 1)
        { 
            substringIndex++;
            index = 0;
        }
        
        else
        {
            Reset();
            if (currentTextInputObject.nextTextEntry != null)
            {
                currentTextInputObject = currentTextInputObject.nextTextEntry;
                ExtractString();
                LoadSpeakerData();
            }
            else
            {
                Close();
            }
        }

        // Clear current text
        currentOutputString = "";
        UpdateText();

        playing = true;

        StartCoroutine(TimedPrint());
    }

    /// <summary>
    /// Resets all variables
    /// </summary>
    private void Reset()
    {
        currentOutputString = "";
        index = 0;
        textInputString = "";
        substringIndex = 0;
    }

    /// <summary>
    /// Closes the text boxes.
    /// </summary>
    private void Close()
    {
        HideTextBox();
        HideFaceBox();
        HideNameBox();
    }

    /// <summary>
    /// Hides the text box UI elements.
    /// </summary>
    private void HideTextBox()
    {
        uiElements.UI_Border.enabled = false;
        uiElements.UI_Box.enabled = false;
        uiElements.UI_Text.enabled = false;
    }

    /// <summary>
    /// Hides the text box UI elements.
    /// </summary>
    private void HideFaceBox()
    {
        uiElements.UIFace_Border.enabled = false;
        uiElements.UIFace_Box.enabled = false;
        uiElements.UIFace_Face.enabled = false;
    }

    private void HideNameBox()
    {
        uiElements.UIName_Border.enabled = false;
        uiElements.UIName_Box.enabled = false;
        uiElements.UIName_Text.enabled = false;
    }
}
