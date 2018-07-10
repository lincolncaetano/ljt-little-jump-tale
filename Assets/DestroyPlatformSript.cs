using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatformSript : MonoBehaviour {

	public float smooth = 1.5f;         // The relative speed at which the camera will catch up.
	private Transform player;           // Reference to the player's transform.
	private Vector3 newPos; 

	void Update(){
		if(player == null){
			player = GameObject.FindGameObjectWithTag("Player").transform;
			transform.parent = player;
		}
	}
	void FixedUpdate ()
	{
		if(player != null){
			
			Vector3 newPos = new Vector3(transform.position.x, player.position.y - 15 , transform.position.z);
			transform.position = Vector3.Lerp(transform.position, newPos , smooth * Time.deltaTime);

		}
	}
	 void OnTriggerEnter2D(Collider2D col)
    {	
		Destroy(col.gameObject);
    }
}
