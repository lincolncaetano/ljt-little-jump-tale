using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public float smooth = 1.5f;         // The relative speed at which the camera will catch up.
	
	
	private Transform player;           // Reference to the player's transform.
	private Vector3 newPos;             // The position the camera is trying to reach.
	public GameController controller;

	
	void FixedUpdate ()
	{
		if(controller.currentState == GameController.GameStates.InGame){
			
			Vector3 newPos = new Vector3(transform.position.x, player.position.y + 1 , transform.position.z);
			transform.position = Vector3.Lerp(transform.position, newPos , smooth * Time.deltaTime);

		}
	}

	public void ResetCamera(){
		transform.position = new Vector3(0,0,-10);
	}

	public void SetPlayer(Transform player){
		this.player = player; 
	}

}
