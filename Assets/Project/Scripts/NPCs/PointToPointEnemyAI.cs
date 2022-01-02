using UnityEngine;

public class PointToPointEnemyAI : PointToPointAI
{
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
}
