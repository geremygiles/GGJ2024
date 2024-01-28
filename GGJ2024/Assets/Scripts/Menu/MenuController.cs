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

    private GameObject player1CategoriesObject;
    private CategoryMenu player1CategoryMenu;
    private GameObject player2CategoriesObject;
    private CategoryMenu player2CategoryMenu;
    private GameObject player1JokesObject;
    private CategoryMenu player1JokeMenu;
    private GameObject player2JokesObject;
    private CategoryMenu player2JokeMenu;

    private bool player1CategoriesOpen = false;
    private bool player2CategoriesOpen = false;
    private bool player1JokesOpen = false;
    private bool player2JokesOpen = false;

    private List<Category> player1Categories = new List<Category>();
    private List<Category> player2Categories = new List<Category>();

    private Joke[] player1Jokes;
    private Joke[] player2Jokes;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        OpenCategoryMenu(1);
        OpenCategoryMenu(2);
        OpenShade();
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

            // Randomize Categories
            player1Categories = SetRandCategories(5);
            Debug.Log(player1Categories.Count);

            // Show Menu
            player1CategoriesObject = Instantiate(categoryMenuPrefab, player1MenuCanvas.transform, false);

            player1CategoryMenu = player1CategoriesObject.GetComponent<CategoryMenu>();

            // Show Data
            for (int i = 0; i < player1Categories.Count; i++)
            {
                player1CategoryMenu.buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = player1Categories[i].categoryName;
            }

            // Set selected item to first
            player1EventSystem.SetSelectedGameObject(player1CategoryMenu.buttons[0].gameObject);

            player1CategoriesOpen = true;

            // Set new controls
            PlayerActionController.ChangePlayerState(1, 1);
        }
        
        if (playerNum == 2)
        {
            // Player 2

            // Randomize Categories
            player2Categories = SetRandCategories(5);
            Debug.Log(player1Categories.Count);

            // Show Menu
            player2CategoriesObject = Instantiate(categoryMenuPrefab, player2MenuCanvas.transform, false);

            player2CategoryMenu = player2CategoriesObject.GetComponent<CategoryMenu>();

            // Moving menu
            RectTransform rectTransform = player2CategoriesObject.GetComponent<RectTransform>();
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.anchorMin = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(1, 1);

            rectTransform.anchoredPosition = new Vector2(-50, -50);

            // Show Data
            for (int i = 0; i < player2Categories.Count; i++)
            {
                player2CategoryMenu.buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = player2Categories[i].categoryName;
            }

            // Set selected item to first
            player2EventSystem.SetSelectedGameObject(player2CategoryMenu.buttons[0].gameObject);

            player2CategoriesOpen = true;

            // Set new controls
            PlayerActionController.ChangePlayerState(2, 1);
        }


    }

    private void OpenJokeMenu(int playerNum, int index)
    {
        // Player 1
        if (playerNum == 1 && !player1JokesOpen) {

            // Show Menu
            player1JokesObject = Instantiate(jokeMenuPrefab, player1MenuCanvas.transform, false);

            player1JokeMenu = player1JokesObject.GetComponent<CategoryMenu>();

            player1EventSystem.SetSelectedGameObject(player1JokesObject.GetComponent<CategoryMenu>().buttons[0].gameObject);

            // Load Jokes
            player1Jokes = player1Categories[index].jokes;

            for (int i = 0; i < player1Jokes.Length; i++)
            {
                player1JokeMenu.buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = player1Jokes[i].joke;
            }

            // Show Data

            player1CategoryMenu.shade.SetActive(true);
            player1JokesOpen = true;
        }

        // Player 2
        else if (playerNum == 2 && !player2JokesOpen)
        {
            player2JokesObject = Instantiate(jokeMenuPrefab, player2MenuCanvas.transform, false);

            player2JokeMenu = player2JokesObject.GetComponent<CategoryMenu>();

            // Moving menu
            RectTransform rectTransform = player2JokesObject.GetComponent<RectTransform>();
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.anchorMin = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(1, 1);

            rectTransform.anchoredPosition = new Vector2(-150, -208);

            player2EventSystem.SetSelectedGameObject(player2JokesObject.GetComponent<CategoryMenu>().buttons[0].gameObject);

            player2CategoryMenu.shade.SetActive(true);

            player2JokesOpen = true;
        }
    }

    public void ButtonClick(int playerNum, int index)
    {
        Debug.Log("We made it");
        
        OpenJokeMenu(playerNum, index);
    }

    private void CloseJokes(int playerNum)
    {
        if (playerNum == 1)
        {
            Destroy(player1JokesObject);
            player1EventSystem.SetSelectedGameObject(player1CategoryMenu.buttons[0].gameObject);

            player1CategoryMenu.shade.SetActive(false);
            player1JokesOpen = false;
        }
        if (playerNum == 2)
        {
            Destroy(player2JokesObject);
            player2EventSystem.SetSelectedGameObject(player2CategoryMenu.buttons[0].gameObject);

            player2CategoryMenu.shade.SetActive(false);
            player2JokesOpen = false;
        }
    }

    private void CloseCategories(int playerNum)
    {
        if (playerNum == 1)
        {
            Destroy(player1CategoriesObject);

            player1MenuCanvas.GetComponent<PlayerCanvas>().shade.SetActive(false);
            player1CategoriesOpen = false;

            // Set new controls
            PlayerActionController.ChangePlayerState(1, 0);

        }
        if (playerNum == 2)
        {
            Destroy(player2CategoriesObject);

            player2MenuCanvas.GetComponent<PlayerCanvas>().shade.SetActive(false);
            player2CategoriesOpen = false;

            // Set new controls
            PlayerActionController.ChangePlayerState(2, 0);
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
                //CloseCategories(1);
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
                //CloseCategories(2);
            }
        }
    }

    private List<Category> SetRandCategories(int categoryCount)
    {
        return GetComponent<CategoryRandomizer>().GenerateCategories(categoryCount);

    }

    private void OpenShade()
    {
        player1MenuCanvas.GetComponent<PlayerCanvas>().shade.SetActive(true);
        player2MenuCanvas.GetComponent<PlayerCanvas>().shade.SetActive(true);
    }
}
