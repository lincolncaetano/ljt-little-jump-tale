using UnityEngine;
using System.Collections;

public class itemColl : MonoBehaviour {

	
	public GameController controller;
	public bool estrelaEgg = false;

	void Start(){
		controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	void OnTriggerEnter2D(Collider2D other) {

		if(controller != null && controller.currentState == GameController.GameStates.InGame){
			if(other.tag == "Player"){
				Destroy(this.gameObject);
				controller.AddContScore();
				if(estrelaEgg){
					
					if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>() != null){
						PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
						player.timeImune = 5;
					}else{
						Player_control player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_control>();
						player.timeImune = 5;
					}
				}
			}
		}

	}
}
