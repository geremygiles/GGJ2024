using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// For Devs
/// Records the keys hit and the time and saves it in a csv
/// </summary>
public class KeyRecorder : MonoBehaviour
{
    [SerializeField, Tooltip("Name of the file this recording will be saved to.")]
    private string fileName;

    private float recordingTime; // current recording time since scene start or last reset

    // csv related fields for saving key number and time
    private List<int> keys = new List<int>();
    private List<float> keyHitTimes = new List<float>();

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        recordingTime += Time.deltaTime;

        TrackKeyInput();
        CheckForReset();
        CheckForSave();
    }

    /// <summary>
    /// Tracks the keyboard input (1, 2, 3, 4) and records the input
    /// </summary>
    private void TrackKeyInput()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Record(1);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Record(2);
        }

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            Record(3);
        }

        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            Record(4);
        }
    }

    /// <summary>
    /// Records the key hit and time
    /// </summary>
    /// <param name="keyHit">Respective input for the key hit</param>
    private void Record(int keyHit)
    {
        keys.Add(keyHit);
        keyHitTimes.Add(recordingTime);
    }

    /// <summary>
    /// Resets the time and all keys if the user presses 'R'
    /// </summary>
    private void CheckForReset()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            recordingTime = 0f;
            keys.Clear();
            keyHitTimes.Clear();
        }
    }

    /// <summary>
    /// Saves all key codes and their respective hit time
    /// </summary>
    private void CheckForSave()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            // if the lists aren't the same size, don't save because something went wrong
            if(keys.Count != keyHitTimes.Count)
            {
                Debug.LogWarning("Cannot Save: Key hit count does not match key hit time count in KeyRecorder.cs");
                return;
            }

            string _saveFilePath = $"{Application.dataPath}/Music Input/{fileName}.csv";
            StreamWriter writer = new StreamWriter(_saveFilePath);

            for (int i = 0; i < keys.Count; i++)
            {
                writer.WriteLine($"{keys[i]},{keyHitTimes[i]}");
            }

            writer.Close();
        }
    }
}
