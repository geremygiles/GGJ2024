using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed;
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {

            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, moveSpeed), ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {

            GetComponent<Rigidbody>().AddForce(new Vector3(-moveSpeed, 0, 0), ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.S))
        {

            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -moveSpeed), ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {

            GetComponent<Rigidbody>().AddForce(new Vector3(moveSpeed, 0, 0), ForceMode.Impulse);
        }

        Vector3 tempVel = GetComponent<Rigidbody>().velocity;
        tempVel.x = Mathf.Clamp(tempVel.x, -maxSpeed, maxSpeed);
        tempVel.z = Mathf.Clamp(tempVel.z, -maxSpeed, maxSpeed);

        GetComponent<Rigidbody>().velocity = tempVel;
    }
}
