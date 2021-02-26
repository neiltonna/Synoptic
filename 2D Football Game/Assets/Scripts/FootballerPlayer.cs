using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballerPlayer : MonoBehaviour
{
    [SerializeField] float speedOfMovement = 6f;
    float padding = 0.4f;
    float xLower;
    float xHigher;
    float yLower;
    float yHigher;

    void Start()
    {
        borderOfCamera();
    }

    void Update()
    {
        footballerMovement();
    }

    void footballerMovement()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speedOfMovement;
        var newXPosition = Mathf.Clamp(transform.position.x + deltaX, xLower, xHigher);
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speedOfMovement;
        var newYPosition = Mathf.Clamp(transform.position.y + deltaY, yLower, yHigher);

        transform.position = new Vector3(newXPosition, newYPosition, transform.position.z);
    }

    void borderOfCamera()
    {
        Camera gameCamera = Camera.main;
        xLower = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xHigher = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yLower = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yHigher = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
