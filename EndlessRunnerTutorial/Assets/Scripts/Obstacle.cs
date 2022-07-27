using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Obstacle : MonoBehaviour {

    bool first = true;
    PlayerMovement playerMovement;

	private void Start () {
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
	}

    private void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.name == "Dinos") {
            playerMovement.Jump();
            // Kill the player
            if (first) {
                playerMovement.Die();
                first = false;
            }
        }
    }

    private void Update () {
	
	}
}