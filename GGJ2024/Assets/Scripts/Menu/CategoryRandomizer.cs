using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryRandomizer : MonoBehaviour
{
    [SerializeField] private List<Category> allCategories;

    public List<Category> GenerateCategories(int categoryCount)
    {
        List<Category> tempAllCategories = allCategories;
        var categories = new List<Category>();

        while (categories.Count < categoryCount)
        {
            Debug.Log("catergories.Count: " + categories.Count);
            Debug.Log("categoryCount: " + categoryCount);
            var randomInt = Random.Range(0, tempAllCategories.Count - 1);
            Debug.Log(randomInt.ToString());


            var category = tempAllCategories[randomInt];
            tempAllCategories.Remove(category);
            categories.Add(category);
        }

        return categories;
    }
}
