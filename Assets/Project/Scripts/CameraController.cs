using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _offsetX, _smoothness,
    _upLimit, _downLimit, _leftLimit, _rightLimit;
    [SerializeField] private Transform _playerTransform;
    private float _playerX, _playerY;

    void FixedUpdate()
    {
        if(_playerTransform != null) {
            Vector3 playerPos = _playerTransform.position;
            float x = playerPos.x + _offsetX;
            _playerX = Mathf.Clamp(x, _leftLimit, _rightLimit);
            _playerY = Mathf.Clamp(playerPos.y, _downLimit, _upLimit);

            Vector3 cameraPos = transform.position;
            Vector3 newPos = new Vector3(_playerX, _playerY, cameraPos.z);
            transform.position = Vector3.Lerp(cameraPos, newPos, _smoothness);
        }
    }
    
}
