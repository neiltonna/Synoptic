using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballerPlayer : MonoBehaviour
{
    [SerializeField] float speedOfMovement = 6f;
    [SerializeField] GameObject footballPrefab;
    [SerializeField] float projectileSpeed = 12f;
    [SerializeField] float projectileFiringTime = 2f;
    Coroutine shootRoutine;
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
        Shoot();
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

    void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shootRoutine = StartCoroutine(ShootContinously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(shootRoutine);
        }
    }
    IEnumerator ShootContinously()
    {
        while (true)
        {
            GameObject footballClone = Instantiate(footballPrefab, this.transform.position, Quaternion.identity);
            footballClone.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFiringTime);
        }
    }

}
