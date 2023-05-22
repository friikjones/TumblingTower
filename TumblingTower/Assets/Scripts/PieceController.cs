using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    // spawn variables
    public Vector3 velocity;
    public float downSpeed;

    // gamestate variables
    public StateManagerScript stateManager;
    public int levelSpawned;

    // internal variables
    public bool moving;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = velocity;
        moving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && moving)
        {
            rb.velocity = Vector3.down * downSpeed;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Piece" || other.transform.tag == "InitialPiece")
        {
            if (moving)
            {
                stateManager.NextLevel();
                moving = false;
                rb.velocity = Vector3.zero;
                rb.useGravity = true;
            }
        }
        else
        {
            stateManager.LoseGame();
        }

    }
}
