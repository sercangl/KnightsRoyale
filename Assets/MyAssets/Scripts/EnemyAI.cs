using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{


    public Transform target;
    public Transform myTransform;
    public int espeed = 2;

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(target);
        transform.Translate(Vector3.forward * espeed * Time.deltaTime);


    }

}
