using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorToAngle : MonoBehaviour
{
    //Угол поворота в градусах
    [SerializeField] private float _rotationAngle = 45;

    private void Start()
    {
        //переводим угол из градусов в радианы
        float angleInRadians = _rotationAngle * Mathf.Deg2Rad;

        //создаем компексное число, представяющее поворот на угол angleInRadians
        ComplexNumber rotationComplex = new ComplexNumber(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

        //создвем вектор2 который нужнно повернуть
        Vector2 vectorToRotate = new Vector2(1f, 0f); //Например, вправо (1,0)

        //представляем вектор вектор2 в виде комплексного числа
        ComplexNumber vectorComplex = new ComplexNumber(vectorToRotate.x, vectorToRotate.y);

        //Умножаем компексные чиса (поворота и представления
        ComplexNumber resultComplex = rotationComplex * vectorComplex;

        //преабразуем результирующий компексный вектор обратно в вектор2
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
