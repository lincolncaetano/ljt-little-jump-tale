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

	[Range(1,10)]
	public float jumpVelocity;

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

		if(Input.GetButtonDown("Jump")){
			GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
		}


		if(controller.currentState == GameController.GameStates.GameOver){
            rigi.bodyType = RigidbodyType2D.Dynamic;
        }

		if(pula){
			progress += Time.deltaTime / duration;
			if (progress > 1f) {
				progress = 1f;
				pula = false;
			}

			if (progress > 0.7f) {

				if(!left){
					transform.rotation = Quaternion.Euler(0,180,0);
				}else{
					transform.rotation = Quaternion.Euler(0,0,0);
				}
			}
			transform.localPosition = curve.GetPoint(progress);

		}

		if(controller.currentState == GameController.GameStates.InGame){
            rigi.bodyType = RigidbodyType2D.Kinematic;
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

			GetComponent<BoxCollider2D>().size = new Vector2(1000,1);

			progress += Time.deltaTime / durationEspecial;
			if (progress > 1f) {
				progress = 1f;
				especial = false;
				anim.SetBool("special", false);
				animSpecial.SetBool("special", false);
				usoSpeceial--;
				GameObject.FindGameObjectWithTag("BtnSpecial").GetComponent<Button>().interactable = true;
				validaEspecial = true;
			}else{
				GameObject.FindGameObjectWithTag("BtnSpecial").GetComponent<Button>().interactable = false;
				validaEspecial = false;
			}
			transform.localPosition = controller.curve.GetPoint(progress);

		}else{

			if(sizeColl == Vector2.zero){
				sizeColl = GetComponent<BoxCollider2D>().size;
			}else{
				GetComponent<BoxCollider2D>().size = sizeColl;
			}

		}



	}


	private BezierCurve curve;
	
	public float duration;

	private float durationEspecial = 3f;
	
	private float progress;


	public void BtnRigth(){

		if(especial == false){

			audio.PlayOneShot(audioGoomo);

			if(progress > 0 && progress < 1){
				transform.localPosition = curve.GetPoint(1f);
			}

			curve = controller.ValidaPulo(false);
			pula = true;
			progress = 0;

			 if(controller.currentState == GameController.GameStates.Watting || controller.currentState == GameController.GameStates.InGame){


				anim.SetTrigger("jump");
				if(left){
					left = false;
				}
				
				controller.BtnRigth();
			}
		}

	}
	
	public void BtnLeft(){

		if(especial == false){

			audio.PlayOneShot(audioGoomo);

			if(progress > 0 && progress < 1){
				transform.localPosition = curve.GetPoint(1f);
			}

			curve = controller.ValidaPulo(true);
			pula = true;
			progress = 0;



			if(controller.currentState == GameController.GameStates.Watting || controller.currentState == GameController.GameStates.InGame){

				anim.SetTrigger("jump");
				if(!left){
					left = true;
				}
				controller.BtnLeft();
			}
		}
	}

	public void btnEspecial(){

		if(controller.currentState == GameController.GameStates.InGame && usoSpeceial > 0 && validaEspecial){

			audio.PlayOneShot(audioSpecial);

			pula = false;
			controller.BtnEspecial(especialContPla);
			especial = true;
			progress =0;
			anim.SetBool("special", true);
			animSpecial.SetBool("special", true);

		}


	}


	public void ResetPlayer(){
		anim.SetBool("dead", false);
		left = true;
        rigi.bodyType = RigidbodyType2D.Static;
        transform.rotation = Quaternion.Euler(0,0,0);
		usoSpeceial = 1;
		validaGameOver = true;
	}

	
}
