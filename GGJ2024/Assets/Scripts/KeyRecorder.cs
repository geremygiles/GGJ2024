using UnityEngine;

/// <summary>
/// For Devs
/// Records the keys hit and the time and saves it in a csv
/// </summary>
public class KeyRecorder : MonoBehaviour
{
    private float recordingTime; // current recording time since scene start or last reset

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

        if(Input.GetKey(KeyCode.Alpha4))
        {
            Record(4);
        }
    }

    /// <summary>
    /// Records the key hit and time
    /// </summary>
    /// <param name="inputNumber">Respective input for the key hit</param>
    private void Record(int inputNumber)
    {

    }

    /// <summary>
    /// Resets the time and all keys if the user presses 'R'
    /// </summary>
    private void CheckForReset()
    {
        if(Input.GetKey(KeyCode.R))
        {
            recordingTime = 0f;
        }
    }

    /// <summary>
    /// Saves all key codes and their respective hit time
    /// </summary>
    private void CheckForSave()
    {

    }
}
