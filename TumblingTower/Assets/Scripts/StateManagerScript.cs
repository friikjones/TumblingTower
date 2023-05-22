using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManagerScript : MonoBehaviour
{
    // level number variables
    public int currentLevel, prevLevel;

    // spawn variables
    public int spawnDistance, spawnHeight;
    public float spawnBaseMoveSpeed, spawnDownSpeed;

    //progression variables
    public AnimationCurve difficultyCurve;
    public int levelDifficultyCap;

    // prefab variables
    public int freezeAfter;
    public GameObject[] piecePrefabs;

    void Start()
    {

    }

    void FixedUpdate()
    {
        if (currentLevel > prevLevel)
        {
            foreach (Transform child in transform)
            {
                if (child.tag != "Indicator")
                {

                    // var childRigidbody = child.GetComponent<Rigidbody>();
                    // if (childRigidbody != null)
                    // {
                    //     childRigidbody.isKinematic = true;
                    // }

                    child.position = child.position + Vector3.down;

                    // if (childRigidbody != null)
                    // {
                    //     childRigidbody.isKinematic = false;
                    // }


                    if (child.tag == "Piece")
                    {
                        if (child.GetComponent<PieceController>().levelSpawned < (currentLevel - freezeAfter))
                        {
                            child.GetComponent<Rigidbody>().isKinematic = true;
                        }
                    }

                }
            }
            prevLevel++;
            SpawnNextPiece();
        }

        float calculatedSpeed = difficultyCurve.Evaluate(currentLevel);
        Debug.Log("curve variable" + calculatedSpeed);


    }

    public void NextLevel()
    {
        currentLevel++;
    }

    public void LoseGame()
    {
        Debug.Log("Lose State here");
        Time.timeScale = 0;
    }

    void SpawnNextPiece()
    {
        int dir = Random.Range(0, 4);
        Vector3 instantiateVector = new Vector3(0, spawnHeight, 0);
        Vector3 instantiateMovementDirection = Vector3.zero;

        switch (dir)
        {
            case 0:
                instantiateVector = instantiateVector + Vector3.forward * spawnDistance;
                instantiateMovementDirection = Vector3.back;
                break;
            case 1:
                instantiateVector = instantiateVector + Vector3.back * spawnDistance;
                instantiateMovementDirection = Vector3.forward;
                break;
            case 2:
                instantiateVector = instantiateVector + Vector3.left * spawnDistance;
                instantiateMovementDirection = Vector3.right;
                break;
            case 3:
                instantiateVector = instantiateVector + Vector3.right * spawnDistance;
                instantiateMovementDirection = Vector3.left;
                break;
            default:
                break;

        }

        // Debug.Log("Spawning at " + instantiateVector);

        GameObject pieceInstance = Instantiate(piecePrefabs[Random.Range(0, piecePrefabs.Length)], instantiateVector, Quaternion.identity);
        pieceInstance.transform.parent = this.transform;

        PieceController pieceScript = pieceInstance.GetComponent<PieceController>();
        pieceScript.velocity = instantiateMovementDirection * spawnBaseMoveSpeed * difficultyCurve.Evaluate(currentLevel);

        pieceScript.downSpeed = spawnDownSpeed;
        pieceScript.levelSpawned = currentLevel;
        pieceScript.stateManager = this.GetComponent<StateManagerScript>();



    }
}
