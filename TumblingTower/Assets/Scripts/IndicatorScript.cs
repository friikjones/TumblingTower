using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorScript : MonoBehaviour
{
    StateManagerScript stateManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        stateManagerScript = GameObject.Find("StateManager").GetComponent<StateManagerScript>();
        transform.position = new Vector3(0, stateManagerScript.spawnHeight - .5f, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
