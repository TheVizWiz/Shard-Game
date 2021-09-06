using System;
using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.Serialization;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class ProgressBar : MonoBehaviour, IProgressBar {
    
    [SerializeField] private float moveTime;
    [SerializeField] private Image backBar;
    [SerializeField] public float min;
    [SerializeField] public float max;
    [SerializeField] public float current;
    
    [HideInInspector] public bool hasWaitTime;
    [HideInInspector] public float waitTime;
    [HideInInspector] public Image frontBar;
    [HideInInspector] public ProgressBarType type;

    private float timeWaited, timeMoved;
    private bool isChanging, initializedWait;
    private float temporaryStartPoint;

    private float currentBarPosition;

    private IEnumerator currentCoRoutine;

    // Start is called before the first frame update
    void Start() {
        if (!hasWaitTime) {
        }
        if (!hasWaitTime && frontBar != null) {
            frontBar.gameObject.SetActive(false);
        }

        if (frontBar != null) {
            frontBar.fillAmount = (current - min) / (max - min);
        }

        backBar.fillAmount = (current - min) / (max - min);
    }

    // Update is called once per frame
    void Update() {
    }

    public void SetMax(float max) {
        this.max = max;
    }

    public void SetMin(float min) {
        this.min = min;
    }

    public void SetCurrentValue(float newVal) {
        // checkBounds = true;
        if ((newVal > max || newVal < min)) {
            print("new value out of bounds");
            return;
        }

        if (!isChanging) {
            currentCoRoutine = ChangeValues(newVal);
            StartCoroutine(currentCoRoutine);
            return;
        }

        if (hasWaitTime) {
            timeWaited = 0;
            initializedWait = false;
        }

        switch (type) {
            case ProgressBarType.TrailOnDecrease when currentBarPosition < newVal:
                StopCoroutine(currentCoRoutine);
                currentCoRoutine = ChangeValues(newVal);
                StartCoroutine(currentCoRoutine);
                break;
            case ProgressBarType.TrailOnDecrease:
                current = newVal;
                timeMoved = 0;
                break;
            case ProgressBarType.TrailOnIncrease when currentBarPosition > newVal:
                StopCoroutine(currentCoRoutine);
                currentCoRoutine = ChangeValues(newVal);
                StartCoroutine(currentCoRoutine);
                break;
            case ProgressBarType.TrailOnIncrease:
                current = newVal;
                timeMoved = 0;
                break;
        }
    }

    public float GetCurrentValue() {
        return current;
    }

    public IEnumerator ChangeValues(float newVal) {
        //current: always the current absolute value
        //temp: always the current changing value
        //newVal: what the current should be set to.
        
        temporaryStartPoint = current;
        currentBarPosition = current;
        current = newVal;
        float fill;
        timeMoved = 0;
        timeWaited = 0;
        isChanging = true;

        while (true) {

            if (!initializedWait && hasWaitTime) {
                initializedWait = true;
                switch (type) {
                    case ProgressBarType.TrailOnDecrease when temporaryStartPoint > newVal:
                        frontBar.fillAmount = (current - min) / (max - min);
                        break;
                    case ProgressBarType.TrailOnDecrease:
                        timeWaited = waitTime + 1;
                        break;
                    case ProgressBarType.TrailOnIncrease when temporaryStartPoint < newVal:
                        backBar.fillAmount = (current - min) / (max - min);
                        break;
                    case ProgressBarType.TrailOnIncrease:
                        timeWaited = waitTime + 1;
                        break;
                }
            }

            if (hasWaitTime && timeWaited < waitTime) {
                timeWaited += Time.deltaTime;
                yield return null;
            } else if (timeMoved < moveTime) {
                timeMoved += Time.deltaTime;
                
                if (!FloatComparer.AreEqual(current, newVal, float.Epsilon)) {
                    newVal = current;
                    temporaryStartPoint = currentBarPosition;
                } else {
                    currentBarPosition = Mathf.SmoothStep(temporaryStartPoint, current, timeMoved / moveTime);
                }

                
                fill = (currentBarPosition - min) / (max - min);

                switch (type) {
                    case ProgressBarType.TrailOnDecrease: {
                        backBar.fillAmount = fill;
                        if (temporaryStartPoint < newVal) {
                            frontBar.fillAmount = fill;
                        }

                        break;
                    }
                    case ProgressBarType.TrailOnIncrease: {
                        frontBar.fillAmount = fill;
                        if (temporaryStartPoint > newVal) {
                            backBar.fillAmount = fill;
                        }

                        break;
                    }
                }
                yield return null;
            } else {
                break;
            }

        }
        isChanging = false;
        initializedWait = false;


    }
    
    
    #if UNITY_EDITOR
    public void UpdateBarFromEditor(float min, float max, float current) {
        this.current = current;
        this.min = min;
        this.max = max;
        if (frontBar != null) {
            frontBar.fillAmount = (current - min) / (max - min);
        }
        if (backBar != null) {
            backBar.fillAmount = (current - min) / (max - min);
        }
    }
    #endif


}



#if UNITY_EDITOR
[CustomEditor(typeof(ProgressBar)), CanEditMultipleObjects]
public class HorizontalBarCustomEditor : Editor {    

    private SerializedProperty waitTime, hasWaitTime, instantBar, type;

    // private readonly string[] directions = {"Trail On Decrease", "Trail On Increase"};

    private void OnEnable() {
        waitTime = serializedObject.FindProperty("waitTime");
        hasWaitTime = serializedObject.FindProperty("hasWaitTime");
        instantBar = serializedObject.FindProperty("frontBar");
        type = serializedObject.FindProperty("type");
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        serializedObject.Update();

        EditorGUILayout.PropertyField(hasWaitTime, new GUIContent("Has Wait Time"));
        if (hasWaitTime.boolValue) {
            EditorGUILayout.PropertyField(type, new GUIContent("Trail Type"));
            EditorGUILayout.PropertyField(waitTime, new GUIContent("Wait Time"));
            EditorGUILayout.PropertyField(instantBar, new GUIContent("Front Bar"));
        }
        // ;
        // EditorGUILayout.Toggle()
        // bar.hasWaitTime = EditorGUILayout.Toggle("Has Wait Time", bar.hasWaitTime); 
        // if (bar.hasWaitTime) {
        //     EditorGUI.indentLevel++;1
        //     bar.waitTime = EditorGUILayout.FloatField("Wait Time", bar.waitTime);
        //     bar.instantBar = EditorGUILayout.ObjectField("Instant Moving Bar", bar.instantBar, typeof(Image), true) as Image;
        //     EditorGUI.indentLevel--;
        // }

        serializedObject.ApplyModifiedProperties();

    }
}
#endif


public enum ProgressBarType {
    TrailOnDecrease,
    TrailOnIncrease,
}