using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PositionStabilazer : MonoBehaviour
{
    [SerializeField] private float _stabelazerForce;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(-_rigidbody.position.z * Vector3.forward * _stabelazerForce, ForceMode.Force);
    }
}
