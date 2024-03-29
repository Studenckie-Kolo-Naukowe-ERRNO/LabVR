using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRLabEssentials;

public class LevelManagerPage : MonoBehaviour
{
    bool lockState = false;
    
    public void LoadLevel(int levelId)
    {
        if(!lockState) 
        {
            StartCoroutine(LoadingLevel(levelId));
        }
    }

    IEnumerator LoadingLevel(int levelId)
    {
        lockState = true;
        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out ErrnoCharacterController ecc);
        if(ecc != null)
        {
            ecc.LockMovement(true);
            ecc.FadeIn();
            yield return new WaitForSeconds(ecc.GetFadeTime() * 1.5f);
        }

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelId);
        while (!loadOperation.isDone)
        {
            yield return null;
        }
    }
}
