using UnityEngine;

public class SlugAI : MonoBehaviour
{
    [SerializeField] private Transform _transform, _startPoint, _endPoint;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;
    private bool _goingToEnd  = true;

    void Start()
    {
        _transform.position = _startPoint.position;
        if(_endPoint.position.x < _startPoint.position.x)
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }

    void Update()
    {
        if( HasValidPoints() ) {
            MoveSlug();
            if( _transform.position == TargetPosition() ) {
                _goingToEnd = !_goingToEnd;
                _spriteRenderer.flipX = !_spriteRenderer.flipX;
            }
        }
    }

    private bool HasValidPoints() {
        return _transform != null &&
            _startPoint.position.x != _endPoint.position.x;
    }

    private Vector3 TargetPosition() {
        return _goingToEnd ? _endPoint.position : _startPoint.position;
    }

    private void MoveSlug()
    {
        _transform.position = Vector3.MoveTowards(
            _transform.position,
            TargetPosition(),
            _speed * Time.deltaTime
        );
    }

}
