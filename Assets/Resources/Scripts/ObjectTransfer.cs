using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Com
{

    public class ObjectTransfer : MonoBehaviour
    {
        private PhotonView photonView;

        public void Start()
        {
            photonView = GetComponent<PhotonView>();
        }

        void OnTriggerEnter(Collider other)
        {
            //  if (obtaincounter == 0) {
            //      this.photonView.RequestOwnership();
            //   //view.TransferOwnership(other.GetComponent<PhotonView>().ViewID);
            //  obtaincounter++;
            // }

            // if (obtaincounter == 0) {
            //  photonView.RequestOwnership();
            // //photonView.TransferOwnership(other.GetComponent<PhotonView>());
            // //photonView.TransferOwnership = other.GetComponent<PhotonView>().ViewID;
            //  obtaincounter++;
            // }

            if (photonView.Owner == null)
            {
                photonView.RequestOwnership();
            }

        }

    }
}
