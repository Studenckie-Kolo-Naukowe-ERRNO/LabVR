using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToLevel : MonoBehaviour
{
    [SerializeField] private int levelId = 0;
    bool lockState = false;
    private void OnTriggerEnter(Collider other)
    {
        if (lockState) return;
        if (!other.CompareTag("Player")) return;
        if (other.TryGetComponent(out ErrnoCharacterController ecc))
        {
            StartCoroutine(LoadLevel(ecc));
        }
    }

    IEnumerator LoadLevel(ErrnoCharacterController ecc)
    {
        lockState = true;
        ecc.LockMovement(true);
        ecc.FadeIn();
        yield return new WaitForSeconds(ecc.GetFadeTime()*1.5f);

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelId);
        while (!loadOperation.isDone)
        {
            Debug.Log(loadOperation.progress);
            yield return null;
        }
        Debug.Log($"Loaded level {levelId}");
    }
}
