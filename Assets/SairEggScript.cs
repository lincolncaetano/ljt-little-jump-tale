using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SairEggScript : MonoBehaviour {
	public GameController controller;
	public GameObject teletransporte;
	private GameObject player;
	private bool voltarValido = false;
	float timeVolta = 1.5f;

	void Start(){
		controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	void Update(){
		if(player == null){
			player = GameObject.FindGameObjectWithTag("Player");
		}
		if(voltarValido && controller.cenaEgg){
			if(timeVolta > 0){
				timeVolta -= Time.deltaTime;
			}else{
				Vector3 vec = new Vector3(0, controller.destroyer.transform.position.y + 20, player.transform.position.z);
				if(player.GetComponent<PlayerController>() != null){
					player.GetComponent<PlayerController>().enabled = true;
				}else{
					player.GetComponent<Player_control>().enabled = true;
				}
				player.transform.position = vec;
				controller.cenaEgg = false;
				GameObject.FindGameObjectWithTag("CenaEgg").SetActive(false);
			}
		}
	}
	void OnTriggerEnter2D(Collider2D other) {

		if(controller != null && controller.currentState == GameController.GameStates.InGame){
			if(other.tag == "Player"){
				teletransporte.SetActive(true);
				voltarValido = true;
				if(player.GetComponent<PlayerController>() != null){
					other.GetComponent<PlayerController>().enabled = true;
				}else{
					other.GetComponent<Player_control>().enabled = true;
				}
			}
		}

	}
}
