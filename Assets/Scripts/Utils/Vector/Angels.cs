using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angels : MonoBehaviour
{
    [SerializeField] private SimpleVector _vectorOne;
    [SerializeField] private SimpleVector _vectorTwo;

    [SerializeField] private SimpleVector _vectorAdditional;

    private void Update()
    {
        float angle = 0;

        angle = Vector3.Angle(_vectorOne.Vector, _vectorTwo.Vector);
        Quaternion rotation = Quaternion.AngleAxis(90, _vectorOne.Vector);
        Vector3 additional = rotation * Vector3.up;
        
       float sing = Mathf.Sign(Vector3.Dot(_vectorTwo.Vector, additional));
        angle *= sing;
        _vectorAdditional.SetVector(additional);
        gameObject.name = $"angle = {angle}:F2";


       //gameObject.name = $"angle = {Vector3.Angle(_vectorOne.Vector,_vectorTwo.Vector)}:F2";
       //gameObject.name = $"angle = {Vector3.SignedAngle(_vectorOne.Vector, _vectorTwo.Vector,Vector3.up)}:F2";
    }
}
