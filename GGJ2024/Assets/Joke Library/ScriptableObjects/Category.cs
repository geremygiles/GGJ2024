using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Category")]
public class Category : ScriptableObject
{
    public string categoryName;
    public Joke[] jokes = new Joke[3];
}
