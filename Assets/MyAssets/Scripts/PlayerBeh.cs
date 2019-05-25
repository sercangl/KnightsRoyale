using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeh : MonoBehaviour {

    [Header("Attributes")]
    public float range = 15f;
    public int health = 200;

    [Header("Setup")]
    private Transform target;
    public string enemyTag = "Player";


    void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            /*Vector3 difference = target.position - transform.position;
            float rotationY = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, rotationY, 0.0f);*/

        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null)
            return;
        else
            return;
    }
 
    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Kill();
        }
    }

    void Kill()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
