using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{

    public AudioClip SwordCollision1;
    public AudioSource CollisionSource1;

    // Use this for initialization
    void Start()
    {
        CollisionSource1.clip = SwordCollision1;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            CollisionSource1.Play();
        }


    }
}
