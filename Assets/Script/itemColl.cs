using UnityEngine;
using System.Collections;

public class itemColl : MonoBehaviour {

	
	public GameController controller;

	void Start(){
		controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	void OnTriggerEnter2D(Collider2D other) {

		if(controller.currentState == GameController.GameStates.InGame){
			if(other.tag == "Player"){
				Destroy(this.gameObject);
				controller.AddContScore();
			}
		}

	}
}
