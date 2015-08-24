using UnityEngine;
using System.Collections;

public class OtrokSkripta : MonoBehaviour {

	// Use this for initialization
	public GameObject zasleduj;
	public GameObject zasledujeMe;
	public GameObject povozenaRaca;
	public GameObject osamljenaRaca;
	Vector3 smer;
	public float speed;

	public bool povozena=false;
	GameObject povozenOtrok;
	AudioSkripta audio;
	void Awake(){
		povozenOtrok = Instantiate (povozenaRaca) as GameObject;
		povozenOtrok.SetActive (false);
		audio = GameObject.Find("Audio").GetComponent<AudioSkripta>();
	}
	void Start () {
		smer = Vector3.zero;
		if(zasleduj)
			zasleduj.GetComponent<ZasledujeMeSkripta> ().ZasledujeMe = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (zasleduj) {

			smer = zasleduj.transform.position - transform.position;
			float step = speed*Time.deltaTime;

			Vector3 newDir = Vector3.RotateTowards(transform.forward,smer,60,0.0f);
			transform.rotation = Quaternion.LookRotation(newDir);
			if(Vector3.Distance(transform.position,zasleduj.transform.position) > 0.1f){
				transform.position += transform.forward * step;
			}else{
				//transform.RotateAround (zasleduj.transform.position, Vector3.up, 60 * Time.deltaTime);
			}
		}
	}



	void OnTriggerEnter(Collider other) {
		if (other.tag.Equals ("kolo")) {
			if(!povozena){
				RackaSkripta.stRack--;
				povozena=true;
				povozenOtrok.transform.position = transform.position;
				povozenOtrok.transform.rotation = transform.rotation;
				povozenOtrok.SetActive(true);
				audio.povozi();
				gameObject.SetActive(false);
			}
		} 
	}

	public void uniciOtroka(){
		Destroy (povozenOtrok);
	}
}
