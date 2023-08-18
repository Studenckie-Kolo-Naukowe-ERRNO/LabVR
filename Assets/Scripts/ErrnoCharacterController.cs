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
    [SerializeField] private float currentCharacterSpeed;
    [Header("Loading")]
    [SerializeField] private Image loadingImage;
    [SerializeField] private float fadeTime =1;
    private Vector3 lastPos;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        StartCoroutine(FadeInOut(false));
    }

    void Update()
    {
        currentCharacterSpeed = Vector3.Distance(lastPos, transform.position)/Time.deltaTime;
        lastPos = transform.position;
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
}
