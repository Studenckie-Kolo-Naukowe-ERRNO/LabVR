using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class ErrnoCharacterController : MonoBehaviour
{
    private CharacterController characterController;
    [Header("Loading")]
    [SerializeField] private Image loadingImage;
    [SerializeField] private float fadeTime = 1;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        StartCoroutine(FadeInOut(false));
    }

    IEnumerator FadeInOut(bool fadeIn)
    {
        float alpha = fadeIn ? 0f : 1f;
        Color fadeColor = new Color(0, 0, 0, alpha);

        loadingImage.color = fadeColor;
        if(!fadeIn) yield return new WaitForSeconds(1);
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
}
