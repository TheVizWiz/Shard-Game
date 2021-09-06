using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ElectroLock : MonoBehaviour {
    
    [SerializeField] private Slider slider;
    [SerializeField] private float amountPerSecond;
    [SerializeField] private float acceptableSliderChargeDifference;
    [SerializeField] private float maxCharge;
    [SerializeField] private UnityEvent fullEvent;

    private float charge;
    
    
    // Start is called before the first frame update
    private void Start() {
        slider.minValue = 0;
        slider.maxValue = maxCharge;
        slider.value = 0;
        // if (!(Mathf.Abs( - slider.value)))
    }

    // updateQuest is called once per frame
    private void Update() {
        if (!(Mathf.Abs(charge - slider.value) < acceptableSliderChargeDifference)) {
            if (charge > slider.value) {
                slider.value += amountPerSecond * Time.deltaTime;
            } else {
                slider.value -= amountPerSecond * Time.deltaTime;
            }
        }
        //
        // if (slider.value < charge) {
        //     slider.value += amountPerSecond * Time.deltaTime;
        // }
        if (Mathf.Abs(maxCharge - slider.value) < acceptableSliderChargeDifference){
            fullEvent.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        ElectroBall ball = other.GetComponent<ElectroBall>();
        if (ball != null) {
            charge += ball.charge;
            ball.Use();
            Vector3 startScale = ball.transform.localScale;
            LeanTween.value(this.gameObject, f => { ball.transform.localScale = startScale * f;}, 1, 0, 0.5f)
                .setOnComplete(() => {
                    ball.gameObject.SetActive(false);
                });
        }
    }

    public void ResetFluid() {
        charge = 0;
    }
    
}