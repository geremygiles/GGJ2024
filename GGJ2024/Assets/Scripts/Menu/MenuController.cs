using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Canvas player1MenuCanvas;
    [SerializeField] private Canvas player2MenuCanvas;
    [SerializeField] private GameObject categoryMenuPrefab;
    [SerializeField] private GameObject jokeMenuPrefab;

    [SerializeField] private MultiplayerEventSystem player1EventSystem;
    [SerializeField] private MultiplayerEventSystem player2EventSystem;

    // Start is called before the first frame update
    void Start()
    {
        OpenCategoryMenus();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Opens the category menus for both players
    /// </summary>
    public void OpenCategoryMenus()
    {
        // Player 1
        GameObject player1Menu = Instantiate(categoryMenuPrefab, player1MenuCanvas.transform, false);

        // Player 2
        GameObject player2Menu = Instantiate(categoryMenuPrefab, player2MenuCanvas.transform, false);

        RectTransform rectTransform = player2Menu.GetComponent<RectTransform>();
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.anchorMin = new Vector2(1, 1);
        rectTransform.pivot = new Vector2(1, 1);

        rectTransform.anchoredPosition = new Vector2(-50, -50);

        GameObject firstButton = player1Menu;
        player1EventSystem.SetSelectedGameObject(firstButton);
    }
}
