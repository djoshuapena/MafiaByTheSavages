using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class PhotonNetworkManager : MonoBehaviour
{
    private List<GameObject> roomPrefabs = new List<GameObject>();

    public static PhotonNetworkManager instance = null;
    public GameObject roomPrefab;
    public InputField roomName;
    public InputField maxCount;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject.transform);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //PhotonNetwork.ConnectUsingSettings("Version 0.1");
        Invoke("RefreshRoomList", 0.1f);
    }

    public void ButtonEvents(string EVENT)
    {
        switch (EVENT)
        {
            case "CreateRoom":
                if (PhotonNetwork.JoinLobby())
                {
                    RoomOptions RO = new RoomOptions();
                    RO.MaxPlayers = byte.Parse(maxCount.text);
                    PhotonNetwork.CreateRoom(roomName.text, RO, TypedLobby.Default);
                }
                break;
            case "RefreshButton":
                if (PhotonNetwork.JoinLobby())
                    RefreshRoomList();
                break;
            case "JoinRandomRoom":
                if (PhotonNetwork.JoinLobby()) Debug.Log("random join");
                    JoinRandomRoom();
                break;
        }
    }

    void RefreshRoomList()
    {
        if(roomPrefabs.Count > 0)
        {
            for(int i = 0; i < roomPrefabs.Count; i++)
            {
                Destroy(roomPrefabs[i]);
            }

            roomPrefabs.Clear();
        }

        for(int i = 0; i < PhotonNetwork.GetRoomList().Length; i++)
        {
            Debug.Log(PhotonNetwork.GetRoomList()[i].Name);

            GameObject g = Instantiate(roomPrefab);
            g.transform.SetParent(roomPrefab.transform.parent);

            g.GetComponent<RectTransform>().localScale = roomPrefab.GetComponent<RectTransform>().localScale;
            g.GetComponent<RectTransform>().localPosition = new Vector3(roomPrefab.GetComponent<RectTransform>().localPosition.x, roomPrefab.GetComponent<RectTransform>().localPosition.y - (i*80), roomPrefab.GetComponent<RectTransform>().localPosition.z);
            g.transform.FindChild("game_name").GetComponent<Text>().text = PhotonNetwork.GetRoomList()[i].Name;
            g.transform.FindChild("numPlayers").GetComponent<Text>().text = PhotonNetwork.GetRoomList()[i].PlayerCount + "/" + PhotonNetwork.GetRoomList()[i].MaxPlayers;
            g.transform.FindChild("ButtonJoinGame").GetComponent<Button>().onClick.AddListener(() => { JoinRoom(PhotonNetwork.GetRoomList()[i].Name); });
            g.SetActive(true);
            roomPrefabs.Add(g);
        }
    }

    void JoinRandomRoom()
    {
        if(PhotonNetwork.GetRoomList().Length>0)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            Debug.LogError("There are no games to join.");
        }
    }

    void JoinRoom(string roomName)
    {
        bool availableRoom = false;
        
        foreach (RoomInfo RI in PhotonNetwork.GetRoomList())
        {
            if(roomName == RI.Name)
            {
                availableRoom = true;
                break;
            }
            else
            {
                availableRoom = false;
            }
        }

        if(availableRoom)
        {
            PhotonNetwork.JoinRoom(roomName);
        }
        else
        {
            Debug.LogError("Room is unavailable.");
        }
    }

    //void OnGUI()
    //{
    //    GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    //}

    void OnJoinedLobby()
    {
        Debug.Log("Lobby Joined.");
        //Invoke("RefreshRoomList", 0.1f);
    }

    void OnPhotonJoinRoomFailed()
    {
        Debug.Log("Failed to join room.");
    }

    void OnPhotonJoinRoom()
    {
        Debug.Log("Room Joined.");
        SceneManager.LoadScene("GameRoom");
    }

    void OnCreatedRoom()
    {
        Debug.Log("Room Created.");
        //temporary fix, but isn't correct
        SceneManager.LoadScene("GameRoom");
    }

}