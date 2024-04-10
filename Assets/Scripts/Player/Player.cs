using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private Engine _engine;
    [SerializeField] private float _constantForsePover;
    [SerializeField] private CowCather _cowCather;

    private PlayerInpyt _playerInpyt;
    private Rigidbody _rigidbody;
    private Transform _transform;
    private ConstantForce _constantForce;

    private void Awake()
    {
        _transform = transform;
        _rigidbody =  GetComponent<Rigidbody>();
        _playerInpyt = gameObject.AddComponent<PlayerInpyt>();
        _constantForce = GetComponent<ConstantForce>();
        _cowCather.SetInpyt(_playerInpyt);
        _engine.Initialize(_rigidbody);
    }

    private void FixedUpdate()
    {
        _constantForce.force =  - Vector3.right * _playerInpyt.Controls.x * _constantForsePover + Physics.gravity * _rigidbody.mass;
    }

    private void Update()
    {
        var isVerticalAxisActive = !Mathf.Approximately(_playerInpyt.Controls.y, 0);

        if (isVerticalAxisActive)
        {
            _engine.SetAltityde(_engine.GetCurrentAltityde());
            _engine.SetOverrideControls(_playerInpyt.Controls.y);
        }

        _engine.Overrided = isVerticalAxisActive;
    }
}
