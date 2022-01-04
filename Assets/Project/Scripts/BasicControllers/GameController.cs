using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private AudioController _audioController;
    [SerializeField] private Text _scoreTxt;
    [SerializeField] private Image _lifePointsImage;
    [SerializeField] private Sprite[] _lifePointsSprites;
    private int _score;

    public void GetCollectable()
    {
        _score++;
        _scoreTxt.text = _score.ToString("D4");
        _audioController.PlayScoreAudio();
    }

    public void UpdateLifePointsHud(byte lifePoints)
    {
        int index = PlayerController.MAX_LIFE_POINTS - lifePoints;
        _lifePointsImage.sprite = _lifePointsSprites[index];
    }

    public void ReloadLevel(float seconds)
    {
        var scenesController = GetComponent<ScenesController>();
        scenesController.Invoke("ReloadScene", seconds);
    }
}
