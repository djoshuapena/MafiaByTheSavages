using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaitingRoom : Photon.MonoBehaviour
{
    private List<GameObject> playerNamePrefabs = new List<GameObject>();
    //stores the information about the player
    //public Text playerNameText;
    //public Text playerIdText;
    public GameObject playerNamePrefab;

    //the minimum amount of players that can be in the game
    //private int minPlayers;

    //future expansion
    //YouText will say YOU next your name in the list of names
    //MasterText will say master next to the creater of the game in the list of names
    //public Text YouText;
    //public Text MasterText;


    //public GameObject UIList;

    //public GameObject PlayerItemPrefab;

    //public LobbyMenuController lmc; //lobby menu controller


    //Total time the waiting room wil stay open
   // float timeLeft = 30.0f;
   // public Text timer;

    // Use this for initialization
	bool gameStart = false;

	public Button startGame;
    public Button testUpdate;
	private PhotonView myPhotonView;

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{	
		//while (gameStart);
		if(stream.isWriting == true)
		{
			stream.SendNext (gameStart);
		    /*if (gameStart == true) {
				SceneManager.LoadScene ("GameScene");
			}*/
	    }
		else
		{
//			gameStart = (bool)stream.ReceiveNext();
			/*if (gameStart == true) {
				SceneManager.LoadScene ("GameScene");
		    }*/
		}
	}

	void Awake ()
	{
		if (startGame == null)
			Debug.Log ("Failed to initialize the DuskPhaseCanvas.");
		else
			startGame.gameObject.SetActive (false);
	}
		
    void Start()
    {
		myPhotonView = gameObject.GetComponent<PhotonView>();

        //minPlayers = PhotonNetwork.room.MaxPlayers / 2;
        //wait 3 seconds and refresh the playerlist
        InvokeRepeating("RefreshPlayerList", 0.1f, 3.0f);

		if(PhotonNetwork.isMasterClient == true)
		{
			startGame.gameObject.SetActive (true);
			//myPhotonView.RPC ("Started", PhotonTargets.AllBuffered);
		}
			

        //update the player count
        //playerCount = playerList();
    }

    public void OnGameStartButton()
    {
        if (PhotonNetwork.isMasterClient)
        {
            myPhotonView.RPC("Started", PhotonTargets.All);
        }

        //gameStart = true;
    }

   
	

    // Update is called once per frame
 //   void Update()
   // {
     //   if (gameStart==true)
       // {
         //   PhotonNetwork.LoadLevel("GameScene");
        //}
        //timing for the waiting room
        //if (PhotonNetwork.playerList.Length >= minPlayers)
        //{
          //  SceneManager.LoadScene("GameScene");
            //timeLeft -= Time.deltaTime;
            //timer.text = "Time Left:" + Mathf.Round(timeLeft);
       // }
        //else
        //{
          //  timeLeft = 30.0f;
            //timer.text = "Time Left:" + Mathf.Round(timeLeft);
        //}
        //if (timeLeft < 0 && PhotonNetwork.playerList.Length >= minPlayers)
        //{
            //join the JoinGame Room
            //lmc.JoinGameCanvasOn();
          //  timer.text = "Time is up";
        //}
   // }


    //getPlayer will get the users name and users ID
    //public void getPlayer()
    //{
    //    //PhotonNetwork.playerName = PlayerPrefs.GetString ("CurrUser"); //delete later
    //    //get the players name
    //    PlayerNameText.text = PhotonNetwork.playerName;
    //    //get the players ID
    //    //PlayerIdText.text = PhotonNetwork.player;

    //    //Find out which player in the list is you and which player is the Master
    //    //YouText.enabled = player.isLocal;
    //    //MasterText.enabled = player.isMasterClient;
    //}

    ////playerList will go through the list of players and get their names and id
    ////        it will also return the length of the Player List(the number of people in the game)
    //        public int playerList()
    //{
    //    for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
    //    {

    //        getPlayer(_player);

    //}
    //       return PhotonNetwork.playerList.Length;
    //   }

    void RefreshPlayerList()
    {
        if (playerNamePrefabs.Count > 0)
        {
            for (int i = 0; i < playerNamePrefabs.Count; i++)
            {
                Destroy(playerNamePrefabs[i]);
            }

            playerNamePrefabs.Clear();
        }

        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            //Debug.Log(PhotonNetwork.playerList[i]);

            GameObject g = Instantiate(playerNamePrefab);
            g.transform.SetParent(playerNamePrefab.transform.parent);

            g.GetComponent<RectTransform>().localScale = playerNamePrefab.GetComponent<RectTransform>().localScale;
            g.GetComponent<RectTransform>().localPosition = new Vector3(playerNamePrefab.GetComponent<RectTransform>().localPosition.x, playerNamePrefab.GetComponent<RectTransform>().localPosition.y - (i * 80), playerNamePrefab.GetComponent<RectTransform>().localPosition.z);
            g.transform.FindChild("Name").GetComponent<Text>().text = PhotonNetwork.playerList[i].ToString();
            //g.transform.FindChild("numPlayers").GetComponent<Text>().text = PhotonNetwork.GetRoomList()[i].PlayerCount + "/" + PhotonNetwork.GetRoomList()[i].MaxPlayers;
            //g.transform.FindChild("ButtonJoinGame").GetComponent<Button>().onClick.AddListener(() => { JoinRoom(g.transform.FindChild("game_name").GetComponent<Text>().text); });
            g.SetActive(true);
            playerNamePrefabs.Add(g);
        }
    }

    //return to Lobby with connection status: JoinedLobby
    public void BackButton()
	{
		//leave the photon waiting room, connection status: ConnectedToMaster
		PhotonNetwork.LeaveRoom();
    }

    //join lobby scene, connection status: JoinedLobby
    void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        SceneManager.LoadScene("Lobby");
    }

    [PunRPC]
    void Started()
    {
        SceneManager.LoadScene("GameScene");

    }
}
