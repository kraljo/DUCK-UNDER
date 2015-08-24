using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MeniSkripta : MonoBehaviour {

	// Use this for initialization
	public static bool stejTocke=false;
	public GameObject kamera;
	public GameObject meni;
	public GameObject hud;
	public GameObject loose;

	public Text bestScore;
	public Text Score;
	public Text stRack;

	float tocke;
	float deltaTocke;
	GameObject raca;
	void Awake(){
		hud.SetActive (false);
		loose.SetActive (false);
		deltaTocke = kamera.transform.position.z;
		tocke = 0;
		raca = GameObject.Find ("raca");
	}
	void Start () {
		bestScore.text = PlayerPrefs.GetInt ("bestScore")+"";
	}
	
	// Update is called once per frame
	void Update () {
		stRack.text = RackaSkripta.stRack+" / 10";
		if(stejTocke){
			tocke += (RackaSkripta.stRack / 10f)*(kamera.transform.position.z-deltaTocke);
		}
		deltaTocke = kamera.transform.position.z;

		Score.text = Mathf.RoundToInt (tocke)+"";
	}


	public void play(){
		tocke = 0;
		deltaTocke = kamera.transform.position.z;
		meni.SetActive (false);
		hud.SetActive (true);
		loose.SetActive (false);
		InputKey.enableI = true;
	}

	public void reset(){
		raca.SetActive (true);
		InputKey.enableI = false;
		hud.SetActive (false);
		loose.SetActive (false);
		meni.SetActive (true);
	}

	public void lost(){
		InputKey.enableI = false;
		hud.SetActive (false);
		loose.SetActive (true);
		if (!PlayerPrefs.HasKey ("bestScore") || PlayerPrefs.GetInt ("bestScore") < Mathf.RoundToInt (tocke)) {
			PlayerPrefs.SetInt ("bestScore", Mathf.RoundToInt (tocke));
			bestScore.text = Mathf.RoundToInt(tocke)+"";
		}
	}
}
