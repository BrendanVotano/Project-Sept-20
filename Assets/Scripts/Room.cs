using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    Renderer renderer;
    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (renderer.IsVisibleFrom(Camera.main)) 
            Debug.Log("Visible");
        else 
            Debug.Log("Not visible");
    }
}
