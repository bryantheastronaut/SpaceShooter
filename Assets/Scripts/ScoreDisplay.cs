using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour {
    TextMeshProUGUI scoreText;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start() {
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
        UpdateScore();
    }

    // Update is called once per frame
    void Update() {
        UpdateScore();
    }

    void UpdateScore() {
        scoreText.text = gameSession.GetScore().ToString();
    }
}
