using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private ScenesController _scenesController;
    [SerializeField] private Text _pressButtonTxt;
    [SerializeField] private float _buttonTransitionspeed;
    private bool _alphaIncreasing;

    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Return) ) {
            _scenesController.LoadFirstLevel();
        }
        DealWithFontAnimation();
    }
    
    private void DealWithFontAnimation()
    {
        Color color = _pressButtonTxt.color;
        float alpha = color.a;
        float delta = Time.deltaTime * _buttonTransitionspeed;

        alpha = _alphaIncreasing ? alpha + (delta * 1.5f) : alpha - delta;

        if(alpha <= 0f) {
            alpha = 0f;
            _alphaIncreasing = true;
        }
        else if(alpha > 1f) {
            alpha = 1f;
            _alphaIncreasing = false;
        }

        _pressButtonTxt.color = new Color(color.r, color.g, color.b, alpha);
    }
}
