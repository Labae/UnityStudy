using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Components
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    [SerializeField] private float f_rotateSpeed = 5.0f;

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
            _rigidbody.AddRelativeForce(Vector3.up);

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
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * f_rotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * f_rotateSpeed * Time.deltaTime);
        }
    }


}
