using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Canvas player1MenuCanvas;
    [SerializeField] private Canvas player2MenuCanvas;
    [SerializeField] private GameObject categoryMenuPrefab;
    [SerializeField] private GameObject jokeMenuPrefab;

    [SerializeField] private MultiplayerEventSystem player1EventSystem;
    [SerializeField] private MultiplayerEventSystem player2EventSystem;

    private GameObject player1CategoriesMenu;
    private CategoryMenu player1CatMenu;
    private GameObject player2CategoriesMenu;
    private CategoryMenu player2CatMenu;
    private GameObject player1JokesMenu;
    private GameObject player2JokesMenu;

    private bool player1CategoriesOpen = false;
    private bool player2CategoriesOpen = false;
    private bool player1JokesOpen = false;
    private bool player2JokesOpen = false;

    private List<Category> player1Categories = new List<Category>();

    // Start is called before the first frame update
    void Start()
    {
        OpenCategoryMenu(1);
        OpenCategoryMenu(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Opens the category menus for both players
    /// </summary>
    public void OpenCategoryMenu(int playerNum)
    {
        if (playerNum == 1)
        {
            // Player 1
            AddCategories(player1Categories, 2);

            for (int i = 0; i < player1Categories.Count; i++)
            {
                player1CatMenu.buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = player1Categories[i].categoryName;
            }

            player1CategoriesMenu = Instantiate(categoryMenuPrefab, player1MenuCanvas.transform, false);

            player1CatMenu = player1CategoriesMenu.GetComponent<CategoryMenu>();

            player1EventSystem.SetSelectedGameObject(player1CatMenu.buttons[0].gameObject);

            player1CategoriesOpen = true;
        }
        
        if (playerNum == 2)
        {
            // Player 2
            player2CategoriesMenu = Instantiate(categoryMenuPrefab, player2MenuCanvas.transform, false);

            player2CatMenu = player2CategoriesMenu.GetComponent<CategoryMenu>();

            // Moving menu
            RectTransform rectTransform = player2CategoriesMenu.GetComponent<RectTransform>();
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.anchorMin = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(1, 1);

            rectTransform.anchoredPosition = new Vector2(-50, -50);

            player2EventSystem.SetSelectedGameObject(player2CatMenu.buttons[0].gameObject);

            player2CategoriesOpen = true;
        }
        

    }

    private void OpenJokeMenu(int playerNum)
    {
        // Player 1
        if (playerNum == 1 && !player1JokesOpen) {
            player1JokesMenu = Instantiate(jokeMenuPrefab, player1MenuCanvas.transform, false);
            player1EventSystem.SetSelectedGameObject(player1JokesMenu.GetComponent<CategoryMenu>().buttons[0].gameObject);

            player1CatMenu.shade.SetActive(true);
            player1JokesOpen = true;
        }

        // Player 2
        else if (playerNum == 2 && !player2JokesOpen)
        {
            player2JokesMenu = Instantiate(jokeMenuPrefab, player2MenuCanvas.transform, false);

            // Moving menu
            RectTransform rectTransform = player2JokesMenu.GetComponent<RectTransform>();
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.anchorMin = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(1, 1);

            rectTransform.anchoredPosition = new Vector2(-150, -208);

            player2EventSystem.SetSelectedGameObject(player2JokesMenu.GetComponent<CategoryMenu>().buttons[0].gameObject);

            player2CatMenu.shade.SetActive(true);

            player2JokesOpen = true;
        }
    }

    public void ButtonClick()
    {
        OpenJokeMenu(1);
    }

    private void CloseJokes(int playerNum)
    {
        if (playerNum == 1)
        {
            Destroy(player1JokesMenu);
            player1EventSystem.SetSelectedGameObject(player1CatMenu.buttons[0].gameObject);

            player1CatMenu.shade.SetActive(false);
            player1JokesOpen = false;
        }
        if (playerNum == 2)
        {
            Destroy(player2JokesMenu);
            player2EventSystem.SetSelectedGameObject(player2CatMenu.buttons[0].gameObject);

            player2CatMenu.shade.SetActive(false);
            player2JokesOpen = false;
        }
    }

    private void CloseCategories(int playerNum)
    {
        if (playerNum == 1)
        {
            Destroy(player1CategoriesMenu);

            player1MenuCanvas.GetComponent<PlayerCanvas>().shade.SetActive(false);
            player1CategoriesOpen = false;
        }
        if (playerNum == 2)
        {
            Destroy(player2CategoriesMenu);

            player2MenuCanvas.GetComponent<PlayerCanvas>().shade.SetActive(false);
            player2CategoriesOpen = false;
        }
    }

    public void Back(int playerNum)
    {
        if (playerNum == 1) {
            if (player1JokesOpen)
            {
                CloseJokes(1);
            }
            else if (player1CategoriesOpen)
            {
                CloseCategories(1);
            }
        }

        if (playerNum == 2)
        {
            if (player2JokesOpen)
            {
                CloseJokes(2);
            }
            else if (player2CategoriesOpen)
            {
                CloseCategories(2);
            }
        }
    }

    private void AddCategories(List<Category> categoryList, int categoryCount)
    {
        categoryList = GetComponent<CategoryRandomizer>().GenerateCategories(categoryCount);

    }
}
