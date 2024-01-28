using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class AudienceBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject[] AudienceRows = new GameObject[3];
    private Vector3[] startingPositions = new Vector3[3];

    [SerializeField]
    private Boolean audienceReact;

    private float shakeAmount = 0.1f;
    private float shakeSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < AudienceRows.Length; i++)
        {
            startingPositions[i] = AudienceRows[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update() 
    {
        if(audienceReact)
        {
            
            AudienceRows[0].transform.position = new Vector3(startingPositions[0].x, startingPositions[0].y + (float)(Math.Sin(Time.time * shakeSpeed) * shakeAmount), startingPositions[0].z);

            AudienceRows[1].transform.position = new Vector3(startingPositions[1].x, startingPositions[1].y - (float)(Math.Sin(Time.time * shakeSpeed*2) * shakeAmount), startingPositions[1].z);

            AudienceRows[2].transform.position = new Vector3(startingPositions[2].x, startingPositions[2].y + (float)(Math.Sin(Time.time * shakeSpeed*4) * shakeAmount), startingPositions[2].z);
        }
        else if(!audienceReact && AudienceRows[0].transform.position != startingPositions[0] && AudienceRows[1].transform.position != startingPositions[1] && AudienceRows[2].transform.position != startingPositions[2])
        {
            float step = shakeSpeed/25 * Time.deltaTime;
            
            AudienceRows[0].transform.position = Vector3.MoveTowards(AudienceRows[0].transform.position, startingPositions[0], step);

            AudienceRows[1].transform.position = Vector3.MoveTowards(AudienceRows[1].transform.position, startingPositions[1], step);

            AudienceRows[2].transform.position = Vector3.MoveTowards(AudienceRows[2].transform.position, startingPositions[2], step);
        }
    }
}
