using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Manages enemy behaviour
/// </summary>
public class EnemyBehaviour : MonoBehaviour
{
    private float speed = 3.0f;
    private Vector3 lookDirection;
    private Rigidbody enemyRb;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameUI.isGamePaused)
        {
            lookDirection = (player.transform.position - transform.position).normalized;
            if (transform.position.y < -10)
            {
                Destroy(gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        enemyRb.AddForce(lookDirection * speed);
    }
}
