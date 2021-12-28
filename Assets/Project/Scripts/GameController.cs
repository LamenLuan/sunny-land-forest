using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private AudioController _audioController;
    [SerializeField] private Text _scoreTxt;
    [SerializeField] private GameObject _explosionPrefab;
    private int _score;

    public void GetCollectable()
    {
        _score++;
        _scoreTxt.text = _score.ToString("D4");
        _audioController.PlayScoreAudio();
    }

    private GameObject CreateExplosion(Collider2D colider, Transform parent)
    {
        Vector3 position = colider.transform.position;
        Quaternion rotation = colider.transform.localRotation;
        return Instantiate(_explosionPrefab, position, rotation, parent);
    }

    private float GetAnimationLength(GameObject gameObject)
    {
        Animator animator = gameObject.GetComponent<Animator>();
        var controller = animator.runtimeAnimatorController;
        return controller.animationClips[0].length * 0.9f;
    }

    public void DestroyEnemy(Collider2D collider)
    {
        GameObject parentObj = collider.transform.parent.gameObject;
        GameObject explosion = CreateExplosion(collider, parentObj.transform);
        _audioController.PlayEnemyDeathAudio();
        Destroy(collider.gameObject);
        Destroy( parentObj, GetAnimationLength(_explosionPrefab) );
    }

}
