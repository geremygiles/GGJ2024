using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Joke")]
public class Joke : ScriptableObject
{
    public string title;
    [TextArea(3,7)]
    public string joke;
}
