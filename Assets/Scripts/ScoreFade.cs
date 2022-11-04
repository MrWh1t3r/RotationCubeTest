using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreFade : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 2.0f;
    [SerializeField] private float speed = 2.0f;
    private TextMeshProUGUI _text;
    
    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Fade());
    }

    public IEnumerator Fade()
    {
        float fadeSpeed = 1.0f / fadeDuration;
        Color c = _text.color;

        for (float t = 0.0f; t <1.0f; t+= Time.deltaTime *fadeSpeed)
        {
            c.a = Mathf.Lerp(1, 0, t);
            _text.color = c;
            yield return true;
        }
        
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
