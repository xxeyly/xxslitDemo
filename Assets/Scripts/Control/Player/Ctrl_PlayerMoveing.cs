using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_PlayerMoveing : MonoBehaviour
{
//    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Ctrl_PlayerAminator.Instance.SetHVValue(horizontal, vertical);
            moveDirection = new Vector3(horizontal, 0, vertical);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= 6 + (Ctrl_HeroProperty.Instance.GetCurrentDEX() * 0.01f);
//            transform.Rotate(0, Input.GetAxis("Horizontal") * 1, 0);
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        //如果垂直轴无变化,不走
        controller.Move(moveDirection * Time.deltaTime);

        if (Math.Abs(Input.GetAxis("Vertical")) > 0.01f)
        {
        }
    }
}