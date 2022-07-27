using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public int score;
    public static GameManager inst;

    [SerializeField, Tooltip("Text yang asli")] Text scoreText;
    [SerializeField, Tooltip("Text pas game over")] Text gameOverScoreText;

    [SerializeField] PlayerMovement playerMovement;

    [SerializeField] GroundSpawner groundSpawner;

    [SerializeField] AudioSource coinAudio;

    public void IncrementScore ()
    {
        coinAudio.Play();
        score++;
        if (score % 200 == 0 && score > 0) {
             groundSpawner.listGroundTile.Reverse();
        } 
        scoreText.text = "SCORE: " + score;
        gameOverScoreText.text = "SCORE: " + score;
        // Increase the player's speed
        if (playerMovement.speed < 25) {
            playerMovement.speed += playerMovement.speedIncreasePerPoint;
        }
    }

    private void Awake ()
    {
        inst = this;
    }

    private void Start () {

	}

	private void Update () {
	
	}
}