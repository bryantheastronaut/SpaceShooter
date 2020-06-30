using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {
    [SerializeField] float delayInSeconds = 2f;

    public void OnStartGame() {
        SceneManager.LoadScene("GameScreen");
    }

    public void OnGameOver() {
        StartCoroutine(DelayLoad());
    }

    IEnumerator DelayLoad() {
        yield return new WaitForSeconds(delayInSeconds);
        int lastScene = SceneManager.sceneCountInBuildSettings - 1;
        SceneManager.LoadScene(lastScene);
    }

    public void OnQuitGame() {
        Application.Quit();
    }

    public void OnMainMenu() {
        SceneManager.LoadScene(0);
    }

}
