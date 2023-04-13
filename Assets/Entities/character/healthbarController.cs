using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class healthbarController: MonoBehaviour
{
    public Entity entity;
    public TrackableFloat wounds;

    private Slider slider;
    // Start is called before the first frame update
    void Start() {
        if ((slider = GetComponent<Slider>()) == null) {
            Debug.LogError("health bar requires a slider");
        }
        if (wounds == null) {
            Debug.LogError("health bar needs to have its wound float set");
        }
        slider.maxValue = entity.maxHealth;
        slider.value = entity.maxHealth;

        wounds.onChange.AddListener(WoundsChanged);
    }

    public void WoundsChanged() {
        slider.value = entity.maxHealth - entity.wounds;
    }
}
