﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        [SerializeField] private FOV fieldOfView;

        public float speed;

        private Animator animator;
        public Vector2 dir = Vector2.zero;      

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Vector2 mov = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
                mov.x = -1;
                animator.SetInteger("Direction", 3);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                mov.x = 1;
                animator.SetInteger("Direction", 2);
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
                mov.y = 1;
                animator.SetInteger("Direction", 1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
                mov.y = -1;
                animator.SetInteger("Direction", 0);
            }

            dir.Normalize();
            fieldOfView.SetAimDirection(dir);
            animator.SetBool("IsMoving", mov.magnitude > 0);

            GetComponent<Rigidbody2D>().velocity = speed * mov;
        }
    }
}
