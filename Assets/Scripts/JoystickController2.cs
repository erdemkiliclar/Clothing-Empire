using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JoystickController2 : MonoBehaviour
{
    [SerializeField] Animator _animator;
    public DynamicJoystick dynamicJoystick;
    public float speed;
    public float turnSpeed;


    private void Update()
    {
        Run();
    }


    void JoystickMovement()
    {
        float horizontal = dynamicJoystick.Horizontal;
        float vertical = dynamicJoystick.Vertical;
        Vector3 addedPos = transform.forward * speed * Time.deltaTime;
        transform.position += addedPos;

        Vector3 direction = Vector3.forward * vertical + Vector3.right * horizontal;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
    }

    void Run()
    {
        if (Input.GetMouseButton(0))
        {
            if (GetComponent<CollectManager>().clothList.Count>0)
            {
                _animator.SetBool("_isCarrying",true);
                _animator.SetBool("_isRun",false);
                _animator.SetBool("_isCarry",false);
                _animator.SetBool("_isIdle",false);
            }
            else
            {
                _animator.SetBool("_isRun",true);
                _animator.SetBool("_isCarrying",false);
                _animator.SetBool("_isCarry",false);
                _animator.SetBool("_isIdle",false);
            }
            JoystickMovement();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (GetComponent<CollectManager>().clothList.Count>0)
            {
                _animator.SetBool("_isCarry",true);
                _animator.SetBool("_isRun",false);
                _animator.SetBool("_isCarrying",false);
                _animator.SetBool("_isIdle",false);
            }
            else 
            {
                _animator.SetBool("_isIdle",true);
                _animator.SetBool("_isCarry",false);
                _animator.SetBool("_isRun",false);
                _animator.SetBool("_isCarrying",false);
            }
        }
        
       
        
    }

}
