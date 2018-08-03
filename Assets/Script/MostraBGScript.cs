using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostraBGScript : MonoBehaviour {

	public GameObject[] listaBG;
	GameController gc;

	// Use this for initialization
	void Start () {
		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < listaBG.Length; i++)
		{
			if(gc.cenaEgg != true){
				if(gc.nCenario == i){
					listaBG[i].SetActive(true);
				}else{
					listaBG[i].SetActive(false);
				}
			}else{
				listaBG[i].SetActive(false);
			}
		}
	}
}
