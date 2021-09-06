using System.Collections;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;

#endif

public class DialogueManager : MonoBehaviour, IAnimatedUI, IInteractable {
    // Start is called before the first frame update
    [SerializeField] public CanvasGroup canvasGroup;
    public RectTransform transform;
    [SerializeField] private TextMeshProUGUI npcName;
    [SerializeField] private TextMeshProUGUI npcDescription;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject optionsPane;
    [SerializeField] private CanvasGroup optionsPaneGroup;
    [SerializeField] private GameObject optionPrefab;
    [SerializeField] private Vector2 hidePosition;
    [SerializeField] private Vector2 showPosition;
    [SerializeField] private bool fadeIn;
    [SerializeField] private bool moveIn;
    [SerializeField] private float time;
    [SerializeField] private float charactersPerSecond;
    [SerializeField] public DialogueManagerPosition currentPosition;

    private NPC npc;
    private Dialogue activeDialogue;

    [HideInInspector] public bool isMoving;
    [HideInInspector] public bool isAnimatingText;
    [HideInInspector] public UnityEvent hideEvent;
    private Coroutine animationRoutine;
    private bool canInteract;
    private bool selectedOption;
    private bool optionsAreShown;

    void Awake() {
        // NPCManager.Initialize();
        GameManager.dialogueManager = this;
        transform.anchoredPosition = hidePosition;
        currentPosition = DialogueManagerPosition.HiddenPosition;

        // SetNPC(NPCManager.npcs["Maya"]);
        if (fadeIn) {
            canvasGroup.alpha = 0;
        }

        canInteract = true;
        animationRoutine = null;
    }

    // Update is called once per frame
    public void Interact() {
        if (!canInteract) return;
        if (currentPosition == DialogueManagerPosition.HiddenPosition) {
            GameManager.playerMovement.input.Player.Disable();
            Show();
            return;
        }

        if (!optionsAreShown) {
            if (!isAnimatingText) {
                if (activeDialogue.IsLastLine() && selectedOption)
                    Hide();
                else 
                    animationRoutine = StartCoroutine(AnimateText(activeDialogue.GetNextLine(), 0));
            } else {
                StopCoroutine(animationRoutine);
                dialogueText.text = activeDialogue.GetCurrentLine();
                isAnimatingText = false;
                if (!selectedOption && activeDialogue.IsLastLine()) {
                    ShowOptions();
                }
            }
        }
    }

    public void Show() {
        activeDialogue = npc.ActivateDialogue();
        selectedOption = false;
        optionsAreShown = false;
        optionsPaneGroup.alpha = 0;
        dialogueText.text = "";
        foreach (Transform transform in optionsPane.transform) {
            Destroy(transform.gameObject);
        }

        activeDialogue.ResetLines();
        if (MovePosition(DialogueManagerPosition.ShownPosition)) {
            // GameManager.playerMovement.isInUI = true;
            if (activeDialogue != null && activeDialogue.HasNextLine() && !isAnimatingText) {
                animationRoutine = StartCoroutine(AnimateText(activeDialogue.GetNextLine(), 0.5f));
            }
        }
    }

    public void Hide() {
        activeDialogue?.ResetLines();
        activeDialogue = null;
        hideEvent.Invoke();
        GameManager.playerMovement.input.Player.Enable();
        MovePosition(DialogueManagerPosition.HiddenPosition);
    }

    private bool MovePosition(DialogueManagerPosition position) {
        if (isMoving) return false;
        if (!fadeIn && !moveIn) return false;
        if (currentPosition == position) return false;
        if (!moveIn) {
            transform.anchoredPosition = showPosition;
        }

        isMoving = true;
        currentPosition = position;
        Vector2 endPosition = position == DialogueManagerPosition.HiddenPosition ? hidePosition : showPosition;
        Vector2 startPosition = transform.anchoredPosition;
        float fadeStart = position == DialogueManagerPosition.HiddenPosition ? 1 : 0;
        if (fadeIn) {
            LeanTween.value(transform.gameObject, f => { canvasGroup.alpha = f; }, fadeStart, 1 - fadeStart, time)
                .setOnComplete(o => isMoving = false);
        }

        if (moveIn) {
            LeanTween.value(transform.gameObject,
                f => {
                    transform.anchoredPosition = new Vector2(Mathf.SmoothStep(startPosition.x, endPosition.x, f),
                        Mathf.SmoothStep(startPosition.y, endPosition.y, f));
                }, 0, 1, time).setOnComplete(o => isMoving = false);
        }

        return true;
    }


