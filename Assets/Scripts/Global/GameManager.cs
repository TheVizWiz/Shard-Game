using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager {
    public class Constants {
        public static int STANDABLE_LAYER = 8;
        public static int ENEMY_LAYER = 10;
        public static int PLAYER_LAYER = 9;
        public static int INTERACTABLE_LAYER = 11;

        public static float ERROR = 0.0000001f;
        public static float INPUT_ERROR = 0.01f;
        public static float AXIS_SENSE = 0.05f;


        public static readonly string STANDABLE_TAG = "Standable";
        public static readonly string INSTANT_DAMAGE_TAG = "InstantDamage";
        public static readonly string ENEMY_TAG = "Enemy";
        public static string PLAYER_TAG = "Player";
        public static string CARRYABLE_TAG = "Carryable";

        public static LayerMask ENEMY_LAYERMASK;
        public static LayerMask SLASHABLE_LAYERMASK;
        public static LayerMask CARRYABLE_LAYERMASK;
        public static LayerMask STRIKABLE_LAYERMASK;
        public static LayerMask DIVABLE_LAYERMASK;


        public static int LayerMaskToLayer(LayerMask layerMask) {
            int layerNumber = 0;
            int layer = layerMask.value;
            while (layer > 0) {
                layer >>= 1;
                layerNumber++;
            }

            return layerNumber - 1;
        }

        public static LayerMask LayerToLayerMask(int layer) {
            LayerMask mask = new LayerMask {value = layer};
            Debug.Log(layer + " " + mask.value);
            return mask;
        }
    }

    public static GameObject player;
    public static PlayerMain playerMain;
    public static PlayerMovement playerMovement;
    public static SceneAnimator sceneAnimator;
    public static LevelManager levelManager;
    public static CameraController cameraController;
    public static DialogueManager dialogueManager;
    public static List<string> preferencesList;

    public static int saveNumber = 0;
    public static bool isInitialized = false;
    public static bool isLoading;
    private static int oldIndex;


    public static string language = "en";


    public static IEnumerator LoadScene(int newIndex) {
        if (isLoading) yield break;
        else isLoading = true;

        oldIndex = levelManager.areaIndex;
        playerMovement.SwitchScenes();
        sceneAnimator.ExitScene();

        AsyncOperation operation = SceneManager.LoadSceneAsync(newIndex);
        operation.allowSceneActivation = false;


        while (operation.progress < 0.8 || sceneAnimator.IsTweening()) {
            yield return null;
        }

        // Debug.Log("finished load");
        operation.allowSceneActivation = true;
    }

    public static void EnterScene(LevelManager levelManager) {
        GameManager.levelManager = levelManager;
        sceneAnimator.EnterScene();

        foreach (EnterExitArea area in levelManager.areas) {
            if (area.nextSceneIndex == oldIndex) {
                levelManager.StartCoroutine(area.Enter());
                break;
            }
        }

        isLoading = false;
    }

    public static string GetPath() {
        return Path.Combine(Application.persistentDataPath, "Save" + saveNumber);
    }

    public static void WipeTempPreferences() {
        foreach (string s in preferencesList) {
            PlayerPrefs.DeleteKey(s);
        }
    }
}