using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PingPop : MonoBehaviour
{
    [SerializeField] 
    private GameObject PlayerObject;

    [SerializeField] 
    private GameObject popUpTextPrefab;

    [SerializeField] 
    public Color[] textColors;

    [SerializeField] 
    private float textKillTime;

    [Range(0f, 2f)]
    [SerializeField]
    private int performance = 0;

    private void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            HitNow(performance);
        }
    }

    public void HitNow(int skill)
    {
        GameObject newPopUp = Instantiate(popUpTextPrefab, PlayerObject.transform.position, Quaternion.identity);
        newPopUp.SetActive(true);

        Color textColor;
        string[] words = new string[3];
        int playerScore = 0;

        switch(skill) {
            case 0: // Bad case:
                textColor = textColors[0];
                words[0] = "Boo!"; words[1] = "You suck!"; words[2] = "Dat Dawg!";
                playerScore = 0;
            break;

            case 1: // Meh case:
                textColor = textColors[1];
                words[0] = "*chuckle*"; words[1] = "Oh brother!"; words[2] = "pfft";
                playerScore = 50;
            break;

            case 2: // Good case:
                textColor = textColors[2];
                words[0] = "Great!"; words[1] = "Ha Ha!"; words[2] = "LMAO!";
                playerScore = 100;
            break;

            default: // Crickets...
                textColor = Color.black;
            break;
        }

        string[] feedback = {"+" + playerScore.ToString(), words[Random.Range(0, words.Length)]};
        newPopUp.GetComponent<PingPopController>().SetTextAndMove(feedback[Random.Range(0, feedback.Length)], textColor);

        Destroy(newPopUp.gameObject, textKillTime);
    }
}
