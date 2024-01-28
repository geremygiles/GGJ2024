using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Canvas player1MenuCanvas;
    [SerializeField] private Canvas player2MenuCanvas;
    [SerializeField] private GameObject categoryMenuPrefab;
    [SerializeField] private GameObject jokeMenuPrefab;
    [SerializeField] private GameObject confirmPromptPrefab;

    [SerializeField] private MultiplayerEventSystem player1EventSystem;
    [SerializeField] private MultiplayerEventSystem player2EventSystem;

    [SerializeField] private Speaker player1Speaker;
    [SerializeField] private Speaker player2Speaker;

    private GameObject player1CategoriesObject;
    private CategoryMenu player1CategoryMenu;
    private GameObject player2CategoriesObject;
    private CategoryMenu player2CategoryMenu;

    private GameObject player1JokesObject;
    private CategoryMenu player1JokeMenu;
    private GameObject player2JokesObject;
    private CategoryMenu player2JokeMenu;

    private GameObject player1ConfirmObject;
    private CategoryMenu player1ConfirmMenu;
    private GameObject player2ConfirmObject;
    private CategoryMenu player2ConfirmMenu;

    private bool player1CategoriesOpen = false;
    private bool player2CategoriesOpen = false;
    private bool player1JokesOpen = false;
    private bool player2JokesOpen = false;
    private bool player1ConfirmOpen = false;
    private bool player2ConfirmOpen = false;
    private bool player1Ready = false;
    private bool player2Ready = false;


    private List<Category> player1Categories = new List<Category>();
    private List<Category> player2Categories = new List<Category>();

    private Joke[] player1Jokes;
    private Joke[] player2Jokes;

    private int player1CurrentCatIndex = 0;
    private int player1CurrentJokeIndex = 0;
    private int player2CurrentCatIndex = 0;
    private int player2CurrentJokeIndex = 0;

    private Joke player1SelectedJoke;
    private Joke player2SelectedJoke;

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

    private void OnDisable()
    {
    player1CategoriesOpen = false;
    player2CategoriesOpen = false;
    player1JokesOpen = false;
    player2JokesOpen = false;
    player1ConfirmOpen = false;
    player2ConfirmOpen = false;
    player1Ready = false;
    player2Ready = false;


    player1Categories = new List<Category>();
    player2Categories = new List<Category>();

    player1CurrentCatIndex = 0;
    player1CurrentJokeIndex = 0;
    player2CurrentCatIndex = 0;
    player2CurrentJokeIndex = 0;
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

            // Show Menu
            player1CategoriesObject = Instantiate(categoryMenuPrefab, player1MenuCanvas.transform, false);

            player1CategoryMenu = player1CategoriesObject.GetComponent<CategoryMenu>();

            // Show Catergory Name Data
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

            // Show Menu
            player2CategoriesObject = Instantiate(categoryMenuPrefab, player2MenuCanvas.transform, false);

            player2CategoryMenu = player2CategoriesObject.GetComponent<CategoryMenu>();

            // Moving menu
            RectTransform rectTransform = player2CategoriesObject.GetComponent<RectTransform>();
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.anchorMin = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(1, 1);

            rectTransform.anchoredPosition = new Vector2(-50, -50);

            // Show Catergory Name Data
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
            player1CurrentCatIndex = index;

            // Show Menu
            player1JokesObject = Instantiate(jokeMenuPrefab, player1MenuCanvas.transform, false);

            player1JokeMenu = player1JokesObject.GetComponent<CategoryMenu>();

            player1EventSystem.SetSelectedGameObject(player1JokesObject.GetComponent<CategoryMenu>().buttons[0].gameObject);

            // Load Joke Title Data
            player1Jokes = player1Categories[index].jokes;

            for (int i = 0; i < player1Jokes.Length; i++)
            {
                player1JokeMenu.buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = player1Jokes[i].name;
            }

            // Show Data

            player1CategoryMenu.shade.SetActive(true);
            player1JokesOpen = true;
        }

        // Player 2
        else if (playerNum == 2 && !player2JokesOpen)
        {
            player2CurrentCatIndex = index;

            player2JokesObject = Instantiate(jokeMenuPrefab, player2MenuCanvas.transform, false);

            player2JokeMenu = player2JokesObject.GetComponent<CategoryMenu>();

            player2EventSystem.SetSelectedGameObject(player2JokesObject.GetComponent<CategoryMenu>().buttons[0].gameObject);

            // Load Joke Title Data
            player2Jokes = player2Categories[index].jokes;

            for (int i = 0; i < player2Jokes.Length; i++)
            {
                player2JokeMenu.buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = player2Jokes[i].name;
            }

            // Moving menu
            RectTransform rectTransform = player2JokesObject.GetComponent<RectTransform>();
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.anchorMin = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(1, 1);

            rectTransform.anchoredPosition = new Vector2(-125, -155);

            
            // Show Data
            player2CategoryMenu.shade.SetActive(true);
            player2JokesOpen = true;
        }
    }

    public void ButtonClick(int playerNum, int index)
    {
        OpenJokeMenu(playerNum, index);
    }

    private void CloseJokes(int playerNum)
    {
        if (playerNum == 1)
        {
            Destroy(player1JokesObject);
            player1EventSystem.SetSelectedGameObject(player1CategoryMenu.buttons[player1CurrentCatIndex].gameObject);

            player1CategoryMenu.shade.SetActive(false);
            player1JokesOpen = false;
        }
        if (playerNum == 2)
        {
            Destroy(player2JokesObject);
            player2EventSystem.SetSelectedGameObject(player2CategoryMenu.buttons[player2CurrentCatIndex].gameObject);

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
        if (playerNum == 1)
        {
            if (player1ConfirmOpen)
            {
                CloseConfirm(1);
            }
            else if (player1JokesOpen)
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
            if (player2ConfirmOpen)
            {
                CloseConfirm(2);
            }
            else if (player2JokesOpen)
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

    public void StoreJokeObject(int player, int jokeIndex)
    {
        if (player == 1)
        {
            player1CurrentJokeIndex = jokeIndex;

            player1SelectedJoke = player1Categories[player1CurrentCatIndex].jokes[jokeIndex];

            ShowConfirmPrompt(1);
        }
        if (player == 2)
        {
            player2CurrentJokeIndex = jokeIndex;

            player2SelectedJoke = player2Categories[player2CurrentCatIndex].jokes[jokeIndex];

            ShowConfirmPrompt(2);
        }
    }

    private void ShowConfirmPrompt(int playerNum)
    {
        if (playerNum == 1)
        {
            // Show Prompt
            player1ConfirmObject = Instantiate(confirmPromptPrefab, player1MenuCanvas.transform, false);

            player1ConfirmMenu = player1ConfirmObject.GetComponent<CategoryMenu>();

            // Show Joke Data
            player1ConfirmMenu.promptText.text = player1SelectedJoke.textString;

            // Set selected item to cancel
            player1EventSystem.SetSelectedGameObject(player1ConfirmMenu.buttons[1].gameObject);

            player1JokeMenu.shade.SetActive(true);
            player1ConfirmOpen = true;
        }

        if (playerNum == 2)
        {
            // Show Prompt
            player2ConfirmObject = Instantiate(confirmPromptPrefab, player2MenuCanvas.transform, false);

            player2ConfirmMenu = player2ConfirmObject.GetComponent<CategoryMenu>();

            // Show Joke Data
            player2ConfirmMenu.promptText.text = player2SelectedJoke.textString;

            // Set selected item to cancel
            player2EventSystem.SetSelectedGameObject(player2ConfirmMenu.buttons[1].gameObject);

            // Move Prompt
            RectTransform rectTransform = player2ConfirmObject.GetComponent<RectTransform>();
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.anchorMin = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(1, 1);

            rectTransform.anchoredPosition = new Vector2(-200, -310);

            player2JokeMenu.shade.SetActive(true);
            player2ConfirmOpen = true;
        }
    }

    private void CloseConfirm(int playerNum)
    {
        if (playerNum == 1)
        {
            Destroy(player1ConfirmObject);
            player1EventSystem.SetSelectedGameObject(player1JokeMenu.buttons[player1CurrentJokeIndex].gameObject);

            player1JokeMenu.shade.SetActive(false);
            player1ConfirmOpen = false;
        }
        
        if (playerNum == 2)
        {
            Destroy(player2ConfirmObject);
            player2EventSystem.SetSelectedGameObject(player2JokeMenu.buttons[player2CurrentJokeIndex].gameObject);

            player2JokeMenu.shade.SetActive(false);
            player2ConfirmOpen = false;
        }
    }

    public void Confirm(int playerNum)
    {
        if (playerNum == 1)
        {
            player1Ready = true;

            // Set new controls
            PlayerActionController.ChangePlayerState(1, 0);

            // Add to text output queue
            Singleton.Instance.TextController.Enqueue(player1SelectedJoke, player1Speaker);
        }
        if (playerNum == 2)
        {
            player2Ready = true;

            // Set new controls
            PlayerActionController.ChangePlayerState(2, 0);

            // Add to text output queue
            Singleton.Instance.TextController.Enqueue(player2SelectedJoke, player2Speaker);
        }

        CloseConfirm(playerNum);
        CloseJokes(playerNum);
        CloseCategories(playerNum);

        

        if (player1Ready && player2Ready)
        {
            Debug.Log("Rhythm Time!");

            Singleton.Instance.TextController.ContinueText();
            this.enabled = false;
        }
    }
}