    public bool SetNPC(NPC npc) {
        if (npc == null) {
            activeDialogue = null;
            return true;
        }

        this.npc = npc;
        activeDialogue = npc.ActivateDialogue();
        if (activeDialogue == null) return false;
        npcName.text = npc.displayNames[GameManager.language];
        npcDescription.text = npc.descriptions[GameManager.language];
        selectedOption = false;
        dialogueText.text = "";
        return true;
    }

    private IEnumerator AnimateText(string text, float startWaitTime) {
        isAnimatingText = true;
        int totalCharacters = text.Length;

        yield return new WaitForSeconds(startWaitTime);
        for (int i = 0; i <= totalCharacters; i++) {
            dialogueText.text = text.Substring(0, i);
            yield return new WaitForSeconds(1 / charactersPerSecond);
        }

        if (!selectedOption && activeDialogue.IsLastLine()) {
            ShowOptions();
        }

        isAnimatingText = false;
    }


    private void ChooseOption(TextMeshProUGUI textMeshProUgui) {
        if (!canInteract) return;
        if (!activeDialogue.EndDialogue(textMeshProUgui.text)) {
            HideOptions(false);
            return;
        }

        HideOptions(true);
    }

    private void ShowOptions() {
        if (activeDialogue.options.Count == 0) {
            optionsAreShown = false;
            selectedOption = true;
            return;
        }

        canInteract = false;
        foreach (DialogueOption option in activeDialogue.options) {
            GameObject optionObject = Instantiate(optionPrefab, optionsPane.transform, false);
            TextMeshProUGUI text = optionObject.GetComponentInChildren<TextMeshProUGUI>();
            // GameManager.eventSystem.firstSelectedGameObject = optionObject;
            Button button = optionObject.GetComponent<Button>();
            button.onClick.AddListener(delegate { ChooseOption(text); });
            button.Select();
            text.text = option.displayStrings[GameManager.language];
        }

        optionsAreShown = true;
        selectedOption = true;
        LeanTween.value(gameObject, f => { optionsPaneGroup.alpha = f; }, 0, 1, time)
            .setOnComplete(delegate(object o) { canInteract = true; });
    }

    private void HideOptions(bool ableToPickOption) {
        optionsAreShown = false;
        foreach (Transform transform in optionsPane.transform) {
            Destroy(transform.gameObject);
        }

        if (ableToPickOption) {
            animationRoutine = StartCoroutine(AnimateText(activeDialogue.GetNextLine(), 0));
        } else {
            animationRoutine = StartCoroutine(AnimateText(npc.autoDeclineMessages[GameManager.language], 0));
            activeDialogue.ClearLines();
        }
    }

    public DialogueManagerPosition CurrentPosition {
        get => currentPosition;
        set => currentPosition = value;
    }


    public Vector2 HidePosition => hidePosition;

    public Vector2 ShowPosition => showPosition;
}


public enum DialogueManagerPosition {
    HiddenPosition,
    ShownPosition
}

#if UNITY_EDITOR
[CustomEditor(typeof(DialogueManager))]
internal class DialogueManagerCustomEditor : Editor {
    private DialogueManager manager;

    private void OnEnable() {
        manager = (DialogueManager) target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        manager.transform.anchoredPosition = manager.CurrentPosition == 0 ? manager.HidePosition : manager.ShowPosition;
        manager.canvasGroup.alpha = manager.CurrentPosition == DialogueManagerPosition.HiddenPosition ? 0 : 1;
    }
}
#endif