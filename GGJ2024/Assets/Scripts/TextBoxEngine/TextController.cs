using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Xml;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField] List<Text> textQueue = new List<Text>();
    [SerializeField] Text currentTextInputObject;
    private string currentOutputString;
    private int index = 0;
    private string textInputString;
    private string[] substrings;
    private int substringIndex;
    private float addtYield = 0;

    private bool open = false;

    [SerializeField] private float addtYieldValue = 0.5f;

    [SerializeField] private bool playing = false;

    [SerializeField] UIElements uiElements;

    /// <summary>
    /// Called by other classes to proceed the text
    /// </summary>
    public void ContinueText()
    {
        if (currentTextInputObject == null)
        {
            PlayNext();
            return;
        }

        if (!playing)
        {
            IterateSubstring();
        }

        else
        {
            playing = false;
            currentOutputString = substrings[substringIndex];
            UpdateText();
        }
    }

    /// <summary>
    /// Called by other classes to add a Text item
    /// </summary>
    /// <param name="text"></param>
    public void Enqueue(Text text)
    {
        textQueue.Add(text);
    }

    /// <summary>
    /// Called by other classes to add a Text item with a speaker
    /// </summary>
    /// <param name="text"></param>
    /// <param name="speaker"></param>
    public void Enqueue(Text text, Speaker speaker)
    {
        text.speaker = speaker;

        Enqueue(text);
    }
    
    /// <summary>
    /// Called interally to proceed
    /// </summary>
    private void PlayNext()
    {
        if (textQueue.Count == 0) return;

        Reset();

        if (!open) Open();

        // Grab text object
        currentTextInputObject = textQueue[0];

        // Remove from queue
        textQueue.RemoveAt(0);

        ExtractString();
        LoadSpeakerData();
        playing = true;

        StartCoroutine(TimedPrint());
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
        if (currentTextInputObject.speaker == null) return;

        if (!(currentTextInputObject.speaker.speakerName == ""))
        {
            uiElements.UIName_Text.text = currentTextInputObject.speaker.speakerName;
        }
        
        if (!(currentTextInputObject.speaker.face == null))
        {
            uiElements.UIFace_Face.sprite = currentTextInputObject.speaker.face;
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
        // Not at end of substrings
        if (substringIndex < substrings.Length - 1)
        { 
            substringIndex++;
            index = 0;

            // Clear current text
            currentOutputString = "";
            UpdateText();

            playing = true;

            StartCoroutine(TimedPrint());
        }
        
        else
        {
            Reset();
            UpdateText();

            // Items in the queue
            if (textQueue.Count > 0)
            {
                PlayNext();
            }
            else
            {
                currentTextInputObject = null;
                Close();
            }
        }

        
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

        open = false;
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

    /// <summary>
    /// Opens the text boxes.
    /// </summary>
    private void Open()
    {
        ShowTextBox();
        ShowFaceBox();
        ShowNameBox();

        open = true;
    }

    /// <summary>
    /// Hides the text box UI elements.
    /// </summary>
    private void ShowTextBox()
    {
        uiElements.UI_Border.enabled = true;
        uiElements.UI_Box.enabled = true;
        uiElements.UI_Text.enabled = true;
    }

    /// <summary>
    /// Hides the text box UI elements.
    /// </summary>
    private void ShowFaceBox()
    {
        uiElements.UIFace_Border.enabled = true;
        uiElements.UIFace_Box.enabled = true;
        uiElements.UIFace_Face.enabled = true;
    }

    private void ShowNameBox()
    {
        uiElements.UIName_Border.enabled = true;
        uiElements.UIName_Box.enabled = true;
        uiElements.UIName_Text.enabled = true;
    }
}
