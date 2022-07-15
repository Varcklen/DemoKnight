using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Image heartFill;

    [HideInInspector] public static GameObject healthBar;

    void Start()
    {
        healthBar = gameObject;
    }

    public void SetMaxHealth(float maxhealth, float health)
    {
        slider.maxValue = maxhealth;
        if (maxhealth < health)
            slider.value = maxhealth;
        else
            slider.value = health;

        fill.color = gradient.Evaluate(1f);
        heartFill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        heartFill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
