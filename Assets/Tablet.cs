using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tablet : MonoBehaviour {

    [ContextMenu("Restart The Level")]
    public void RestartTheLevel() {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    [ContextMenu("Return To Hub")]
    public void ReturnToHub() {
        SceneManager.LoadScene(0);
    }
}
