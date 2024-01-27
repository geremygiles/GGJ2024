using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Loads keys from a music input file and stores the key input and key hit times
/// </summary>
public class SongLoader : MonoBehaviour
{

    [SerializeField, Tooltip("Whether to display the key ID or not")]
    private bool showKeyID;

    [SerializeField, Tooltip("Prefabs for all paws, IN ORDER")]
    private GameObject[] pawPrefabs;
    [SerializeField, Tooltip("Spawn positions for all paws, IN ORDER")]
    private Vector3[] pawSpawnPositions;

    private float playTime; // current playing time for the loaded song
    private bool songIsPlaying; // whether a song is currently playing or not

    // csv related fields for loading key number and time
    private List<int> keyIDs = new List<int>();
    private List<int> keys = new List<int>();
    private List<float> keyHitTimes = new List<float>();

    private float songDelayStartTime = 0; // delay for the song to start, i.e. how long before notes spawn

    /// <summary>
    /// Sets the song delay
    /// </summary>
    /// <param name="delay">delay before notes spawn</param>
    public void SetDelay(float delay)
    {
        songDelayStartTime = delay;
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {
        LoadSong("Song1"); // test line of code
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        if (songIsPlaying)
        {

            playTime += Time.deltaTime;
            SpawnNotes();
        }
    }

    /// <summary>
    /// Checks if notes can be spawned
    /// </summary>
    private void SpawnNotes()
    {
        // goes through each remaining note in the note list
        for (int i = 0; i < keys.Count; i++)
        {
            // if the note time has passed, spawn the note and remove it from the "queue"
            if (playTime >= keyHitTimes[0])
            {
                GameObject _newKey = Instantiate(pawPrefabs[keys[i] - 1], pawSpawnPositions[keys[i] - 1], Quaternion.identity);

                if(showKeyID)
                {
                    _newKey.GetComponentInChildren<TextMeshProUGUI>().text = keyIDs[i].ToString();
                    keyIDs.RemoveAt(i);
                }

                keys.RemoveAt(i);
                keyHitTimes.RemoveAt(i);
                i--; // makes sure notes aren't skipped
            }
            else
            {
                // else break from the for loop if the first note isn't ready to be played
                break;
            }
        }
    }

    /// <summary>
    /// Loads a songs key input data
    /// </summary>
    /// <param name="songFileName">Name of the file to load</param>
    public void LoadSong(string songFileName)
    {
        ResetSongData();

        string _filePath = "";
#if UNITY_EDITOR
        _filePath = $"{Application.dataPath}/Music Input/{songFileName}.csv";
#else
        _filePath = $"{Application.dataPath}/{songFileName}.csv";
#endif

        string[] _fileData = System.IO.File.ReadAllLines(_filePath);

        for (int i = 0; i < _fileData.Length; i++)
        {
            string[] _line = _fileData[i].Split(",");
            keyIDs.Add(int.Parse(_line[0]));
            keys.Add(int.Parse(_line[1]));
            keyHitTimes.Add(float.Parse(_line[2] + songDelayStartTime));
        }

        songIsPlaying = true;
        songDelayStartTime = 0;

    }

    /// <summary>
    /// Resets all song data
    /// </summary>
    private void ResetSongData()
    {
        playTime = 0;
        keys.Clear();
        keyHitTimes.Clear();
    }
}
