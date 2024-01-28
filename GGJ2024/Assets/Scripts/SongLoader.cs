using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Loads keys from a music input file and stores the key input and key hit times
/// </summary>
public class SongLoader : MonoBehaviour
{
    private float playTime; // current playing time for the loaded song
    private bool songIsPlaying; // whether a song is currently playing or not

    // csv related fields for loading key number and time
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
        LoadSong("Song1");
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        if (songIsPlaying)
            playTime += Time.deltaTime;
    }

    /// <summary>
    /// Loads a songs key input data
    /// </summary>
    /// <param name="songFileName">Name of the file to load</param>
    public void LoadSong(string songFileName)
    {
        ResetSongData();

        // Filepath Vars ------------------------------------- //
        string _filePath = "";
        #if UNITY_EDITOR
                _filePath = $"{Application.dataPath}/Music Input/{songFileName}.csv";
        #else
                _filePath = $"{Application.dataPath}/{songFileName}.csv";
        #endif
        string[] _fileData = System.IO.File.ReadAllLines(_filePath);
        
        // Other Vars ---------------------------------------- //
        float deviationThreshold = 0.5f;

        for (int i = 0; i < _fileData.Length; i++)
        {
            string[] _line = _fileData[i].Split(",");
            keys.Add(int.Parse(_line[1]));
            keyHitTimes.Add(float.Parse(_line[2] + songDelayStartTime));

            // Adjust key hit deviation:
            if (i > 0 && keyHitTimes[i] - keyHitTimes[i-1] < deviationThreshold)
            {
                keyHitTimes[i] = keyHitTimes[i-1];
            }
        }

        songIsPlaying = true;
        songDelayStartTime = 0;
    }

    /// <summary>
    /// Resets all song data
    /// </summary>
    public void ResetSongData()
    {
        playTime = 0;
        songIsPlaying = false;
        keys.Clear();
        keyHitTimes.Clear();
    }

    public bool getSongStatus()
    {
        return songIsPlaying;
    }

    public void setSongStatus(bool newStatus)
    {
        songIsPlaying = newStatus;
    }

    public float getPlayTime()
    {
        return playTime;
    }

    public List<int> getKeys()
    {
        return keys;
    }

    public List<float> getKeyTimes()
    {
        return keyHitTimes;
    }

}
