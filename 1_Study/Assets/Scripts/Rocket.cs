using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Components
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    // speeds
    [SerializeField] private float f_rcsThrush = 250.0f;
    [SerializeField] private float f_mainThrush = 50.0f;


    private float f_rotationThisFrame;
    
	void Start ()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }
	
	void Update ()
    {
        ProcessInput();
	}

    private void ProcessInput()
    {
        BoostRocket();
        RotateRocket();
    }

    private void BoostRocket()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddRelativeForce(Vector3.up * f_mainThrush);

            if (_audioSource.isPlaying == false)
            {
                _audioSource.Play();
            }
        }
        else
        {
            _audioSource.Stop();
        }
    }

    private void RotateRocket()
    {
        _rigidbody.freezeRotation = true;

        f_rotationThisFrame = f_rcsThrush * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * f_rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * f_rotationThisFrame);
        }

        _rigidbody.freezeRotation = false;
    }


}
