using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    public float speed = 3.0f;
    private Vector3 lookDirection;
    private Rigidbody enemyRb;
    private GameObject playerGoal;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerGoal = GameObject.Find("Goals/Player Goal");
        lookDirection = (playerGoal.transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        enemyRb.AddForce(lookDirection * speed);
    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal" || other.gameObject.name == "Player Goal")
        {
            Destroy(gameObject);
        } 
    }

}
