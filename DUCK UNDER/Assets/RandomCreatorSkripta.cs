using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomCreatorSkripta : MonoBehaviour {

	// Use this for initialization
	GameObject raca;

	public GameObject kmetija;

	public GameObject crte;
	public int verCesta=0;
	public GameObject cesta;

	public int verZeleznica=0;
	public GameObject zeleznica;

	public GameObject travaSiroka;

	public GameObject trava;
	public GameObject leviBreg;
	public GameObject desniBreg;

	public GameObject prviCesta;
	public GameObject zadnjiCesta;

	public GameObject prviZeleznica;
	public GameObject zadnjiZeleznica;

	public GameObject prviSiroka;
	public GameObject zadnjiSiroka;

	public GameObject prviCrte;
	public GameObject zadnjiCrte;

	public GameObject prviTrava;
	public GameObject zadnjiTrava;
	


	public static int nalozeno=0;
	GameObject prejsni;
	GameObject[] tabela;
	GameObject Kmetija;
	Vector3 vec;
	List<GameObject> list;
	int brisi=0;
	int stetjeTrave =0;
	int stTrav=0;


	bool vDelovanju=false;

	RandomVoziloSkripta randomVozilo;

	void Awake(){
		int skupaj = verCesta + verZeleznica;
		int sestevek = 0;
		randomVozilo = transform.FindChild ("randomVozilo").GetComponent<RandomVoziloSkripta>();
		
		raca = GameObject.Find ("raca");
		list = new List<GameObject>();
		vec = Vector3.zero;//transform.position;
		tabela = new GameObject[skupaj];
		for (int i=0; i < verCesta; i++) {
			tabela[i] = cesta;
			
		}
		sestevek += verCesta;

		for (int i=sestevek; i < sestevek+verZeleznica; i++) {
			tabela[i] = zeleznica;
			
		}
		sestevek += verZeleznica;

		prviCesta = Instantiate (cesta) as GameObject;
		prviCesta.transform.SetParent (transform);
		GameObject zacasna = prviCesta;

		for (int i=0; i < 10; i++) {
			GameObject inst = Instantiate(cesta) as GameObject;
			zacasna.GetComponent<nazajSkripta>().nazaj = inst;
			zacasna.GetComponent<nazajSkripta>().id = "cesta";
			inst.transform.SetParent(transform);
			zacasna = inst;

		}
		zacasna.GetComponent<nazajSkripta>().id = "cesta";
		zadnjiCesta = zacasna;



		prviZeleznica = Instantiate (zeleznica) as GameObject;
		prviZeleznica.transform.SetParent (transform);
		GameObject zacasna2 = prviZeleznica;

		for (int i=0; i < 10; i++) {
			GameObject inst = Instantiate(zeleznica) as GameObject;
			zacasna2.GetComponent<nazajSkripta>().nazaj = inst;
			zacasna2.GetComponent<nazajSkripta>().id = "zeleznica";
			inst.transform.SetParent(transform);
			zacasna2 = inst;

		}
		zacasna2.GetComponent<nazajSkripta>().id = "zeleznica";
		zadnjiZeleznica = zacasna2;

		prviSiroka = Instantiate (travaSiroka) as GameObject;
		prviSiroka.transform.SetParent (transform);
		GameObject zacasna3 = prviSiroka;

		for (int i=0; i < 10; i++) {
			GameObject inst = Instantiate(travaSiroka) as GameObject;
			zacasna3.GetComponent<nazajSkripta>().nazaj = inst;
			zacasna3.GetComponent<nazajSkripta>().id = "siroka";
			inst.transform.SetParent(transform);
			zacasna3 = inst;

		}
		zacasna3.GetComponent<nazajSkripta>().id = "siroka";
		zadnjiSiroka = zacasna3;

		prviCrte = Instantiate (crte) as GameObject;
		prviCrte.transform.SetParent (transform);
		GameObject zacasna4 = prviCrte;
		zacasna4.SetActive (false);
		for (int i=0; i < 10; i++) {
			GameObject inst = Instantiate(crte) as GameObject;
			zacasna4.GetComponent<nazajSkripta>().nazaj = inst;
			zacasna4.GetComponent<nazajSkripta>().id = "crte";
			inst.transform.SetParent(transform);
			zacasna4 = inst;
			zacasna4.SetActive (false);
		}
		zacasna4.GetComponent<nazajSkripta>().id = "crte";
		zadnjiCrte = zacasna4;

		prviTrava = Instantiate (trava) as GameObject;
		prviTrava.transform.SetParent (transform);
		GameObject zacasna5 = prviTrava;
		zacasna5.SetActive (false);
		for (int i=0; i < 10; i++) {
			GameObject inst = Instantiate(trava) as GameObject;
			zacasna5.GetComponent<nazajSkripta>().nazaj = inst;
			zacasna5.GetComponent<nazajSkripta>().id = "trava";
			inst.transform.SetParent(transform);
			zacasna5 = inst;
			zacasna5.SetActive (false);
		}
		zacasna5.GetComponent<nazajSkripta>().id = "trava";
		zadnjiTrava = zacasna5;

		Kmetija = Instantiate (kmetija);
		list.Add (Kmetija);
		nalozeno++;


	}

	public void StartPostavitev () {
		dodajElement(prviCesta,cesta);
		prejsni = cesta;
		for (int i=0; i < 4; i++) {
			GameObject spawn = tabela[Random.Range(0,tabela.Length)];
			if(spawn == prejsni && spawn == travaSiroka){
				spawn = cesta;
			}
			if(prejsni == spawn && spawn == cesta){
				dodajElement(prviCrte,crte);
			
			}else if(prejsni == spawn && spawn == zeleznica){
				dodajElement(prviTrava,trava);
				
			}else if(prejsni != travaSiroka && prejsni != spawn && spawn != travaSiroka){
				dodajElement(prviTrava,trava);
			}

			if(spawn == cesta){
				dodajElement(prviCesta,spawn);
			}
			else if(spawn == zeleznica){
				dodajElement(prviZeleznica,spawn);
			
			}else{ //if(spawn == travaSiroka){
				dodajElement(prviSiroka,spawn);
			}

			prejsni = spawn;
			stetjeTrave++;
		}
		vDelovanju = true;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (nalozeno + " nalozilo");
		if (nalozeno == 23) {
			StartPostavitev();
			nalozeno++;
		}

		if (raca && vDelovanju && list[list.Count-3].transform.position.z < raca.transform.position.z) {
			if(stetjeTrave >= 3){
				stetjeTrave=0;
				stTrav++;
				dodajElement(prviSiroka,travaSiroka);
				prejsni = travaSiroka;
			}else{
				GameObject spawn = tabela[Random.Range(0,tabela.Length)];
				stetjeTrave++;
				if(spawn == prejsni && spawn == travaSiroka){
					spawn = cesta;
				}
				if(prejsni == spawn && spawn == cesta){
					dodajElement(prviCrte,crte);
				
				}else if(prejsni == spawn && spawn == zeleznica){
					dodajElement(prviTrava,trava);
					
				}else if(prejsni != travaSiroka && prejsni != spawn && spawn != travaSiroka){
					dodajElement(prviTrava,trava);
				}

				if(spawn == cesta){
					dodajElement(prviCesta,spawn);
				}
				else if(spawn == zeleznica){
					dodajElement(prviZeleznica,spawn);
				
				}else{ //if(spawn == travaSiroka){
					dodajElement(prviSiroka,spawn);
				}
				prejsni = spawn;
			//Destroy(list[brisi++]);
			}

		}
		Debug.Log (list [0].transform.position.z);
		if (raca && vDelovanju && list[2].transform.position.z < raca.transform.position.z) {
			GameObject brisem = list[0];
			Debug.Log("prisem primerek");
			string id = brisem.GetComponent<nazajSkripta>().id;
			brisem.SetActive(false);
			if(id.Equals("cesta")){
				zadnjiCesta.GetComponent<nazajSkripta>().nazaj = brisem;
				zadnjiCesta = brisem;
			
			}else if(id.Equals("zeleznica")){
				zadnjiZeleznica.GetComponent<nazajSkripta>().nazaj = brisem;
				zadnjiZeleznica = brisem;
			}else if(id.Equals("siroka")){
				zadnjiSiroka.GetComponent<nazajSkripta>().nazaj = brisem;
				zadnjiSiroka = brisem;
				brisem.GetComponent<SirokaRandomSkripta>().reset();
			
			}else if(id.Equals("trava")){
				zadnjiTrava.GetComponent<nazajSkripta>().nazaj = brisem;
				zadnjiTrava = brisem;
			}else if(id.Equals("crte")){
				zadnjiCrte.GetComponent<nazajSkripta>().nazaj = brisem;
				zadnjiCrte = brisem;
			}
			list.RemoveAt(0);
		}
	}

	void dodajElement(GameObject spawn, GameObject spawnTabela){
		spawn.SetActive (true);
		Mesh mesh = spawn.GetComponent<MeshFilter>().sharedMesh;
		Bounds bounds = mesh.bounds;
		vec.z+= bounds.size.z*spawn.transform.localScale.z;
		Vector3 pozicija = spawn.transform.position;
		pozicija.z = vec.z - bounds.size.z/2f*spawn.transform.localScale.z;
		spawn.transform.localPosition = pozicija;


		if(spawnTabela == cesta){
			spawn.GetComponent<izberiSpawnSkripta>().nastaviSpawnInHitrost(stTrav*2);
			prviCesta = spawn.GetComponent<nazajSkripta>().nazaj;
		}
		else if(spawnTabela == zeleznica){
			prviZeleznica = spawn.GetComponent<nazajSkripta>().nazaj;
		
		}else if(spawnTabela == travaSiroka){
			spawn.GetComponent<SirokaRandomSkripta>().postavi();
			prviSiroka = spawn.GetComponent<nazajSkripta>().nazaj;
		}else if(spawnTabela == crte){
			prviCrte = spawn.GetComponent<nazajSkripta>().nazaj;
		}else if(spawnTabela == trava){
			prviTrava = spawn.GetComponent<nazajSkripta>().nazaj;
		}
		//GameObject zac = Instantiate (spawn, pozicija, Quaternion.Euler (0, 0, 0)) as GameObject;
		//zac.transform.SetParent (transform);
		Debug.Log ("dodaj element");
		list.Add(spawn);
	}

	public void pobrisiVse(){
		vDelovanju = false;
		for (int i=0; i < list.Count; i++) {
			GameObject brisem = list[i];
			Debug.Log("prisem primerek");
			string id = brisem.GetComponent<nazajSkripta>().id;
			brisem.SetActive(false);
			if(id.Equals("cesta")){
				zadnjiCesta.GetComponent<nazajSkripta>().nazaj = brisem;
				zadnjiCesta = brisem;
			
			}else if(id.Equals("zeleznica")){
				zadnjiZeleznica.GetComponent<nazajSkripta>().nazaj = brisem;
				zadnjiZeleznica = brisem;
			}else if(id.Equals("siroka")){
				zadnjiSiroka.GetComponent<nazajSkripta>().nazaj = brisem;
				zadnjiSiroka = brisem;
				brisem.GetComponent<SirokaRandomSkripta>().reset();
			
			}else if(id.Equals("trava")){
				zadnjiTrava.GetComponent<nazajSkripta>().nazaj = brisem;
				zadnjiTrava = brisem;
			}else if(id.Equals("crte")){
				zadnjiCrte.GetComponent<nazajSkripta>().nazaj = brisem;
				zadnjiCrte = brisem;
			}

		}
		list.Clear ();
		vec = Vector3.zero;
		Kmetija.SetActive (true);
		list.Add (Kmetija);
		StartPostavitev ();

	}

	public GameObject vrniRandomVozilo(){
		return randomVozilo.vrniVozilo ();
	}
}
