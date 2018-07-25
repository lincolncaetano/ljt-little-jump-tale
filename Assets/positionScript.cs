using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionScript : MonoBehaviour {

	Camera cam;
	public Transform trans;
	public bool direita;
	// Use this for initialization
	void Start () {
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 worldPoint;
		if(direita){
			worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, 5));
			Vector3 pos = new Vector3(worldPoint.x  , worldPoint.y, 5);
			transform.position = pos;
		}else{
			worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 5));
			Vector3 pos = new Vector3(worldPoint.x - (2*GetComponent<BoxCollider2D>().size.x) , worldPoint.y, 5);
			transform.position = pos;
		}
	}
}
