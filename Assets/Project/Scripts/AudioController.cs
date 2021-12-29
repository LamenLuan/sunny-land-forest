using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _scoreAudio, _jumpAudio,
        _enemyDeathAudio, _playerDeathAudio, _playerHurtAudio;

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

    public void PlayEnemyDeathAudio()
    {
        PlayOneShotAudio(_enemyDeathAudio);
    }

    public void PlayPlayerHurtAudio()
    {
        PlayOneShotAudio(_playerHurtAudio);
    }

    public void PlayPlayerDeathAudio()
    {
        PlayOneShotAudio(_playerDeathAudio);
    }
}