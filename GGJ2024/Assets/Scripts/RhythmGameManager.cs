using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmGame : MonoBehaviour
{
    // !!! ATTENTION !! //
    // I am using this array of two ints to store both our player's score currently.
    // We may need to write code to pick which index of the array we are manipulating
    // depending on who the current player is. My default starting player is 1.

    // Geremy, this may be where you need to link your players to this script.
    public int[] playerScores = new int[2];
    public int currPlayer = 0;

    // Player1 = 0, Player2 = 1
    // ETC



    // Created an instance so I can reference this script in other scripts w/o
    // having to serializefield or anything wacky like that...
    public static RhythmGame instance;

    [SerializeField]
    private GameObject pawPrefab;

    [SerializeField]
    private int heightOffset = 10;
    private GameObject[] buttons = new GameObject[4];
    private Vector3[] pawSpawnPositions = new Vector3[4];

    [SerializeField]
    private SongLoader loader;


    // Tile Info:
    private int maxTiles;
    private int tilesHit;
    private int goneTiles;


    // Start is called before the first frame update
    void Start()
    {
        // We will only have 1 Rhythm Game Manager. Since we made it static, they can only share this same instance value... 
        instance = this;
        

        // Set the spawn positions:
        SetPawSpawns();
        maxTiles = loader.getKeys().Count;
    }

    private void FixedUpdate()
    {
        // Enabling the game:
        if(loader.getSongStatus() && this.enabled)
        {
            SpawnNotes();
        }
        
        // This means the game is over
        /*if(goneTiles == maxTiles)
        {
            Reset();
            this.enabled = false;
        }
        But it doesn't fokin work*/
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
                GameObject Paw = Instantiate(pawPrefab, pawSpawnPositions[loader.getKeys()[i] - 1], Quaternion.identity);

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


    // Tile logistics:
    public void AddScore(int score)
    {
        tilesHit++;
        TileDestroyed();

        // Calculating points:
        float multiplier = tilesHit/maxTiles * 100;

        // Awarding points:
        // ... help here ... //
    }

    public void TileDestroyed()
    {   goneTiles++;    }

    public void Reset()
    {
        tilesHit = 0;
        goneTiles = 0;
        maxTiles = 0;
        loader.ResetSongData();
    }
}
