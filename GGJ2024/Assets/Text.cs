using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Text Entry")]
public class Text : ScriptableObject
{
    public string speakerName;
    public Sprite face;
    [TextArea(5, 10)]
    public string textString;
    public float charSpeed = 2;
    public Text nextTextEntry;

    
    
}
