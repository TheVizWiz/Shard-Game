using System;
using TMPro;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerMain : MonoBehaviour {

    [SerializeField] private float maxHealth;
    [SerializeField] private float maxMana;
    [SerializeField] private ProgressBar healthBar;
    [SerializeField] private ProgressBar manaBar;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI manaText;

    public SpriteRenderer spriteRenderer;
    public GameObject raycastPosition;


    public float health;
    public float mana;

    private float damageMultiplier;

    private AudioListener audioListener;

    private void Awake() {
        audioListener = GetComponent<AudioListener>();

    }

    void Start() {
        manaBar.max = maxMana;
        manaBar.min = 0;
        healthBar.max = maxHealth;
        healthBar.min = 0;
        manaBar.SetCurrentValue(maxMana);
        healthBar.SetCurrentValue(maxHealth);

    }
    
    public void Update() {
    }

    /// <summary>
    /// Tries to use a given amount of Mana
    /// </summary>
    /// <param name="amount">The amount of mana to use</param>
    /// <returns>Whether or not the use was successful. If unsuccessful, will not use any mana.</returns>
    public bool UseMana(float amount) {        
        if (mana < amount) return false;
        mana -= amount;
        if (mana < 0) mana = 0;
        UpdateManaUI();
        return true;
    }
    
    public void AddMana(float amount) {
        if (Mathf.Approximately(mana, maxMana)) return;
        if (mana + amount > maxMana) {
            mana = maxMana;
        } else {
            mana += amount;
        }
        UpdateManaUI();
    }

    public bool Damage(float amount) {
        if (amount > health) return false;
        health -= amount;
        if (health < 0) health = 0;
        UpdateHealthUI();
        return true;
    }

    public void AddHealth(float amount) {
        if (Mathf.Approximately(health, maxHealth)) return;
        if (health + amount > maxHealth) {
            health = maxHealth;
        } else {
            health += amount;
        }
        UpdateHealthUI();
    }

    public ProgressBar GetHealthBar() {
        return healthBar;
    }

    public ProgressBar GetManaBar() {
        return manaBar;
    }

    public void UpdateHealthUI() {
        // healthBar.SetMax(maxHealth);
        // manaBar.SetMax(maxMana);
        healthBar.SetCurrentValue(health);
        HealthText.text = (int) health + "/" + (int) maxHealth;
    }

    public void UpdateManaUI() {
        manaBar.SetCurrentValue(mana);
        ManaText.text = (int) mana + "/" + (int) maxMana;
    }

    public float MaxHealth {
        get => maxHealth;
        set => maxHealth = value;
    }

    public float MaxMana {
        get => maxMana;
        set => maxMana = value;
    }

    public TextMeshProUGUI ManaText => manaText;
    public TextMeshProUGUI HealthText => healthText;
}

#if UNITY_EDITOR
[CustomEditor(typeof(PlayerMain))]
internal class PlayerMainCustomEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        PlayerMain main = ((PlayerMain) target);
        main.GetHealthBar().UpdateBarFromEditor(0, main.MaxHealth, main.health);
        main.GetManaBar().UpdateBarFromEditor(0, main.MaxMana, main.mana);
        main.mana = Mathf.Min(Mathf.Max(0, main.mana), main.MaxMana);
        main.health = Mathf.Min(Mathf.Max(0, main.health), main.MaxHealth);
        main.ManaText.text = (int) main.mana + "/" + (int) main.MaxMana;
        main.HealthText.text = (int) main.health + "/" + (int) main.MaxHealth;
    }
}  
#endif

