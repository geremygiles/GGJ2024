using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text Entry")]
public class Text : ScriptableObject
{
    [TextArea(5, 10)]
    public string textString;
    public float charSpeed;

    
}
