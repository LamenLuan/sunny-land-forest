using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _scoreAudio, _jumpAudio;

    private void PlayOneShotAudio(AudioClip audio)
    {
        if(audio != null) _audioSource.PlayOneShot(audio);
    }

    public void PlayScoreAudio()
    {
        PlayOneShotAudio(_scoreAudio);
    }

    public void PlayJumpAudio()
    {
        PlayOneShotAudio(_jumpAudio);
    }
}