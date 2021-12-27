using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private AudioController _audioController;
    [SerializeField] private Text _scoreTxt;
    private int _score;

    public int Score {
        get => _score;
        set {
            _score = value;
            _scoreTxt.text = _score.ToString("D4");
        }
    }

    public void GetCollectable()
    {
        Score++;
        _audioController.PlayScoreAudio();
    }

}
