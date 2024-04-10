using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleVector : MonoBehaviour
{
    public Vector3 Vector => transform.forward * transform.localScale.magnitude;

    public void SetVector(Vector3 value)
    {
        Quaternion quaternion = Quaternion.identity;
        quaternion.SetLookRotation(value.normalized);

        transform.rotation = quaternion;
        transform.localScale = Vector3.one.normalized * value.magnitude;
    }

    private void OnDrawGizmos()
    {
        if (transform == null)
            return;

        Gizmos.color = Color.gray;
        Gizmos.DrawRay(transform.position, Vector);
    }
}
