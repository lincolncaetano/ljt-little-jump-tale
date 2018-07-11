using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player_control : MonoBehaviour {

	public Animator anim;
	private Animator animSpecial;
	public bool left = true;
	private GameController controller;
	private bool pula = false;
	public bool especial = false;
	public Rigidbody2D rigi;
	private Vector2 sizeColl;
	public int especialContPla;
	public int usoSpeceial;
	public AudioClip audioGoomo;
	public AudioClip audioSpecial;
	public AudioClip audioOps;
    public AudioSource audio;

	private bool validaGameOver = true;
	private bool validaEspecial = true;


	[HideInInspector] public bool facingRight = true;
	private float moviment = 0f;
	public float movimentSpeed = 10f;

	void Start(){
		controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		animSpecial = GameObject.FindGameObjectWithTag("ShowSpecial").GetComponent<Animator>();

		audio = GetComponent<AudioSource>();
        audio.volume = 0.5f;
		audio.pitch = 3;

        usoSpeceial = 1;
	}

	
	void Update () {

		if(PlayerPrefs.GetInt("som") == 1){
			audio.enabled = true;
		}else{
			audio.enabled = false;
		}

		 #if UNITY_ANDROID
            moviment = Input.acceleration.x * movimentSpeed;
        #elif UNITY_IPHONE
            moviment = Input.acceleration.x * movimentSpeed;
        #else
            moviment = Input.GetAxis("Horizontal") * movimentSpeed;
        #endif

		
        
        


		if(controller.currentState == GameController.GameStates.GameOver){
            
        }

		if(controller.currentState == GameController.GameStates.InGame){
            if (usoSpeceial <= 0){
				GameObject.FindGameObjectWithTag("BtnSpecial").GetComponent<Button>().interactable = false;
			}else{
				GameObject.FindGameObjectWithTag("BtnSpecial").GetComponent<Button>().interactable = true;
			}
		}

		if(controller.currentState == GameController.GameStates.Watting){
			GameObject.FindGameObjectWithTag("BtnSpecial").GetComponent<Button>().interactable = false;
		}

		if(controller.currentState == GameController.GameStates.GameOver){
			if(validaGameOver){
				audio.PlayOneShot(audioOps);
				validaGameOver = false;
			}
		}

		if(especial){


		}else{

			if(sizeColl == Vector2.zero){
				sizeColl = GetComponent<BoxCollider2D>().size;
			}else{
				GetComponent<BoxCollider2D>().size = sizeColl;
			}

		}

	}

	void FixedUpdate()
    {

		if (controller.currentState == GameController.GameStates.InGame)
        {
			Vector2 velocity = rigi.velocity;
			velocity.x = moviment;
			rigi.velocity = velocity;

			if (moviment > 0 && !facingRight)
				Flip ();
			else if (moviment < 0 && facingRight)
				Flip ();
        }
        //float h = moviment;
    }

	void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }



	public void btnEspecial(){

		if(controller.currentState == GameController.GameStates.InGame && usoSpeceial > 0 && validaEspecial){

			audio.PlayOneShot(audioSpecial);

			pula = false;
			controller.BtnEspecial(especialContPla);
			especial = true;
			anim.SetBool("special", true);
			animSpecial.SetBool("special", true);

		}


	}


	public void ResetPlayer(){
		anim.SetBool("dead", false);
		left = true;
        transform.rotation = Quaternion.Euler(0,0,0);
		usoSpeceial = 1;
		validaGameOver = true;
	}

	
}
