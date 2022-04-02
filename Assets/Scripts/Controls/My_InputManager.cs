using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class My_InputManager : NetworkBehaviour
{
    public Camera cam;
    public PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {
        if (!IsLocalPlayer)
        {
            GetComponentInChildren<Camera>().enabled = false;
        } else
        {
            pc = GetComponent<PlayerController>();
            cam = GetComponentInChildren<Camera>();
        }
            
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsLocalPlayer)
        {
            CheckMovement();
            CheckFire();
            GetMouseInput();
        }
        
    }

    private void GetMouseInput()
    {
        Vector3 dir = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        pc.UpdateDirection(dir);
    }

    private void CheckFire()
    {
        if (Input.GetButton("Fire1")){
            pc.OnFired();
        }
    }

    private void CheckMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if(x != 0f || y != 0f)
        {
            pc.OnMove(x, y);
        }
    }
}
