using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Tablet : MonoBehaviour {
    [SerializeField] private AudioMixer effectsVolume;
    [SerializeField] private AudioMixer musicVolume;
    private int graphicsLevelIndex = 0;

    private void Start() {
        SetupGraphicsChangingBtn();
    }

    private void SetupGraphicsChangingBtn() {
        QualitySettings.SetQualityLevel(graphicsLevelIndex, true);
    }

    [ContextMenu("Restart The Level")]
    public void RestartTheLevel() {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    [ContextMenu("Return To Hub")]
    public void ReturnToHub() {
        SceneManager.LoadScene(0);
    }

    public void ChangeGraphicLevel(GameObject btn) {
        graphicsLevelIndex++;
        if (graphicsLevelIndex >= QualitySettings.names.Length) graphicsLevelIndex = 0;
        QualitySettings.SetQualityLevel(graphicsLevelIndex, true);
        btn.GetComponentInChildren<TextMeshProUGUI>().SetText(QualitySettings.names[graphicsLevelIndex]);
    }

    public void SetEffectVolume(float sliderValue) {
        effectsVolume.SetFloat("EffectsVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetMusicVolume(float sliderValue) {
        musicVolume.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }
}
