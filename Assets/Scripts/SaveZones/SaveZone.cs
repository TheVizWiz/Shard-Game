using System;
using UnityEngine;

public class SaveZone : MonoBehaviour {



    // Start is called before the first frame update
    private void Start() {

    }

    // updateQuest is called once per frame
    private void Update() {

    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(GameManager.Constants.PLAYER_TAG)) {
            SaveZoneHandler.SetSafeZone(this);
        }
    }
}
