﻿using UnityEngine;
using System.Collections;

public class posliIzbrisSkripta : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("unicevalka"))
        {
            
            other.gameObject.GetComponent<unicevalkaSkripta>().pobrisiZadnjega();
        }

    }
}
