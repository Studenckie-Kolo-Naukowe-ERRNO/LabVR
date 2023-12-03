using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Tablet : MonoBehaviour {
    [SerializeField] private AudioMixer effectsVolume;
    [SerializeField] private AudioMixer musicVolume;

    [ContextMenu("Restart The Level")]
    public void RestartTheLevel() {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    [ContextMenu("Return To Hub")]
    public void ReturnToHub() {
        SceneManager.LoadScene(0);
    }

    public void ChangeGraphicLevel(int level) {
        level = Mathf.Clamp(level, 0, QualitySettings.names.Length - 1);
        QualitySettings.SetQualityLevel(level, true);
    }

    public void SetEffectVolume(float sliderValue) {
        effectsVolume.SetFloat("EffectsVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetMusicVolume(float sliderValue) {
        musicVolume.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }
}
