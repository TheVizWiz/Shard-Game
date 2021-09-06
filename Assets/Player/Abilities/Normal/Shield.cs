using System;
using UnityEngine;

public class Shield : MonoBehaviour {

    [SerializeField] private ShieldController shieldController;

    // Start is called before the first frame update
    private void Start() {

    }

    // updateQuest is called once per frame
    private void Update() {
    }

    public void OnCollisionEnter2D(Collision2D other) {
        shieldController.OnShieldCollision(other);
    }
}
