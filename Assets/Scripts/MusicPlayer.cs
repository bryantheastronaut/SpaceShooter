using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    private void Awake() {
        var musicPlayerCount = FindObjectsOfType<MusicPlayer>().Length;
        if (musicPlayerCount > 1) {
            // inactivate it and destroy in. this should be a singleton.
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }
}
