using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip _scoreAudio, _jumpAudio,
        _enemyDeathAudio, _playerDeathAudio, _playerHurtAudio;

    private void PlayOneShotAudio(AudioClip audio)
    {
        var audioSource = GetComponent<AudioSource>();
        if(audio != null) audioSource.PlayOneShot(audio);
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