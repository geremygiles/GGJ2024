using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PingPopController : MonoBehaviour
{
    private TextMeshProUGUI myText;

    [SerializeField]
    private float moveAmnt;

    [SerializeField]
    private float moveSpeed;

    private Vector3[] moveDirs;
    private Vector3 myMoveDir;

    private bool canMove = false;

    private void Start()
    {
        moveDirs = new Vector3[] {
            transform.up, 
            transform.up + transform.right/4,
            transform.up + -transform.right/4
        };

        myMoveDir = moveDirs[Random.Range(0, moveDirs.Length)];
    }

    private void Update()
    {
        if (canMove) 
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + myMoveDir, moveAmnt * (moveSpeed * Time.deltaTime));
        }
    }

    public void SetTextAndMove(string textStr, Color textColor)
    {
        myText = GetComponentInChildren<TextMeshProUGUI>();

        // I want to change this to be textColor, not just green
        myText.color = textColor;
        myText.text = textStr;
        canMove = true;
    }
}
