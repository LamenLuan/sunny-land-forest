using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Return) ) {
            var scenesController = GetComponent<ScenesController>();
            scenesController.LoadFirstLevel();
        }
    }
}
