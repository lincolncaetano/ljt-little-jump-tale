using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatformSript : MonoBehaviour {

	public float smooth = 1.5f;         // The relative speed at which the camera will catch up.
	private Transform player;           // Reference to the player's transform.
	private Vector3 newPos;
	private GameController gc;

	void Update(){
		if(player == null){
			player = GameObject.FindGameObjectWithTag("Player").transform;
			//transform.parent = player;
		}
		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	void FixedUpdate ()
	{
		if(player != null){	
			if(player.position.y - 5.5f > transform.position.y){
				Vector3 newPos = new Vector3(transform.position.x, player.position.y - 5.5f , transform.position.z);
				transform.position = Vector3.Lerp(transform.position, newPos , smooth * Time.deltaTime);
			}
		}
	}
	 void OnTriggerEnter2D(Collider2D col)
    {	
		if(col.gameObject.tag.Equals("Player")){
			gc.currentState = GameController.GameStates.GameOver;
		}else if(col.GetComponent<ScripPlataforma>()!= null && !col.GetComponent<ScripPlataforma>().inicio){
			Destroy(col.gameObject);
		}
    }

	void OnCollisionEnter2D(Collision2D col)
    {	
		
		
    }
}
