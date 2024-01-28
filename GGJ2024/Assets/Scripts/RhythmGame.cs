using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RhythmGame : MonoBehaviour
{
    [SerializeField]
    private GameObject pawPrefab;

    [SerializeField]
    private int heightOffset = 10;
    private GameObject[] buttons = new GameObject[4];
    private Vector3[] pawSpawnPositions = new Vector3[4];

    [SerializeField]
    private SongLoader loader;

    

    private float[] defaultAlphas = new float[2];
    private Component[] images = new Component[2];

    // Start is called before the first frame update
    void Start()
    {
        // Set the spawn positions:
        SetPawSpawns();

        images = buttons[0].GetComponentsInChildren<Image>();
        int index = 0;
        foreach (Image image in images)
        {
            defaultAlphas[index] = image.color.a;
            index++;
        }
    }

    private void Update()
    {
        if(loader.getSongStatus())
            SpawnNotes();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {   ButtonPress(0);     }
        else
        {   ButtonRelease(0);   }


        if(Input.GetKey(KeyCode.Alpha2))
        {   ButtonPress(1);     }
        else
        {   ButtonRelease(1);   }


        if(Input.GetKey(KeyCode.Alpha3))
        {   ButtonPress(2);     }
        else
        {   ButtonRelease(2);   }


        if(Input.GetKey(KeyCode.Alpha4))
        {   ButtonPress(3);     }
        else
        {   ButtonRelease(3);   }
    }

    private void ButtonPress(int buttonCode)
    {
        images = buttons[buttonCode].GetComponentsInChildren<Image>();
        foreach(Image image in images)
        {
            var temp = image.color;
            temp.a = 1f;
            image.color = temp;
        }
    }

    private void ButtonRelease(int buttonCode)
    {
        images = buttons[buttonCode].GetComponentsInChildren<Image>();
        foreach(Image image in images)
        {
            var temp = image.color;
            temp.a = defaultAlphas[0];
            image.color = temp;
        }
    }

    private void SetPawSpawns()
    {
        Vector3 pawHeight = new Vector3(0, heightOffset, 0);

        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = GameObject.Find("PawButton" + (i+1));
            pawSpawnPositions[i] = buttons[i].transform.position + pawHeight;
        }
    }

    private void SpawnNotes()
    {
        // goes through each remaining note in the note list
        // keys.Count = total tiles
        for (int i = 0; i < loader.getKeys().Count; i++)
        {
            // if the note time has passed, spawn the note and remove it from the "queue"
            if (loader.getPlayTime() >= loader.getKeyTimes()[0])
            {
                GameObject newKey = Instantiate(pawPrefab, pawSpawnPositions[loader.getKeys()[i] - 1], Quaternion.identity);

                loader.getKeys().RemoveAt(i);
                loader.getKeyTimes().RemoveAt(i);
                i--; // makes sure notes aren't skipped
            }
            else
            {
                // else break from the for loop if the first note isn't ready to be played
                break;
            }
        }
    }
}
