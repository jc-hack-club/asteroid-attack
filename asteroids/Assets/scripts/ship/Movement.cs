using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour{

    private float thrust = 4f;
    private float rotationSpeed = 180f;
    private float MaxSpeed = 5f;
    private Rigidbody2D rb;

    private void FixedUpdate()
    {
        rb = GetComponent<Rigidbody2D>();
        ControlRocket();
        CheckPosition();
    }


    private void ControlRocket()
    {
        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
        rb.AddForce(transform.up * thrust * Input.GetAxis("Vertical"));
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -MaxSpeed, MaxSpeed), Mathf.Clamp(rb.velocity.y, -MaxSpeed, MaxSpeed));
        transform.Find("flame").gameObject.GetComponent<Animator>().SetBool("moving",!Input.GetAxis("Horizontal").Equals(0) || !Input.GetAxis("Vertical").Equals(0));
        
    }

    private void OnDisable()
    {
        transform.Find("flame").gameObject.GetComponent<Animator>().SetBool("moving",false);

    }

    private void CheckPosition()
    {
        Camera mainCam = Camera.main;

        float sceneWidth = mainCam.orthographicSize * 2 * mainCam.aspect;
        float sceneHeight = mainCam.orthographicSize * 2;

        float sceneRightEdge = sceneWidth / 2;
        float sceneLeftEdge = sceneRightEdge * -1;
        float sceneTopEdge = sceneHeight / 2;
        float sceneBottomEdge = sceneTopEdge * -1;

        if (transform.position.x > sceneRightEdge)
        {
            transform.position = new Vector2(sceneLeftEdge, transform.position.y);
        }
        if (transform.position.x < sceneLeftEdge) { transform.position = new Vector2(sceneRightEdge, transform.position.y); }
        if (transform.position.y > sceneTopEdge)
        {
            transform.position = new Vector2(transform.position.x, sceneBottomEdge);
        }
        if (transform.position.y < sceneBottomEdge)
        {
            transform.position = new Vector2(transform.position.x, sceneTopEdge);
        }
    }
}