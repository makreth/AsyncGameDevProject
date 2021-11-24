using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    public GameObject checkpointObject;
    private Checkpoint checkpoint;
    void Start()
    {
        checkpoint = checkpointObject.GetComponent<Checkpoint>();
    }

    public Checkpoint GetCheckpoint(){
        return checkpoint;
    }
}
