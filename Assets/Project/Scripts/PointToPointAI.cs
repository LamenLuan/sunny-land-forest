using UnityEngine;

public abstract class PointToPointAI : MonoBehaviour
{
    [SerializeField] protected Transform _transform, _startPoint, _endPoint;
    [SerializeField] protected float _speed;
    protected bool _goingToEnd = true;

    protected void Start()
    {
        _transform.position = _startPoint.position;
    }

    protected void Update()
    {
        if( HasValidPoints() ) {
            Move();
            if( _transform.position == TargetPosition() ) ChangeTarget();
        }
    }

    protected virtual void ChangeTarget()
    {
        _goingToEnd = !_goingToEnd;
    }

    protected bool HasValidPoints() {
        return _transform != null &&
            _startPoint.position.x != _endPoint.position.x;
    }

    protected Vector3 TargetPosition() {
        return _goingToEnd ? _endPoint.position : _startPoint.position;
    }

    protected void Move()
    {
        _transform.position = Vector3.MoveTowards(
            _transform.position,
            TargetPosition(),
            _speed * Time.deltaTime
        );
    }

}
