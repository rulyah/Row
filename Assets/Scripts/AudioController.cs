using UI;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private AudioSource _song;
    [SerializeField] private AudioSource _sound;

    public void Init()
    {
        PauseScreen.onSongVolumeChange += OnSongVolumeChange;
        PauseScreen.onSoundVolumeChange += OnSoundVolumeChange;
    }

    public void Play(AudioClip audioClip)
    {
        if(_sound.mute == false) _sound.PlayOneShot(audioClip);
    }
    
    private void OnSongVolumeChange(bool isPlaying)
    {
        _song.mute = !isPlaying;
    }

    private void OnSoundVolumeChange(bool isPlaying)
    {
        _sound.mute = !isPlaying;
    }

    public void DeInit()
    {
        PauseScreen.onSongVolumeChange -= OnSongVolumeChange;
        PauseScreen.onSoundVolumeChange -= OnSoundVolumeChange;
    }
}