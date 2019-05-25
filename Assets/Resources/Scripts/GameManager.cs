using System;
using System.Collections;


using UnityEngine;
using UnityEngine.SceneManagement;


using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

namespace Com
{
    public class GameManager : MonoBehaviourPunCallbacks, IOnEventCallback
    {

        public static GameManager Instance;

        public void SpawnSceneObjects()
        {
            PhotonNetwork.InstantiateSceneObject("Swsespar2", new Vector3(-11.066f, -0.37f,1.248f), Quaternion.Euler(0f, 0f, 90f), 0);
            PhotonNetwork.InstantiateSceneObject("Swsespar2", new Vector3(-13.74f, -0.37f, 22.33f), Quaternion.Euler(0f, 0f, 90f), 0);
            PhotonNetwork.InstantiateSceneObject("Swsespar2", new Vector3(0.597f, -0.37f, 22.33f), Quaternion.Euler(90f, 0f, 0f), 0);


        }

        public void SpawnPlayer()
        {
            //VR Local
            //GameObject localAvatar = Instantiate((Resources.Load("OVRPLAYER")) as GameObject, new Vector3(UnityEngine.Random.Range(-5.0f, 5.0f), 2f, UnityEngine.Random.Range(-5.0f, 5.0f)), Quaternion.identity);
            //GameObject localAvatar = Instantiate((Resources.Load("OVRPLAYER")) as GameObject, new Vector3(2f, 2f, 2f), Quaternion.identity);
            //GameObject localAvatar = Instantiate(Resources.Load("OPEL")) as GameObject;
            //GameObject localAvatar = Instantiate(Resources.Load("OVRPLAYER")) as GameObject;
            GameObject localAvatar = Instantiate(Resources.Load("OVRPLAYERARMOR")) as GameObject;



            PhotonView photonView = localAvatar.GetComponent<PhotonView>();

            if (PhotonNetwork.AllocateViewID(photonView))
            {
                RaiseEventOptions raiseEventOptions = new RaiseEventOptions
                {
                    CachingOption = EventCaching.AddToRoomCache,
                    Receivers = ReceiverGroup.Others
                };

                SendOptions sendOptions = new SendOptions
                {
                    Reliability = true
                };

                PhotonNetwork.RaiseEvent(4, photonView.ViewID, raiseEventOptions, sendOptions);
            }
            else
            {
                Debug.LogError("Failed to allocate a ViewId.");

                Destroy(localAvatar);
            }

            //VR
        }

        void Start()
        {

            Instance = this;

            SpawnPlayer();

            SpawnSceneObjects();

        }

        //VR Remote
        public void OnEvent(EventData photonEvent)
        {
            if (photonEvent.Code == 4)
            {
                //GameObject remoteAvatar = Instantiate(Resources.Load("rem")) as GameObject;
                GameObject remoteAvatar = Instantiate(Resources.Load("remzırh")) as GameObject;
                PhotonView photonView = remoteAvatar.GetComponent<PhotonView>();
                photonView.ViewID = (int)photonEvent.CustomData;
            }
        }

        public override void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        public override void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        //VR

        #region Photon Callbacks



        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }

        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                //LoadArena();
            }
        }


        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                //LoadArena();
            }
        }

        #endregion

        #region Public Methods

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }


        #endregion


        #region Private Methods


        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            }
            Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
            //PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel("hazırmap");
            //PhotonNetwork.LoadLevel("Room for 2");



        }


        #endregion


    }
}