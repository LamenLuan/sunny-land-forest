using UnityEngine;

public class PlatformController : PointToPointAI
{
    void Start()
    {
        _transform.position = _startPoint.position;
    }

    void Update()
    {
        if (HasValidPoints())
        {
            Move();
            if (_transform.position == TargetPosition())
            {
                _goingToEnd = !_goingToEnd;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject gameObject = collider.gameObject;

        switch (gameObject.tag)
        {
            case "Player": gameObject.transform.parent = this.transform; break;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        GameObject gameObject = collider.gameObject;

        switch (gameObject.tag)
        {
            case "Player": gameObject.transform.parent = null; break;
        }
    }
    
}
