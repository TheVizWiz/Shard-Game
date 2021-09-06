using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public List<GameObject> objects;
    public int numAlive;
    public bool hasToDie;
    public float timeBetweenSpawns;
    
    private new Transform transform;
    private float elapsedTime;
    private List<GameObject> activeObjects;

    // Start is called before the first frame update
    private void Start() {
        activeObjects = new List<GameObject>();
        elapsedTime = 0;
    }

    // updateQuest is called once per frame
    private void Update() {
        if (elapsedTime >= timeBetweenSpawns && (!hasToDie || activeObjects.Count < numAlive)) {
            activeObjects.Add(
                Instantiate(objects[Random.Range(0, objects.Count)], transform.position, Quaternion.identity));
            elapsedTime = 0;
        }

        foreach (GameObject gameObject in activeObjects) {
            if (gameObject == null) {
                activeObjects.Remove(gameObject);
            }
        }

    }
    
    
}