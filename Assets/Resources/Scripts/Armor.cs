using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Com
{
    public class Armor : MonoBehaviour
{
        PhotonAvatarView pa;
        void Start()
        {
            pa = new PhotonAvatarView();
        }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sharp"))
        {
                transform.parent.parent.parent.parent.GetComponent<PhotonAvatarView>().CollisionDetected(this);
            }

        }
}
}
