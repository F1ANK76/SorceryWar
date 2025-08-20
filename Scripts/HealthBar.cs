using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public SpriteRenderer fillSprite;

    private Vector3 initialScale;

    void Start()
    {
        initialScale = fillSprite.transform.localScale;
    }

    public void SetHealth(float current, float max)
    {
        float ratio = Mathf.Clamp01(current / max);

        Vector3 scale = fillSprite.transform.localScale;
        scale.x = initialScale.x * ratio;
        fillSprite.transform.localScale = scale;
    }
}
