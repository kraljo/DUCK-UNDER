using UnityEngine;
using System.Collections;

public class izberiSpawnSkripta : MonoBehaviour {

	// Use this for initialization
	public GameObject nazaj;
	int izbira;
	void Awake(){

		SpawnGameObjectSkripta[] spawn1 = GetComponentsInChildren<SpawnGameObjectSkripta> ();
		if (spawn1.Length > 0) {
			spawn1[0].enabled=false;
			spawn1[1].enabled=false;
			izbira = Random.Range (0, 2);
			spawn1 [izbira].enabled = true;
		} else {
			spawnColnSkripta[] spawn2 = GetComponentsInChildren<spawnColnSkripta> ();
			if(spawn2.Length > 0){
				spawn2 [Random.Range (0, 2)].enabled = false;
			}else{
				spawnVlakSkripta[] spawn3 = GetComponentsInChildren<spawnVlakSkripta> ();
				spawn3 [Random.Range (1, 2)].enabled = false;
			}
		}

	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void nastaviSpawnInHitrost(float povecava){
		SpawnGameObjectSkripta[] spawn1 = GetComponentsInChildren<SpawnGameObjectSkripta> ();
		spawn1[izbira].speed+= povecava;
	}
}
