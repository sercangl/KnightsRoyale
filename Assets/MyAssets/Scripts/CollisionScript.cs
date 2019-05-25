using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{

    public ParticleSystem Effect;
    public AudioClip ses;
   

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag.Equals("Enemy"))
        {
            Effect.Play();
        }

       // if (gameObject.tag.Equals("Sw"))
       // {
      //      AudioClip audio;
          
     //   }

    }
}
