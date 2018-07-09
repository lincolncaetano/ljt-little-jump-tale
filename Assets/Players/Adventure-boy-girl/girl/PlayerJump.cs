using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerJump : MonoBehaviour {

	[Range(1,10)]
	public float jumpVelocity;

	public bool grounded;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Translate(Input.acceleration.x, 0, 0);

		if(grounded){
			GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
		}
	}
}
