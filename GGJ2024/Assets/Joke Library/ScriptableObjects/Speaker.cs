using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Speaker")]
public class Speaker : ScriptableObject
{
    public string speakerName;
    public Sprite face;
    public bool leftAligned;
}
