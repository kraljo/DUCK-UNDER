using UnityEngine;
using System.Collections;

public class RandomVoziloSkripta : MonoBehaviour {



	public GameObject[] tabela;
	
	void Awake(){

	}
	void Start () {



	}
	
	public GameObject vrniVozilo(){
		return tabela[Random.Range(0,tabela.Length)];
	}
}
