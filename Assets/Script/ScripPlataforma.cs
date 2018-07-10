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

	void Start(){
		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	 void OnCollisionEnter2D(Collision2D col)
    {
		if(col.relativeVelocity.y <= 0f){
			Rigidbody2D rigi = col.collider.GetComponent<Rigidbody2D>();
			if(rigi != null){
				Vector2 velocity = rigi.velocity;
				velocity.y = jumpForce;
				rigi.velocity = velocity;
				for (int i = 0; i < 50; i++)
				{
					gc.SpawnPlataforma();
				}
			}
		}
    }
}
