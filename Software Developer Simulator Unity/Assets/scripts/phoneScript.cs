using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class phoneScript : MonoBehaviour
{
    public float chargeEfficiency;
    public Slider battery;
    public TextMeshProUGUI charge;
    public TextMeshProUGUI timeText;

    private Animator anim;
    private Animator screenAnim;
    private int currentMode;

    private float timeUntilCharge;

    private bool isOn;

    void Start()
    {
        currentMode = 0;
        anim = GetComponent<Animator>();
        screenAnim = transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            timeUntilCharge += Time.deltaTime;
            if (Mathf.Abs(timeUntilCharge - chargeEfficiency) < 0.05f)
            {
                timeUntilCharge = 0;
                DecreaseCharge(1);
            }
        }
        if (Input.GetKeyDown("e"))
        {
            bool temp = anim.GetBool("on");
            isOn = !temp;
            Cursor.lockState = temp ? CursorLockMode.Locked : CursorLockMode.None;
            anim.SetBool("on", !temp);
            timeText.text = DateTime.Now.ToShortTimeString();
        }
    }
    void DecreaseCharge(int factor)
    {
        battery.value -= factor;
        Color c = battery.gameObject.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Image>().color;
        battery.gameObject.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Image>().color = new Color(c.r + 2.55f, c.g, c.b, c.a);
        charge.text = battery.value.ToString() + "%";
    }
    public void OnHomeButtonPressed()
    {
        if (currentMode == 0)
        {
            screenAnim.Play("toHome", 0);
            currentMode++;
            return;
        }
    }
    public void OnPowerButtonPressed()
    {
        if (currentMode == 1)
        {
            screenAnim.Play("toLock", 0);
            currentMode--;
            return;
        }
        if (currentMode == 0)
        {
            anim.SetBool("on", false);
        }
    }
}
