using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com
{
    public class animationScript : MonoBehaviour
    {
        Animator m_animator;

        //int walkHash = Animator.StringToHash("Walk");
        // Start is called before the first frame update
        void Start()
        {
            m_animator = GetComponent<Animator>();

        }

        // Update is called once per frame
        void Update()
        {
            bool iswalk = false;
            // iswalk = Input.GetKey(KeyCode.Space);





            iswalk = OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp);
            m_animator.SetBool("walk", iswalk);





            /*
             * if (Input.GetKeyUp(KeyCode.T))
            {
                animator.SetTrigger(walkHash);
            }
            */
        }
    }
}
