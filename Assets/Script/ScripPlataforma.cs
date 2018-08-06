using UnityEngine;
using System.Collections;

public class ScripPlataforma : MonoBehaviour {

	public int idPlat;
	public GameObject topo;
	public GameObject proximo;
	public bool left;
	public GameObject item;

	private GameController gc;

	private float jumpForce = 12f;
	public bool inicio = false;
	private bool pulou = false;

	void Start(){
		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	

	 void OnCollisionEnter2D(Collision2D col)
    {
		if(gc.currentState == GameController.GameStates.InGame && col.relativeVelocity.y <= 0f){
			Rigidbody2D rigi = col.collider.GetComponent<Rigidbody2D>();
			if(rigi != null){
				Vector2 velocity = rigi.velocity;
				velocity.y = jumpForce;
				rigi.velocity = velocity;
				pulou = true;
			}
		}
    }

	 void OnCollisionExit2D(Collision2D col)
    {
		if(gc.currentState == GameController.GameStates.InGame){
			Rigidbody2D rigi = col.collider.GetComponent<Rigidbody2D>();
			if(rigi != null && pulou){
				col.gameObject.GetComponent<Animator>().SetBool("jump", true);
				pulou = false;
			}
		}
    }

	void OnCollisionStay2D(Collision2D col){
		if(gc.currentState == GameController.GameStates.InGame && inicio){
			if(col.relativeVelocity.y <= 0f){
				col.gameObject.GetComponent<Animator>().SetBool("jump", true);
				Rigidbody2D rigi = col.collider.GetComponent<Rigidbody2D>();
				if(rigi != null){
					Vector2 velocity = rigi.velocity;
					velocity.y = jumpForce;
					rigi.velocity = velocity;
				}
			}
		}
	}
}
