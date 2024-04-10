using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Engine : MonoBehaviour
{
    [HideInInspector]
    public bool Overrided = false;

    [Header("Spherecst")]
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _spherecastRadius;
    [SerializeField] private float _maxDistance;

    [Header("Lift")]
    [SerializeField] private float _maxForce;
    [SerializeField] private float _damping;

    private Transform _transform;
    private Rigidbody _targetBody;
    private float _dictance;
    private float _springSpeed;
    private float _lastDistance;
    private float _altitude;
    private float _inpytY;

    public float GetCurrentAltityde()
    {
        if (Physics.SphereCast(_transform.position, _spherecastRadius, _transform.forward, out RaycastHit hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
        {
            return hitInfo.distance;
        }

        return _maxDistance;
    }

    public void SetAltityde(float altityde)
    {
        _altitude = Mathf.Clamp(altityde, _spherecastRadius, _maxDistance);
    }

    public void Initialize(Rigidbody rigidbody)
    {
        _transform = transform;
        _targetBody = rigidbody;
    }

    private void FixedUpdate()
    {
        if (_targetBody == null)
            return;

        var forward = _transform.forward;

        if (Overrided)
        {
            ForseUpDown(forward);
        }
        else
        {
            Lift(forward);  
        }

        Damping();
    }

    private void ForseUpDown(Vector3 forward)
    {
        float forceFactor = (_inpytY > 0)?1:0;
        _targetBody.AddForce(-forward * Mathf.Max(forceFactor * _maxForce - _springSpeed * _maxForce * _damping, 0f), ForceMode.Force);
    }

    private void Lift(Vector3 forward)
    {
        if (Physics.SphereCast(_transform.position, _spherecastRadius, forward, out RaycastHit hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
        {
            _dictance = hitInfo.distance;

            var minForceHeight = _altitude + 1f;
            var maxForceHeight = _altitude - 1f;
            Mathf.Clamp(_dictance, maxForceHeight, minForceHeight);

            var forceFactor = Mathf.Clamp(_dictance, maxForceHeight, minForceHeight).Remap(maxForceHeight, minForceHeight, 1, 0);
            _targetBody.AddForce(-forward * Mathf.Max(forceFactor * _maxForce - _springSpeed * _maxForce * _damping, 0f), ForceMode.Force);
        }
    }

    private void Damping()
    {
        _springSpeed = (_dictance - _lastDistance) * Time.fixedDeltaTime;
        _springSpeed = Mathf.Max(_springSpeed, 0f);
        _lastDistance = _dictance;
    }

    internal void SetOverrideControls(float inpytY)
    {
        _inpytY = inpytY;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        var startPoint = transform.position;
        var endPoint = transform.position + transform.forward * _maxDistance;

        Gizmos.DrawWireCube(startPoint, Vector3.one * 0.2f);
        Gizmos.DrawLine(startPoint, endPoint);
        Gizmos.DrawSphere(endPoint, _spherecastRadius);
    }
}
