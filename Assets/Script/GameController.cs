using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class GameController : MonoBehaviour {

	public CharsScript charsManager;
	public GameObject[] platFloresta;
	public GameObject[] platNeve;
	public GameObject[] platCemiterio;
	public GameObject[] platDeserto;

	private GameObject ultimo;
	public GameObject[] listaCenario;
	public int nCenario;
	public GameObject cenario;
	public GameObject proximoPlat;
	public GameObject mainCam;

	public GameStates currentState;
	public enum GameStates{Menu, Watting, InGame, InPause, GameOver, Shop};

	public int score = 0;
	public int scoreAnt = -1;
	public Text txtScore;
	public cenario_control cenarioCont;
	public Image health;

	//private bool criarPlataforma = true;

	public GameObject obj;
	private float amout = 10;
	private float decrementoTempoInicial = 1.5f;
	private float decrementaTempo = 1.5f;

	public GameObject player;
	public BezierCurve curve;
	public BezierCurve curveErrada;


	public btnActionScript btnCanvasRigth;
	public btnActionScript btnCanvasLeft;
	public btnActionScript btnCanvasEspecial;

	public GameObject btnNoAds;
	public GameObject btnNoAdsGameOver;
	private bool validaGameOver = true;


	public int nPlataforma;
	public GameObject neve;


	public Text best;
	public Text scoreGameOver;

	//public Text bestShop;
	public Text totalScoreShop;
	public Text totalGemaShop;

	public AudioSource audioS;


	//private Provider targetProvider = Provider.FACEBOOK;

	public GameObject panelClose;
	public GameOverScript gameOverScript;

	private int countChart;


	void Start () {

		charsManager = GameObject.FindGameObjectWithTag(CharsScript.tag).GetComponent<CharsScript>();
		//	PlayerPrefs.DeleteAll();

		if(PlayerPrefs.GetString("CharSelecionado") == ""){
			PlayerPrefs.SetString("CharSelecionado", CharsScript.CharSelect.AdvGil.ToString());
			PlayerPrefs.SetInt(SelectChar.prefixComprado+CharsScript.CharSelect.AdvGil.ToString(), 1);
		}
		if(PlayerPrefs.GetInt("primeiroJogo") == 0){
			PlayerPrefs.SetInt("primeiroJogo",1);
			PlayerPrefs.SetInt("musica", 1);
			PlayerPrefs.SetInt("som", 1);
		}

		GameObject obj = null;

		
		if(obj == null){
			obj = Instantiate(charsManager.retornoChar(PlayerPrefs.GetString("CharSelecionado"))) as GameObject;
		}


		obj.transform.position = GameObject.FindGameObjectWithTag("PlayerStart").transform.position;
		player = GameObject.FindGameObjectWithTag("Player");

		btnCanvasLeft.playerCont = player.GetComponent<Player_control>();
		btnCanvasRigth.playerCont = player.GetComponent<Player_control>();
		btnCanvasEspecial.playerCont = player.GetComponent<Player_control>();

		mainCam.GetComponent<CameraMovement>().SetPlayer(player.transform);


		currentState = GameStates.Menu;
		inicializaFase();

	}

	public void resetAfterEquip(){
		Destroy(GameObject.FindGameObjectWithTag("Player"));
		GameObject obj = Instantiate(charsManager.retornoChar(PlayerPrefs.GetString("CharSelecionado"))) as GameObject;
		
		obj.transform.position = GameObject.FindGameObjectWithTag("PlayerStart").transform.position;
		player = obj;

		btnCanvasLeft.playerCont = player.GetComponent<Player_control>();
		btnCanvasRigth.playerCont = player.GetComponent<Player_control>();
		btnCanvasEspecial.playerCont = player.GetComponent<Player_control>();

		mainCam.GetComponent<CameraMovement>().SetPlayer(player.transform);
	}


	
	private void inicializaFase(){

		nCenario = Random.Range(0, listaCenario.Length);
		cenario = listaCenario[nCenario];

		int aux1 = 0;
		foreach(GameObject g in listaCenario){
			if(aux1 == nCenario){
				g.SetActive(true);
			}else{
				g.SetActive(false);
			}
			aux1++;
		}


		for(int cont = 0; cont < 5; cont++){

			nPlataforma++;

			if(cont == 0){
				
				if(nCenario == 0){
					obj = Instantiate(platFloresta[Random.Range(0,platFloresta.Length)]) as GameObject;
				}else if(nCenario == 1){
					obj = Instantiate(platDeserto[Random.Range(0,platDeserto.Length)]) as GameObject;
				}else if(nCenario == 2){
					obj = Instantiate(platNeve[Random.Range(0,platNeve.Length)]) as GameObject;
				}else{
					obj = Instantiate(platCemiterio[Random.Range(0,platCemiterio.Length)]) as GameObject;
				}

				int lado = Random.Range(0, 2);
                if (lado == 1)
                {
                    obj.GetComponent<ScripPlataforma>().left = false;
                }
                else
                {
                    obj.GetComponent<ScripPlataforma>().left = true;
                }

                if (obj.GetComponent<ScripPlataforma>().left)
                {
                    obj.transform.localPosition = new Vector3(-1.9f, 0f, 0);
                }
                else {
                    obj.transform.localPosition = new Vector3(1.9f, 0f, 0);
                }

				obj.transform.parent = cenario.transform;
				ultimo = obj;
				proximoPlat = obj;
				
			}else{
				
				if(nCenario == 0){
					obj = Instantiate(platFloresta[Random.Range(0,platFloresta.Length)]) as GameObject;
				}else if(nCenario == 1){
					obj = Instantiate(platDeserto[Random.Range(0,platDeserto.Length)]) as GameObject;
				}else if(nCenario == 2){
					obj = Instantiate(platNeve[Random.Range(0,platNeve.Length)]) as GameObject;
				}else{
					obj = Instantiate(platCemiterio[Random.Range(0,platCemiterio.Length)]) as GameObject;
				}

                ScripPlataforma scrip = ultimo.GetComponent<ScripPlataforma>();
                scrip.proximo = obj;
                Vector3 topoAnt = scrip.topo.transform.localPosition;

                int lado = Random.Range(0, 2);
                if (lado == 1)
                {
                    obj.GetComponent<ScripPlataforma>().left = false;
                }
                else
                {
                    obj.GetComponent<ScripPlataforma>().left = true;
                }


                if (obj.GetComponent<ScripPlataforma>().left)
                {
                    obj.transform.localPosition = new Vector3(-1.9f, ultimo.transform.localPosition.y + 3.9f, 0);
                }
                else
                {
                    obj.transform.localPosition = new Vector3(1.9f, ultimo.transform.localPosition.y + 3.9f, 0);
                }

                //obj.transform.position = topoAnt;
                obj.transform.parent = cenario.transform;
                ultimo = obj;

			}
		}


		if(nCenario == 2){
			neve.SetActive(true);
		}else if(nCenario == 1){
			neve.SetActive(false);
		}else{
			neve.SetActive(false);
		}
		
		
		
		configuraCurva();
		configuraCurvaErrada();
	}

	public void BtnReset(){
		score = 0;
		amout = 10;
		nPlataforma =0;
		validaGameOver = true;
		decrementaTempo = decrementoTempoInicial;


		nCenario = Random.Range(0,3);
		cenario = listaCenario[nCenario];



		GameObject.FindGameObjectWithTag("Player").GetComponent<Player_control>().ResetPlayer();

		foreach(GameObject plataforma in GameObject.FindGameObjectsWithTag("Plataforma")){
			Destroy(plataforma);
		}


		cenario.transform.position = Vector3.zero;
		inicializaFase();

		player.transform.position = GameObject.FindGameObjectWithTag("PlayerStart").transform.position;

		currentState = GameStates.Watting;
		mainCam.GetComponent<CameraMovement>().ResetCamera();
	}
	
	// Update is called once per frame
	void Update () {
		txtScore.text = score.ToString();
		if(PlayerPrefs.GetInt("musica") == 1){
			audioS.enabled = true;
			if(!audioS.isPlaying){
				audioS.Play();
			}
		}else{
			audioS.enabled = false;
		}

		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKeyUp(KeyCode.Escape)) {
				panelClose.SetActive(true);
			}
		}

		if(currentState == GameStates.Menu){
			
		
		}else if(currentState == GameStates.Watting){

			
			if(!audioS.isPlaying){
				audioS.Play();
			}


			health.fillAmount = amout /20;
			health.color = Color.white;

		
		}else if(currentState == GameStates.InGame){

			if(!player.GetComponent<Player_control>().especial){
				amout = amout - decrementaTempo * Time.deltaTime;
			}
			health.fillAmount = amout /20;
			health.color = Color.white;

			if(amout <= 0){
				currentState = GameStates.GameOver;
			}



			if(score % 20 == 0 && score > scoreAnt){
				decrementaTempo += 0.25f; 
				scoreAnt = score;
			}




		}else if(currentState == GameStates.InPause){
			
		}else if(currentState == GameStates.GameOver){

			//audioS.Stop();

			best.text = PlayerPrefs.GetInt("BestScore").ToString();
			scoreGameOver.text = score.ToString();

			if(validaGameOver){

				best.text = PlayerPrefs.GetInt("BestScore").ToString();
				scoreGameOver.text = score.ToString();

				if(PlayerPrefs.GetInt("BestScore") < score){
					PlayerPrefs.SetInt("BestScore", score);
				}

				int totalScore = PlayerPrefs.GetInt("TotalScore") + score;
				PlayerPrefs.SetInt("TotalScore", totalScore );


				validaGameOver = false;
			}
			
		}else if(currentState == GameStates.Shop){

			//bestShop.text = PlayerPrefs.GetInt("BestScore").ToString();
			totalScoreShop.text = PlayerPrefs.GetInt("TotalScore").ToString();
			totalGemaShop.text = PlayerPrefs.GetInt("TotalGema").ToString();

		}

	}

	public void BtnCloseGame(){
		Application.Quit();
		return;
	}

	public void BtnCancelCloseGame(){
		panelClose.SetActive(false);
	}


	public void BtnRigth(){
		StartGame();
		SpawnPlataforma();

		configuraCurva();
		configuraCurvaErrada();
		
		if(proximoPlat.GetComponent<ScripPlataforma>().left){
			currentState = GameStates.GameOver;
		}else{
			proximoPlat = proximoPlat.GetComponent<ScripPlataforma>().proximo;
		}



	}


	
	public void BtnLeft(){
		StartGame();
		SpawnPlataforma();

		configuraCurva();
		configuraCurvaErrada();

		if(!proximoPlat.GetComponent<ScripPlataforma>().left){
			currentState = GameStates.GameOver;
		}else{
			proximoPlat = proximoPlat.GetComponent<ScripPlataforma>().proximo;
		}

	}

	public void BtnEspecial(int aux){


		for(int cont =0; cont < aux; cont ++){
			SpawnPlataforma();
			configuraCurva();
			proximoPlat = proximoPlat.GetComponent<ScripPlataforma>().proximo;
		}

	}

	public BezierCurve ValidaPulo(bool left){

		if(left && proximoPlat.GetComponent<ScripPlataforma>().left){
			return curve;
		}else if(!left && !proximoPlat.GetComponent<ScripPlataforma>().left){
			return curve;
		} else{
			return curveErrada;
		}
	}



	private void SpawnPlataforma(){

		nPlataforma++;

        if(nCenario == 0){
			obj = Instantiate(platFloresta[Random.Range(0,platFloresta.Length)]) as GameObject;
		}else if(nCenario == 1){
			obj = Instantiate(platDeserto[Random.Range(0,platDeserto.Length)]) as GameObject;
		}else if(nCenario == 2){
			obj = Instantiate(platNeve[Random.Range(0,platNeve.Length)]) as GameObject;
		}else{
			obj = Instantiate(platCemiterio[Random.Range(0,platCemiterio.Length)]) as GameObject;
		}

        ScripPlataforma scrip = ultimo.GetComponent<ScripPlataforma>();
		scrip.proximo = obj;
		Vector3 topoAnt = scrip.topo.transform.localPosition;

        int lado = Random.Range(0, 2);
        if (lado == 1)
        {
            obj.GetComponent<ScripPlataforma>().left = false;
        }
        else {
            obj.GetComponent<ScripPlataforma>().left = true;
        }


        if (obj.GetComponent<ScripPlataforma>().left)
        {
            obj.transform.localPosition = new Vector3(-1.9f, ultimo.transform.localPosition.y + 3.2f, 0);
        }
        else
        {
            obj.transform.localPosition = new Vector3(1.9f, ultimo.transform.localPosition.y + 3.2f, 0);
        }

        //obj.transform.position = topoAnt;
		obj.transform.parent = cenario.transform;
		ultimo = obj;
	}


	public void StartGame(){
		if(currentState == GameStates.Watting){
			currentState = GameStates.InGame;
		}
	}


	public void AddContScore(){
		amout = amout + 0.5f;
		if(amout > 20){
			amout = 20;
		}
		health.color = Color.black;
		score++;
	}

	public void BtnWatting(){
		currentState = GameStates.Watting;
	}

	public void BtnPause(){
		currentState = GameStates.InPause;
		Time.timeScale = 0f;
	}

	public void BtnShop(){
		BtnReset();
		currentState = GameStates.Shop;
	}

	public void BtnResume(){
		currentState = GameStates.InGame;
		Time.timeScale = 1f;
	}







	void configuraCurva(){
		curve.points[0] = player.transform.position;
		
		ScripPlataforma scripPlat = proximoPlat.GetComponent<ScripPlataforma>();
		Player_control scriptPlayer = player.GetComponent<Player_control>();
		
		if(scripPlat.left && scriptPlayer.left){
			curve.points[1] = new Vector3(player.transform.position.x, player.transform.position.y + 5,0);
		}else if(!scripPlat.left && !scriptPlayer.left){
			curve.points[1] = new Vector3(player.transform.position.x, player.transform.position.y + 5,0);
		}else{
			curve.points[1] = new Vector3(0, player.transform.position.y + 5,0);
		}
		
		
		curve.points[2] = proximoPlat.GetComponent<ScripPlataforma>().item.transform.position;
	}

	void configuraCurvaErrada(){

		curveErrada.points[0] = player.transform.position;
		
		ScripPlataforma scripPlat = proximoPlat.GetComponent<ScripPlataforma>();
		Player_control scriptPlayer = player.GetComponent<Player_control>();
		
		if(scripPlat.left && scriptPlayer.left){
			curveErrada.points[1] = new Vector3(player.transform.position.x, player.transform.position.y + 5,0);
		}else if(!scripPlat.left && !scriptPlayer.left){
			curveErrada.points[1] = new Vector3(player.transform.position.x, player.transform.position.y + 5,0);
		}else{
			curveErrada.points[1] = new Vector3(0, player.transform.position.y + 5,0);
		}


		Vector3 vecAux = proximoPlat.GetComponent<ScripPlataforma>().item.transform.position;
		if(scripPlat.left){
			curveErrada.points[2] = new Vector3(-vecAux.x, vecAux.y,  vecAux.z); 
		}

		curveErrada.points[2] = proximoPlat.GetComponent<ScripPlataforma>().item.transform.position;
	}

}
