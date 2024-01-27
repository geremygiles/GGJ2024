using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        //if (Input.GetKey(KeyCode.W))
        //{

        //    GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, moveSpeed), ForceMode.Impulse);
        //}
        //if (Input.GetKey(KeyCode.A))
        //{

        //    GetComponent<Rigidbody>().AddForce(new Vector3(-moveSpeed, 0, 0), ForceMode.Impulse);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{

        //    GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -moveSpeed), ForceMode.Impulse);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{

        //    GetComponent<Rigidbody>().AddForce(new Vector3(moveSpeed, 0, 0), ForceMode.Impulse);
        //}

        //Vector3 tempVel = GetComponent<Rigidbody>().velocity;
        //tempVel.x = Mathf.Clamp(tempVel.x, -maxSpeed, maxSpeed);
        //tempVel.z = Mathf.Clamp(tempVel.z, -maxSpeed, maxSpeed);

        //GetComponent<Rigidbody>().velocity = tempVel;
    }

    /// <summary>
    /// Player moves with left Joystick and D-Pad
    /// </summary>
    private void OnMove(InputValue value)
    {
        Vector2 moveAxis = value.Get<Vector2>();
        if (moveAxis != Vector2.zero)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3 (moveAxis.x * moveSpeed, 0, moveAxis.y), ForceMode.Impulse);
        }
        Debug.Log(moveAxis);
    }

    /// <summary>
    /// Player confirms with East Face Button
    /// </summary>
    private void OnConfirm()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
    }
}
