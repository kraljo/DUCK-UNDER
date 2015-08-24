using UnityEngine;
using System.Collections;

public class SlediRaciSkripta : MonoBehaviour {

	// Use this for initialization
	public static Camera kamera;
	public GameObject raca;
	public float gorDol;

	float maxZ;

	Vector3 stalni;
	Vector3 poX;
	Vector3 kameraPoz;

	Vector3 startPoz;
	void Start () {
		startPoz = transform.position;
		kamera = gameObject.GetComponent<Camera> ();
		stalni = raca.transform.position - transform.position;
		poX = transform.position;
		kameraPoz = transform.position;
		maxZ = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		if (raca) {
			Vector3 pozicija = raca.transform.position - stalni;
			if(pozicija.x > poX.x-gorDol && pozicija.x < poX.x + gorDol){
				kameraPoz.x = pozicija.x;
			}
			if(pozicija.z > maxZ){
				kameraPoz.z = pozicija.z;
				maxZ = pozicija.z;
			}
			transform.position=kameraPoz;
			Ray ray = new Ray(transform.position,transform.forward);
			Debug.DrawRay(transform.position, transform.forward, Color.green);
			
			RaycastHit[] hit;
			hit = Physics.RaycastAll (ray);
			MeniSkripta.stejTocke=true;
			for (int i=0; i < hit.Length; i++) {
				if (hit [i].point != null && hit [i].collider.gameObject.tag.Equals ("siroka")) {
					MeniSkripta.stejTocke=false;
					break;
				}
			}
		}
	}

	public void Reset(){
		transform.position = startPoz;
		kamera = gameObject.GetComponent<Camera> ();
		stalni = raca.transform.position - transform.position;
		poX = transform.position;
		kameraPoz = transform.position;
		maxZ = transform.position.z;

	}
}
