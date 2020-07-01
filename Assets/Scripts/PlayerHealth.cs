using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour {
    // cached reference
    TextMeshProUGUI playerHealthText;
    Player player;

    // Start is called before the first frame update
    void Start() {
        player = FindObjectOfType<Player>();
        playerHealthText = GameObject.FindGameObjectWithTag("PlayerHealthText").GetComponent<TextMeshProUGUI>();
        UpdatePlayerHealth();

    }

    // Update is called once per frame
    void Update() {
        UpdatePlayerHealth();
    }

    private void UpdatePlayerHealth() {
        var health = player.GetHealth();
        if (health < 0) health = 0;
        playerHealthText.text = health.ToString();
    }
}
