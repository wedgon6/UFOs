using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInpyt : MonoBehaviour
{
    private Vector2 _moveInpyt;

    public event Action CatchPressed;
    public event Action CatchReleased;

    public Vector2 Controls => _moveInpyt;

    private void Update()
    {
        _moveInpyt = Vector2.right * Input.GetAxis("Horizontal") + Vector2.up * Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CatchPressed?.Invoke();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            CatchReleased?.Invoke();
        }
    }
}
