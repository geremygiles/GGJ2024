using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Text Entry")]
public class Text : ScriptableObject
{
    public Speaker speaker;
    [TextArea(5, 10)]
    public string textString;
    public float charSpeed = 2;
}
