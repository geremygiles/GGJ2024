using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmGame : MonoBehaviour
{
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
    private int maxTiles = 0;
    private int tilesHit = 0;
    private int goneTiles = 0;


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
        if(goneTiles == maxTiles)
        {
            Reset();
            this.enabled = false;
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
