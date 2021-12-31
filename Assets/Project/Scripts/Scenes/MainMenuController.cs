using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private ScenesController _scenesController;

    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Return) ) {
            _scenesController.LoadFirstLevel();
        }
    }
}
