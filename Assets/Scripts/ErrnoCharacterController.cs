using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(CharacterController))]
public class ErrnoCharacterController : MonoBehaviour
{
    private CharacterController characterController;
    private ActionBasedContinuousMoveProvider moveProvider;
    [SerializeField] private Camera mainCam;
    [Header("Stats")]
    [SerializeField] private float moveSpeed = 1;
    [Header("Loading")]
    [SerializeField] private Image loadingImage;
    [SerializeField] private float fadeTime = 1;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        moveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
        if(moveProvider != null)
        {
            moveProvider.moveSpeed = moveSpeed;
        }
        FadeOut();
    }

    //true means screen getting darker
    IEnumerator FadeInOut(bool fadeIn)
    {
        float alpha = fadeIn ? 0f : 1f;
        Color fadeColor = new Color(0, 0, 0, alpha);

        loadingImage.color = fadeColor;

        yield return null;
        mainCam.enabled = true;

        if (!fadeIn) yield return new WaitForSeconds(1);
        while (alpha>=0 && alpha<=1)
        {
            if (fadeIn) alpha += Time.deltaTime / fadeTime;
            else alpha -= Time.deltaTime / fadeTime;
            fadeColor.a = alpha;
            loadingImage.color = fadeColor;
            yield return null;
        }

        alpha = fadeIn ? 1f : 0f;
        fadeColor.a = alpha;
        loadingImage.color = fadeColor;
    }
    [ContextMenu("Fade out")]
    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInOut(false));
    }
    [ContextMenu("Fade in")]
    public void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInOut(true));
    }

    public float GetFadeTime()
    {
        return fadeTime;
    }
    //true means move is locked
    public void LockMovement(bool lockState)
    {
        moveProvider.moveSpeed = lockState ? 0 : moveSpeed;
    }
}
