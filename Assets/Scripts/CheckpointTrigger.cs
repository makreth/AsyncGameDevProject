using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField]
    private LayerMask playerCollisionMask = 0;
    private Checkpoint checkpoint;

    void Start(){
        Assert.IsTrue(playerCollisionMask > 0);
        Assert.IsTrue(transform.parent.GetComponent<Checkpoint>() != null);
        checkpoint = transform.parent.GetComponent<Checkpoint>();
    }

    void OnTriggerEnter2D(Collider2D col){
        if (Utility.CheckLayer(playerCollisionMask, col.gameObject.layer)){
            col.gameObject.GetComponent<Player>().SetCheckpoint(checkpoint);
            checkpoint.SetupLevel();
            gameObject.SetActive(false);
        }
    }
}
