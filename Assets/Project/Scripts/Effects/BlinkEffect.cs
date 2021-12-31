using UnityEngine;
using UnityEngine.UI;

public class BlinkEffect : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private float _transitionSpeed, _increaseSpeed;
    private bool _alphaIncreasing;

    void Update()
    {
        if(_text) StartBlink();
    }

    private float ColorAlpha()
    {
        float delta = Time.deltaTime * _transitionSpeed;
        float alpha = _text.color.a;

        return _alphaIncreasing
            ? alpha + (delta * _increaseSpeed)
            : alpha - (delta * (1 - _increaseSpeed));
    }

    private void StartBlink()
    {
        Color color = _text.color;
        float alpha = ColorAlpha();

        if (alpha <= 0f)
        {
            alpha = 0f;
            _alphaIncreasing = true;
        }
        else if (alpha > 1f)
        {
            alpha = 1f;
            _alphaIncreasing = false;
        }

        _text.color = new Color(color.r, color.g, color.b, alpha);
    }
}
