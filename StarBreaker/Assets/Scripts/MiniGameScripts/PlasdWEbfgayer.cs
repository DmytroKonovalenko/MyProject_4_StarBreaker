﻿using UnityEngine;

public class PlasdWEbfgayer : MonoBehaviour
{
   
    public float strength = 5f;
    public float gravity = -9.81f;
    public float tilt = 5f;

 
    private Vector3 direction;
 

    private void Awake()
    {
       
    }

    private void Start()
    {
       
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            direction = Vector3.up * strength;
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        Vector3 rotation = transform.eulerAngles;
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;
    }

   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle")) {
            AdewgvcbweDsdf.Instance.GameOver();
        } else if (other.gameObject.CompareTag("Scoring")) {
            AdewgvcbweDsdf.Instance.IncreaseScore();
        }
    }

}
