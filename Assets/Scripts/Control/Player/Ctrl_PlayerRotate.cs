using UnityEngine;
using System.Collections;

public class Ctrl_PlayerRotate : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;

    private void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed, 0);
        }
    }
}