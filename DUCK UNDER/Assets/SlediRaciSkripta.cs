﻿using UnityEngine;
using System.Collections;

public class SlediRaciSkripta : MonoBehaviour {

	// Use this for initializationssssss
	public static Camera kamera;
	public GameObject raca;
	public float gorDol;

	float maxZ;

	Vector3 stalni;
	Vector3 poX;
	Vector3 kameraPoz;

	Vector3 startPoz;
    int ekran = 0;

    public GameObject unicevalka;
    public RandomCreatorSkripta mapCreator;

	void Start () {
		startPoz = transform.position;
        
		
		stalni = raca.transform.position - transform.position;
		poX = transform.position;
        if (gameObject.GetComponent<Camera>() != null)
        {
            kamera = gameObject.GetComponent<Camera>();
        }
        kameraPoz = transform.position;
		maxZ = transform.position.z;
        if (ScreenOrientation.Portrait == Screen.orientation)
        {
            ekran = 1;
        }
        else
        {
            ekran = 2;
        }
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
			hit = Physics.RaycastAll(ray);
			MeniSkripta.stejTocke=true;
			for (int i=0; i < hit.Length; i++) {

				if (hit [i].point != null && hit [i].collider.gameObject.CompareTag ("siroka")) {
					MeniSkripta.stejTocke=false;
					break;
				}
			}

            hit = Physics.RaycastAll(Camera.main.ViewportPointToRay(new Vector3(0, 1, 0)));
            for(int i=0; i < hit.Length; i++)
            {
                if (hit.Length < 2)
                {
                    Debug.Log(hit[i].collider.gameObject.tag + "top left");
                    mapCreator.dodajNoviElement();
                }
                
            }

            hit = Physics.RaycastAll(Camera.main.ViewportPointToRay(new Vector3(1, 0, 0)));
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].point != null && (hit[i].collider.gameObject.CompareTag("siroka") || hit[i].collider.gameObject.CompareTag("tla")))
                {
                    Debug.Log(hit[i].collider.gameObject.tag + "bot right");
                    unicevalka.transform.position = hit[i].point;
                }

            }
        }

        if(ScreenOrientation.Portrait == Screen.orientation)
        {
            if(ekran == 2)
            {
                portret();
                ekran = 1;
            }
        }
        else
        {
            if(ekran == 1)
            {
                ladscape();
                ekran = 2;
            }
        }
    }


    public void Reset()
    {
        transform.position = startPoz;
        if (gameObject.GetComponent<Camera>())
            kamera = gameObject.GetComponent<Camera>();
        stalni = raca.transform.position - transform.position;
        poX = transform.position;

        kameraPoz = transform.position;
        maxZ = transform.position.z;
    }

    public void youLostShow()
    {


    }

    void ladscape()
    {
        kamera.fieldOfView = 54;
        transform.rotation = Quaternion.Euler(66,90,0);
        postaviPobrisane();

    }

    void portret()
    {
        kamera.fieldOfView = 63;
        transform.rotation = Quaternion.Euler(54, 70, 0);
        postaviPobrisane();
    }

    void postaviPobrisane()
    {
        for(int i=0; i < RandomCreatorSkripta.zadnjihX.Length; i++)
        {
            if(RandomCreatorSkripta.zadnjihX[i] != null)
            {
                RandomCreatorSkripta.zadnjihX[i].SetActive(true);
                mapCreator.list.Insert(0, RandomCreatorSkripta.zadnjihX[i]);
            }
        }
        mapCreator.zaIzbrisatObjekt = null;
    }
}
