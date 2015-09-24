﻿using UnityEngine;
using System.Collections;

public class spawnVlakSkripta : MonoBehaviour {

	// Use this for initialization
	public float min=3;
	public float max=12;
	public GameObject objekt;
	

	public float speed=5;
	Collider terminator;
	float cas;
	
	
	GameObject prvi;
	public GameObject zadnji;
    int nalozeniAvti = 0;
    int stAvtov = 12;

    void Awake(){
        
        stAvtov = transform.parent.gameObject.GetComponent<izberiSpawnSkripta>().stAvtov;
        //transform.position += transform.forward * Random.Range (-zamik/2, zamik/2);

    }
	
	void Start () {
		terminator = transform.FindChild ("terminator").GetComponent<Collider> ();
		GameObject zacasna;
		prvi = Instantiate(objekt) as GameObject;
		zacasna = prvi;
		Physics.IgnoreCollision(zacasna.GetComponent<Collider>(), terminator);
		zacasna.transform.rotation = transform.rotation;
		zacasna.transform.position = transform.position;
		zacasna.transform.SetParent(transform.parent);
		zacasna.GetComponent<SkriptaPotujNaprej>().speed = speed;
		zacasna.GetComponent<SkriptaPotujNaprej>().pozicija = zacasna.transform.localPosition;
		zacasna.SetActive(false);
		
		for (int i=0; i < stAvtov; i++) {
			GameObject vozilo = Instantiate(objekt) as GameObject;
			Physics.IgnoreCollision(vozilo.GetComponent<Collider>(), terminator);
			vozilo.transform.rotation = transform.rotation;
			vozilo.transform.position = transform.position;
			vozilo.transform.SetParent(transform.parent);
			vozilo.GetComponent<SkriptaPotujNaprej>().speed = speed;
			vozilo.GetComponent<SkriptaPotujNaprej>().pozicija = vozilo.transform.localPosition;
			zacasna.GetComponent<SkriptaPotujNaprej>().nazaj = vozilo;
			zacasna = vozilo;
			zadnji = zacasna;
			
		}
		
		postaviVozila ();
		cas = vrniCas();
		
	}
	
	// Update is called once per frame
	void Update () {
		cas -= Time.deltaTime;
		if (cas <= 0) {
			GameObject zac = prvi;


			zac.transform.localPosition = zac.GetComponent<SkriptaPotujNaprej>().pozicija;
			prvi = zac.GetComponent<SkriptaPotujNaprej>().nazaj;
			
			zac.GetComponent<SkriptaPotujNaprej>().nazaj=null;
			cas = vrniCas();
			zac.SetActive(true);
			Physics.IgnoreCollision(zac.GetComponent<Collider>(), terminator);

		}
		
		
	}
	
	public void postaviVozila(){
		float vsota = 0;
		for (int i=0; i < 1; i++) {
			vsota = i * vrniCas();
			GameObject zac = prvi;

			zac.transform.localPosition = zac.GetComponent<SkriptaPotujNaprej>().pozicija;
			zac.transform.position += transform.forward*vsota;
			
			prvi = zac.GetComponent<SkriptaPotujNaprej>().nazaj;
			zac.SetActive(true);
			Physics.IgnoreCollision(zac.GetComponent<Collider>(), terminator);
			zac.GetComponent<SkriptaPotujNaprej>().nazaj=null;
			
			
		}
	}

	public float vrniCas(){
		return Random.Range (min,max);
	}
}
