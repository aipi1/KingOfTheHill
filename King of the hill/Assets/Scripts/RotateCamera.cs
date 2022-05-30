using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Handles camera rotation around Y-axis
/// </summary>
public class RotateCamera : MonoBehaviour
{
    private float rotationSpeed = 50;

    // Update is called once per frame
    void Update()
    {
        if (!GameUI.isGamePaused)
        {
            float horizontalInput = -Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime * horizontalInput);
        }
    }
}
