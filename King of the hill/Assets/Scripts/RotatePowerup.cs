using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Manages floating and rotation of the Powerup
/// </summary>
public class RotatePowerup : MonoBehaviour
{
    private float rotationSpeed = 50.0f;
    private float floatingSpeed = 0.15f;
    private float upperYBound = 1.0f;
    private float lowerYBound = 0.5f;
    private bool movingUp = true;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // Slowly floating along Y-axis inside boundaries
        if (movingUp == true)
        {
            transform.Translate(Vector3.up * floatingSpeed * Time.deltaTime);
            if (transform.position.y >= upperYBound)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.Translate(Vector3.down * floatingSpeed * Time.deltaTime);
            if (transform.position.y <= lowerYBound)
            {
                movingUp = true;
            }
        }
    }
}
