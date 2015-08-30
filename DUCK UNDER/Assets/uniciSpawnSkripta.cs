using UnityEngine;
using System.Collections;

public class uniciSpawnSkripta : MonoBehaviour {

	// Use this for initialization
	SpawnGameObjectSkripta spawn;

	void Awake(){
		SpawnGameObjectSkripta[] zac = transform.parent.parent.GetComponentsInChildren<SpawnGameObjectSkripta> ();
		if (zac [0].enabled) {
			spawn = zac [0];
		} else {
			spawn = zac[1];
		}

	}
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("vozilo")) {
			Debug.Log("unici");
			spawn.zadnji.GetComponent<SkriptaPotujNaprej>().nazaj = other.gameObject;
			spawn.zadnji = other.gameObject;
			other.gameObject.SetActive(false);
		}
	}
}
