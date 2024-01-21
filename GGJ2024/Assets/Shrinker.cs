using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrinker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 scale = transform.localScale;
        scale = new Vector2(15 - transform.position.z, 15 - transform.position.z);

        transform.localScale = scale;
    }
}
