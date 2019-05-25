using System.Collections;
using System.Collections.Generic;
using Com;
using UnityEngine;
using Photon.Pun;

namespace Com
{

    public class TestPlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{
    [Tooltip("Current Health of our player")]
    public float Health = 1f;

    /*
    //Can be removed!
    void Update()
    {
        if (Health <= 0f)
        {
            GameManager.Instance.LeaveRoom();
        }
    }
    */

    #region IPunObservable Implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(Health);
        }
        else
        {
            this.Health = (float)stream.ReceiveNext();
        }
    }

    #endregion
    void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (!other.gameObject.CompareTag("Sword"))
        {
            return;
        }

        Health -= 0.1f;
    }
    
}
}

