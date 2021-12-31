using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    [SerializeField] private Image _fadeImg;
    [SerializeField] private Color _initialColor, _finalColor;
    [SerializeField] private float _duration;

    void Start()
    {
        if(_fadeImg) StartCoroutine( StartFade() );
    }

    IEnumerator StartFade()
    {

        for (float time = 0f; time < _duration; time += Time.deltaTime)
        {
            float lerpTime = time / _duration;
            _fadeImg.color = Color.Lerp(_initialColor, _finalColor, lerpTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
