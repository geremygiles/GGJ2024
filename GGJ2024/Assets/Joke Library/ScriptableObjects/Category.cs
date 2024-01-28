using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Category")]
public class Category : ScriptableObject
{
    public Joke[] jokes = new Joke[3];
}
