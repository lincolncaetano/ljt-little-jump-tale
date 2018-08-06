using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyPlatformSript : MonoBehaviour {

	public float velocidade = 1.5f;         // The relative speed at which the camera will catch up.
	private GameObject player;           // Reference to the player's transform.
	private Vector3 newPos;
	private GameController gc;
	private float timerUp = 0;

	void Start(){
		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	void Update(){
		if(player == null){
			player = GameObject.FindGameObjectWithTag("Player");
		}
	}
	void FixedUpdate ()
	{
		if(player != null){	
			if(gc.currentState == GameController.GameStates.InGame && gc.cenaEgg == false){
				if(timerUp > 3){
					Vector3 newPos;
					if(player.transform.position.y - transform.position.y > 20){
						newPos = new Vector3(transform.position.x, player.transform.position.y + 5f , transform.position.z);
					}else{
						newPos = new Vector3(transform.position.x, transform.position.y + velocidade , transform.position.z);
					}
					transform.position = Vector3.Lerp(transform.position, newPos , Time.deltaTime);
				}else{
					timerUp+= Time.deltaTime;
				}

				if(gc.score > 50 && gc.score < 100 ){
					velocidade = 1.7f;
				}else if(gc.score > 100 && gc.score < 150){
					velocidade = 1.95f;
				}else if(gc.score > 150){
					velocidade = 2.05f;
				}else if(gc.score > 225){
					velocidade = 2.5f;
				}
			}
		}
	}
	 void OnTriggerEnter2D(Collider2D col)
    {	
		if(col.gameObject.tag.Equals("Player")){
			if(player.GetComponent<PlayerController>() != null){
				if(player.GetComponent<PlayerController>().timeImune > 0){
					GameObject.FindGameObjectWithTag("CenaEgg").SetActive(true);
					GameObject.FindGameObjectWithTag("CenaEgg").GetComponent<ScenneEggScript>().StartEgg();
				}else{
					gc.currentState = GameController.GameStates.Offertas;
					player.GetComponent<PlayerController>().AudioLose();
				}
			}else{
				if(player.GetComponent<Player_control>().timeImune > 0){
					GameObject.FindGameObjectWithTag("CenaEgg").SetActive(true);
					GameObject.FindGameObjectWithTag("CenaEgg").GetComponent<ScenneEggScript>().StartEgg();
				}else{
					gc.currentState = GameController.GameStates.Offertas;
					player.GetComponent<Player_control>().AudioLose();
				}
			}
		}else if(col.GetComponent<ScripPlataforma>()!= null && !col.GetComponent<ScripPlataforma>().inicio){
			Destroy(col.gameObject);
		}

		if(col.gameObject.tag.Equals("Item")){
			Destroy(col.gameObject);
		}
    }

	public void Reset(){
		Vector3 posicaoInicial = new Vector3(0, -4.94f, 0);
		transform.position = posicaoInicial;
		timerUp = 0f;
		velocidade = 1.5f; 
	}

}
