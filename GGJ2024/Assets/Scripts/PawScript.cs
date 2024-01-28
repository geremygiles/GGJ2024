using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class PawCollision : MonoBehaviour
{
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        float _speed = 2f;
        GetComponent<Rigidbody>().velocity = _speed * Vector3.down;
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag == "TileKiller")
        {
            Destroy(gameObject);
        }
    }
}
