﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private int curr;

    public Camera [] cams;
    public RawImage crosshair;

    [Header ("Movement")]
    public float speed;

    void Start()
    {
        curr = 0;
        for (int i = 0; i < cams.Length; i++)
            cams[i].gameObject.SetActive(false);
        cams[0].gameObject.SetActive(true);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 scale = crosshair.rectTransform.localScale;
            crosshair.rectTransform.localScale = new Vector3(scale.x*1.25f, scale.y*1.25f, scale.z);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Vector3 scale = crosshair.rectTransform.localScale;
            crosshair.rectTransform.localScale = new Vector3(scale.x / 1.25f, scale.y / 1.25f, scale.z);
        }
        if (Input.GetKeyDown("c"))
        {
            cams[curr].gameObject.SetActive(false);
            if (++curr >= cams.Length)
                curr = 0;
            cams[curr].gameObject.SetActive(true);
        }
        float rawXForce = Input.GetAxis("Horizontal");
        float rawYForce = Input.GetAxis("Vertical");
        float xForce = rawXForce * (!Input.GetKey("tab") ? speed : speed * 1.5f);
        float yForce = rawYForce * (!Input.GetKey("tab") ? speed : speed * 1.5f);
        transform.Translate(new Vector3(xForce, 0, yForce));
        animator.SetFloat("speed", rawYForce);
        transform.rotation = Quaternion.Euler(0, cams[curr].GetComponent<cameraScript>().currentY, 0);
    }
    private GameObject GetRayCast(int range)
    {
        RaycastHit hit;
        if (Physics.Raycast(cams[curr].transform.position, cams[curr].transform.forward, out hit, range))
            return hit.transform.gameObject;
        return null;
    }
}
