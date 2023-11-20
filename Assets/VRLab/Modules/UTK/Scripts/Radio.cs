using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour {
    [NonReorderable, SerializeField] private List<RadioChannels> channels = new List<RadioChannels>();
    private int channelSelected = 0;
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        foreach (RadioChannels channel in channels) {
            StartCoroutine(channel.PlayMusicCoroutine(audioSource));
            StartCoroutine(channel.CountSeconds());
        }
    }

    [ContextMenu("Switch channel or Turn on the radio")]
    public void OnRadioStationChange() {
        foreach (RadioChannels channel in channels) {
            channel.channelIsPlaying = false;
        }
        channels[channelSelected].channelIsPlaying = true;

        channels[channelSelected].PlayMusic(audioSource);
        audioSource.time = channels[channelSelected].secondsPlayed;

        channelSelected++;
        if (channelSelected >= channels.Count) { 
            channelSelected = 0; 
        }

    }
}

[System.Serializable]
public class RadioChannels {
    public AudioClip[] music;
    [HideInInspector] public ulong secondsPlayed = 0;
    private int playingSongIndex = 0;
    [HideInInspector] public bool channelIsPlaying = false;

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

    public IEnumerator PlayMusicCoroutine(AudioSource audioSource) {
        while (true) {
            secondsPlayed = 0;
            yield return new WaitForSeconds(GetTheSongLength());

            if (channelIsPlaying) {
                GetRandomSong();
                PlayMusic(audioSource);
            }
        }
    }

    public IEnumerator CountSeconds() {
        while (true) {
            yield return new WaitForSeconds(1);
            secondsPlayed++;
        }
    }

    public void PlayMusic(AudioSource audioSource) {
        audioSource.clip = music[playingSongIndex];
        audioSource.Play();
    }
}
