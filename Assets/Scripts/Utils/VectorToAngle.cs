using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorToAngle : MonoBehaviour
{
    //���� �������� � ��������
    [SerializeField] private float _rotationAngle = 45;

    private void Start()
    {
        //��������� ���� �� �������� � �������
        float angleInRadians = _rotationAngle * Mathf.Deg2Rad;

        //������� ���������� �����, ������������� ������� �� ���� angleInRadians
        ComplexNumber rotationComplex = new ComplexNumber(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

        //������� ������2 ������� ������ ���������
        Vector2 vectorToRotate = new Vector2(1f, 0f); //��������, ������ (1,0)

        //������������ ������ ������2 � ���� ������������ �����
        ComplexNumber vectorComplex = new ComplexNumber(vectorToRotate.x, vectorToRotate.y);

        //�������� ���������� ���� (�������� � �������������
        ComplexNumber resultComplex = rotationComplex * vectorComplex;

        //����������� �������������� ���������� ������ ������� � ������2
        Vector2 rotatedVector = new Vector2(resultComplex.Real,resultComplex.Imaginary);
    }
}

public class ComplexNumber
{
    private float _real;
    private float _imagenary;

    public float Real => _real;
    public float Imaginary => _imagenary;

    public ComplexNumber(float real, float imagenary)
    {
        _real = real;
        _imagenary = imagenary;
    }

    public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
    {
        float realPart = a._real * b._real - a._imagenary * b._imagenary;
        float imagenaryPart = a._real * b._imagenary + a._imagenary * b._real;
        return new ComplexNumber(realPart, imagenaryPart);
    }

}
