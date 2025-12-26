using System;
using UnityEngine;

public class JumpingBall : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float slowSpeed = 2f;
    [SerializeField] private float jumpForce = 5f;

    [Header("Visual")]
    [SerializeField] private Color slowColor = Color.red;
    [SerializeField] private Color fastColor = Color.white;

    private Rigidbody _body;
    private MeshRenderer _visual;

    private bool _wasBelowThreshold = false;
    private float _timeEnteredSlowState;


    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _visual = GetComponent<MeshRenderer>();

        _visual.material.color = fastColor;
    }

    void FixedUpdate()
    {
        CheckSpeed();
    }

    void CheckSpeed()
    {
        float speed = _body.linearVelocity.magnitude; 

        if (speed < slowSpeed)
        {
            if (!_wasBelowThreshold)
            {
                _wasBelowThreshold = true;
                _timeEnteredSlowState = Time.time;
                _visual.material.color = slowColor;
            }
        }
        else
        {
            if (_wasBelowThreshold)
            {
                _wasBelowThreshold = false;
                _visual.material.color = fastColor;

                float timeSpentSlow = Time.time - _timeEnteredSlowState;
                Debug.Log($"Объект двигался медленно в течение {Math.Round(timeSpentSlow, 2)} секунд");
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        _body.linearVelocity = Vector3.up * jumpForce;
    }
}
