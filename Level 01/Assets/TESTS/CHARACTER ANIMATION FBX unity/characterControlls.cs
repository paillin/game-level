
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterControlls : MonoBehaviour
{
    static Animator anim;
    public float speed = 100.0f;
    public float rotationSpeed = 100.0f;




    void Start()
    {
        anim = GetComponent<Animator>();

    }

        void Update()
        {

            float translation = Input.GetAxis("Vertical") * speed;
            float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;
            transform.Translate(0, 0, translation);
            transform.Rotate(0, rotation, 0);


            if (Input.GetButtonDown("Jump"))
            {
                anim.SetTrigger("isJumping");
            }

            if (translation != 0)
            {
                anim.SetBool("isRunning", true);
                anim.SetBool("isIdle", false);
            }
            else
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }







        }
    }

    

