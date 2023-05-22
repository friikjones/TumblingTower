using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 initalPos;
    public float catchUpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        initalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, initalPos);
        // Debug.Log("camera distance: " + distance);
        if (distance > 0.1)
        {
            transform.position = Vector3.Lerp(transform.position, initalPos, Time.deltaTime * catchUpSpeed);
        }
    }
}
