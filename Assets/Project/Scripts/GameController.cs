using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private AudioController _audioController;
    [SerializeField] private Text _scoreTxt;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private Image _lifePointsImage;
    [SerializeField] private Sprite[] _lifePointsSprites;
    private int _score;

    public void GetCollectable()
    {
        _score++;
        _scoreTxt.text = _score.ToString("D4");
        _audioController.PlayScoreAudio();
    }

    private GameObject CreateExplosion(GameObject gameObject, Transform parent)
    {
        Vector3 position = gameObject.transform.position;
        Quaternion rotation = gameObject.transform.localRotation;
        return Instantiate(_explosionPrefab, position, rotation, parent);
    }

    private float GetAnimationLength(GameObject gameObject)
    {
        Animator animator = gameObject.GetComponent<Animator>();
        var controller = animator.runtimeAnimatorController;
        return controller.animationClips[0].length * 0.9f;
    }

    public void DestroyEnemy(GameObject enemy)
    {
        GameObject parentObj = enemy.transform.parent.gameObject;
        GameObject explosion = CreateExplosion(enemy, parentObj.transform);
        _audioController.PlayEnemyDeathAudio();
        Destroy(enemy);
        Destroy( parentObj, GetAnimationLength(_explosionPrefab) );
    }

    public void UpdateLifePointsHud(byte lifePoints)
    {
        int index = PlayerController.MAX_LIFE_POINTS - lifePoints;
        _lifePointsImage.sprite = _lifePointsSprites[index];
    }

}
