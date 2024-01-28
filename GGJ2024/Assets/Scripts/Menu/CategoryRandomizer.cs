using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryRandomizer : MonoBehaviour
{
    [SerializeField] private List<Category> allCategories;

    public List<Category> GenerateCategories(int categoryCount)
    {
        List<Category> tempAllCategories = new List<Category>(allCategories);
        
        var categories = new List<Category>();

        while (categories.Count < categoryCount)
        {
            var randomInt = Random.Range(0, tempAllCategories.Count - 1);

            var category = tempAllCategories[randomInt];
            tempAllCategories.Remove(category);
            categories.Add(category);
        }

        return categories;
    }
}
