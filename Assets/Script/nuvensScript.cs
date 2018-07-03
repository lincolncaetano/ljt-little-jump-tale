using UnityEngine;
using System.Collections;

public class nuvensScript : MonoBehaviour {

	private GameObject sol;
	public float y;

	public void Start(){

		sol = GameObject.FindGameObjectWithTag("MainCamera");
		y = transform.position.y;

	}
	

	// Update is called once per frame
	void Update () {

		transform.position = new Vector2(transform.position.x,  sol.transform.position.y + y);

		float speed =  Random.Range(0, 5);


		if(speed < 0){
			speed *= -1;
		}

		transform.Translate(Vector3.left * speed * Time.deltaTime);
		
		
		if(transform.position.x < -15){
			Destroy(this.gameObject);
		}
		
	}
}
