using System;
using UnityEngine;

public class DamageBarrier : MonoBehaviour {
    
    private float distance;
    private Transform transform;
    // Start is called before the first frame update
    private void Start() {
        transform = gameObject.transform;
        distance = GameManager.playerMovement.transform.position.y - transform.position.y;
        SaveZoneHandler.barrier = this;
    }

    // updateQuest is called once per frame
    private void Update() {
        if (GameManager.playerMovement.transform.position.y - ((Component) this).transform.position.y > distance) {
            ResetPosition();
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(GameManager.Constants.PLAYER_TAG)) {
            SaveZoneHandler.RespawnAtLastZone(other.gameObject);
        }
    }

    public void ResetPosition() {
        var position = transform.position;
        position = new Vector3(position.x, GameManager.playerMovement.transform.position.y - distance,
            position.z);
        transform.position = position;
    }
}
