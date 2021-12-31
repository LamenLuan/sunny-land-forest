using UnityEngine;

public class PlatformController : PointToPointAI
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject gameObject = collider.gameObject;

        switch(gameObject.tag) {
            case "Player": gameObject.transform.parent = this.transform; break;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        GameObject gameObject = collider.gameObject;

        switch(gameObject.tag) {
            case "Player": gameObject.transform.parent = null; break;
        }
    }
    
}
