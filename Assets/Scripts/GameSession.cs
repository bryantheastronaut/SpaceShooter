using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour {
    [SerializeField] int score = 0;

    private void Awake() {
        SetUpSingleton();
    }

    private void SetUpSingleton() {
        if (FindObjectsOfType(GetType()).Length > 1) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore() { return score; }
    public void UpdateScore(int points) { score += points; }
    public void ResetScore() { Destroy(gameObject); }

}
