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
	//private float amout = 10;
	private float decrementoTempoInicial = 1.5f;
	private float decrementaTempo = 1.5f;

	public GameObject player;
	public BezierCurve curve;
	public BezierCurve curveErrada;

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

	public float levalWidth = 3f;
	public float minY = 1.2f;
	public float maxY = 3.9f;
	public GameObject itemEstrela;
	public GameObject itemRaio;
	public GameObject itemIma;
	public GameObject destroyer;

	public bool primeiroJogo = false;
	private float timerWatting = 0f;

	public bool cenaEgg;

	Camera cam;
	void Start () {
		
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		levalWidth = Camera.main.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, 5)).x;
		

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
			primeiroJogo = true;
		}

		GameObject obj = null;

		
		if(obj == null){
			obj = Instantiate(charsManager.retornoChar(PlayerPrefs.GetString("CharSelecionado"))) as GameObject;
		}


		obj.transform.position = GameObject.FindGameObjectWithTag("PlayerStart").transform.position;
		player = GameObject.FindGameObjectWithTag("Player");

		mainCam.GetComponent<CameraMovement>().SetPlayer(player.transform);


		currentState = GameStates.Menu;
		inicializaFase();

	}

	public void resetAfterEquip(){
		Destroy(GameObject.FindGameObjectWithTag("Player"));
		GameObject obj = Instantiate(charsManager.retornoChar(PlayerPrefs.GetString("CharSelecionado"))) as GameObject;
		
		obj.transform.position = GameObject.FindGameObjectWithTag("PlayerStart").transform.position;
		player = obj;

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
				
				SpawnPlataforma();

			}
		}


		if(nCenario == 2){
			neve.SetActive(true);
		}else if(nCenario == 1){
			neve.SetActive(false);
		}else{
			neve.SetActive(false);
		}
		
	}

	public void BtnReset(){
		score = 0;
		//amout = 10;
		nPlataforma = 0;
		validaGameOver = true;


		GameObject.FindGameObjectWithTag("Player").GetComponent<Player_control>().ResetPlayer();

		foreach(GameObject plataforma in GameObject.FindGameObjectsWithTag("Plataforma")){
			Destroy(plataforma);
		}
		foreach(GameObject plataforma in GameObject.FindGameObjectsWithTag("Item")){
			Destroy(plataforma);
		}

		destroyer.GetComponent<DestroyPlatformSript>().Reset();
		spawPosition = new Vector3();

		//cenario.transform.position = Vector3.zero;
		inicializaFase();

		player.transform.position = GameObject.FindGameObjectWithTag("PlayerStart").transform.position;

		currentState = GameStates.InGame;
		mainCam.GetComponent<CameraMovement>().ResetCamera();
	}
	
	// Update is called once per frame
	void Update () {

		if(currentState == GameStates.InGame){
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
		}else{
			Screen.sleepTimeout = SleepTimeout.SystemSetting;
		}

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

			if(timerWatting > 1.5){
				currentState = GameController.GameStates.InGame;
				Rigidbody2D rigi = player.GetComponent<Rigidbody2D>();
				if(rigi != null){
					Vector2 velocity = rigi.velocity;
					velocity.y = 12;
					rigi.velocity = velocity;
				}
			}
			timerWatting+= Time.deltaTime;

		}else if(currentState == GameStates.InGame){

			if(!player.GetComponent<Player_control>().especial){
				//amout = amout - decrementaTempo * Time.deltaTime;
			}
			
			if(score % 20 == 0 && score > scoreAnt){
				scoreAnt = score;
			}
			
			if(player.transform.position.y + 20  > spawPosition.y){
				SpawnPlataforma();
			}
		}else if(currentState == GameStates.GameOver){

			//audioS.Stop();

			best.text = PlayerPrefs.GetInt("BestScore").ToString();
			scoreGameOver.text = score.ToString();

			if(validaGameOver){

				best.text = PlayerPrefs.GetInt("BestScore").ToString();
				scoreGameOver.text = score.ToString();

				if(PlayerPrefs.GetInt("BestScore") < score){
					PlayerPrefs.SetInt("BestScore", score);
					PlayerServices.PostScore(score, LittleJumpTaleServices.leaderboard_ranking);
				}

				int totalScore = PlayerPrefs.GetInt("TotalScore") + score;
				PlayerPrefs.SetInt("TotalScore", totalScore );

				PlayerServices.IncrementAnchievment(LittleJumpTaleServices.achievement_collect_100_stars, score);


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

	public void BtnEspecial(int aux){
		for(int cont =0; cont < aux; cont ++){
			SpawnPlataforma();
			proximoPlat = proximoPlat.GetComponent<ScripPlataforma>().proximo;
		}
	}

	Vector3 spawPosition = new Vector3();
	public void SpawnPlataforma(){

		nPlataforma++;
		spawPosition.y += Random.Range(minY, maxY);
		spawPosition.x = Random.Range(-levalWidth, levalWidth);


        if(nCenario == 0){
			obj = Instantiate(platFloresta[Random.Range(0,platFloresta.Length)], spawPosition, Quaternion.identity );
		}else if(nCenario == 1){
			obj = Instantiate(platDeserto[Random.Range(0,platDeserto.Length)], spawPosition, Quaternion.identity );
		}else if(nCenario == 2){
			obj = Instantiate(platNeve[Random.Range(0,platNeve.Length)], spawPosition, Quaternion.identity );
		}else{
			obj = Instantiate(platCemiterio[Random.Range(0,platCemiterio.Length)], spawPosition, Quaternion.identity );
		}

        ScripPlataforma scrip = ultimo.GetComponent<ScripPlataforma>();
		scrip.proximo = obj;
		

        //obj.transform.position = topoAnt;
		obj.transform.parent = cenario.transform;
		ultimo = obj;
		

		float randValue = Random.value;
		if (randValue >= .95f) // 45% of the time
		{
			SpawRaio();
		}else if(randValue >= .9 & randValue <.95){
			SpawIma();
		}else{
			SpawEstrela();
		}
	}

	int contEstrela = 0;
	public void SpawEstrela(){
		contEstrela++;
		Vector3 spawPositionEstrela = spawPosition;
		
		spawPositionEstrela.y += Random.Range(2f, 5f);
		spawPositionEstrela.x = Random.Range(-levalWidth, levalWidth);
		GameObject estrela = Instantiate( itemEstrela , spawPositionEstrela, Quaternion.identity );
		if(contEstrela == 89){
			estrela.GetComponent<itemColl>().estrelaEgg = true;
		}

	}

	public void SpawRaio(){

		Vector3 spawPositionEstrela = spawPosition;
		spawPositionEstrela.y += Random.Range(2f, 5f);
		spawPositionEstrela.x = Random.Range(-levalWidth, levalWidth);
		Instantiate( itemRaio , spawPositionEstrela, Quaternion.identity );

	}

	public void SpawIma(){

		Vector3 spawPositionEstrela = spawPosition;
		spawPositionEstrela.y += Random.Range(2f, 5f);
		spawPositionEstrela.x = Random.Range(-levalWidth, levalWidth);
		Instantiate( itemIma , spawPositionEstrela, Quaternion.identity );

	}


	public void StartGame(){
		if(currentState == GameStates.Watting){
			currentState = GameStates.InGame;
		}
	}


	public void AddContScore(){
		score++;
	}

	private int partidas = 0;
	public void BtnWatting(){
		if(partidas < 2 ){
			currentState = GameStates.Watting;
		}else{
			currentState = GameStates.InGame;
			Rigidbody2D rigi = player.GetComponent<Rigidbody2D>();
			if(rigi != null){
				Vector2 velocity = rigi.velocity;
				velocity.y = 12;
				rigi.velocity = velocity;
			}
		}
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

}
