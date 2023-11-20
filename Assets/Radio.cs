using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Radio : MonoBehaviour {
    [NonReorderable] public List<RadioChannels> channels = new List<RadioChannels>();
    private int channelSelected = 0;
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        foreach (RadioChannels channel in channels) {
            StartCoroutine(channel.PlayMusicInTheBackground(audioSource));
            StartCoroutine(channel.CountSeconds());
        }
    }

    [ContextMenu("Switch channel or Turn on the radio")]
    public void OnRadioStationChange() {
        foreach (RadioChannels channel in channels) {
            channel.isPlaying = false;
        }

        channels[channelSelected].isPlaying = true;

        audioSource.Stop();
        audioSource.clip = channels[channelSelected].music[channels[channelSelected].playingSongIndex];
        audioSource.time = channels[channelSelected].secondsPlayed;
        audioSource.Play();

        channelSelected++;

        if (channelSelected >= channels.Count) { 
            channelSelected = 0; 
        }

    }
}

[System.Serializable]
public class RadioChannels {
    public AudioClip[] music;
    [HideInInspector] public int playingSongIndex = 0;
    [HideInInspector] public ulong secondsPlayed = 0;
    [HideInInspector] public bool isPlaying = false;

    public void GetRandomSong() {
        int randomSong = Random.Range(0, music.Length - 1);

        if (randomSong == playingSongIndex) {
            if (playingSongIndex == 0) {
                randomSong++;
            } else {
                randomSong--;
            }

        }

        playingSongIndex = randomSong;
    }

    public float GetTheSongLength() {
        return music[playingSongIndex].length;
    }

    public IEnumerator PlayMusicInTheBackground(AudioSource audioSource) {
        while (true) {
            secondsPlayed = 0;

            yield return new WaitForSeconds(GetTheSongLength());
            if (isPlaying) {
                GetRandomSong();
                audioSource.clip = music[playingSongIndex];
                audioSource.Play();
            }
        }
    }

    public IEnumerator CountSeconds() {
        while (true) {
            yield return new WaitForSeconds(1);
            secondsPlayed++;
        }
    }
}
