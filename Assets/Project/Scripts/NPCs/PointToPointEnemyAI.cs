using UnityEngine;

public abstract class PointToPointEnemyAI : PointToPointAI
{
    [SerializeField] private GameObject _explosionPrefab;

    new void Start()
    {
        base.Start();
        if(_endPoint.position.x < _startPoint.position.x) FlipEnemy();
    }

    protected override void ChangeTarget()
    {
        base.ChangeTarget();
        FlipEnemy();
    }

    private void FlipEnemy()
    {
        Vector3 scale = _transform.localScale;
        _transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
    }

    private GameObject CreateExplosion(Transform parent)
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

    protected void DestroyEnemy()
    {
        GameObject parentObj = gameObject.transform.parent.gameObject;
        GameObject explosion = CreateExplosion(parentObj.transform);
        AudioController._instance.PlayEnemyDeathAudio();
        Destroy(gameObject);
        Destroy(parentObj, GetAnimationLength(_explosionPrefab));
    }
}
