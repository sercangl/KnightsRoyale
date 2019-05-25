using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com
{

    public class PlayerAnimManager : MonoBehaviourPun
    {

        private Animator animator;
        // Start is called before the first frame update
        void Start()
        {

            animator = GetComponent<Animator>();
            if (!animator)
            {
                Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
            {
                return;
            }

            if (!animator)
            {
                return;
            }

            bool iswalk = false;
            // iswalk = Input.GetKey(KeyCode.Space);

            iswalk = OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp);
            animator.SetBool("walk", iswalk);
        }
    }
}
