using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalEggScript : MonoBehaviour {

	public GameController controller;
	public GameObject fogos;

	void Start(){
		controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		int egg1 = PlayerPrefs.GetInt("Egg1Idade");
		if(egg1 == 1 ){
			this.gameObject.SetActive(false);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) {

		if(controller != null && controller.currentState == GameController.GameStates.InGame){
			if(other.tag == "Player"){
				Destroy(this.gameObject);
				controller.AddContScore();
				int total = PlayerPrefs.GetInt("TotalGema");
				fogos.SetActive(true);
        		PlayerPrefs.SetInt("TotalGema", total + 5);
				PlayerPrefs.SetInt("Egg1Idade", 1);
			}
		}

	}
}
