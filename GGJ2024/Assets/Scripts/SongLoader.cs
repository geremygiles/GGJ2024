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
    [SerializeField] private List<int> keys = new List<int>();
    [SerializeField] private List<float> keyHitTimes = new List<float>();

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        if (songIsPlaying)
        {
            playTime += Time.deltaTime;
        }
    }

    public void LoadSong(string songFileName)
    {

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
            keys.Add(int.Parse(_line[0]));
            keyHitTimes.Add(float.Parse(_line[1]));
        }

    }

    private void ResetSong()
    {
        playTime = 0;
        keys.Clear();
        keyHitTimes.Clear();
    }
}
