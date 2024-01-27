using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawCollision : MonoBehaviour
{
    private int collisionCount = 0;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        float _speed = 2f;
        GetComponent<Rigidbody>().velocity = _speed * Vector3.down;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            switch (collisionCount)
            {
                case 1:
                    Debug.Log("Miss");
                    break;
                case 2:
                    Debug.Log("OK");
                    break;
                case 3:
                    Debug.Log("Great");
                    break;
                case 4:
                    Debug.Log("Perfect");
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        collisionCount++;
    }

    private void OnTriggerExit(Collider other)
    {
        collisionCount--;
    }
}
