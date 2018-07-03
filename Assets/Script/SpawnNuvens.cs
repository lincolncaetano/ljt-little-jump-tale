using UnityEngine;
using System.Collections;

public class SpawnNuvens : MonoBehaviour {

	public GameObject nuvem;

	float timer = 0;

	GameObject randObject;
	float timeSpawn = 0;


    // Update is called once per frame
    void Update () {


        GameController gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (gc.currentState == GameController.GameStates.InGame) {
            timer += Time.deltaTime;

            if (timer > timeSpawn)
            {
                Vector3 vec = new Vector3(transform.position.x, Random.Range(-4F, 4F), 10f);


                float size = Random.Range(0.1f, 1f);
                nuvem.transform.localScale = new Vector3(size, size, size);

                GameObject gbo = Instantiate(nuvem, vec, Quaternion.identity) as GameObject;
                gbo.GetComponent<nuvensScript>().y = vec.y;
                timer = 0f;


                timeSpawn = Random.Range(0, 5);
            }
        }
	}
}
