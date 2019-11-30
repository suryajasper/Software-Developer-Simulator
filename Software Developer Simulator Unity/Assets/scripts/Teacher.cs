using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class Teacher : MonoBehaviour
{
    private NavMeshAgent agent;
    //public Subject subject;
    //public Classroom classroom;
    public GameObject tmpParent;

    private TMP_Text talkOut;
    private Button talkForward;

    // Start is called before the first frame update
    void Start()
    {
        talkOut = tmpParent.transform.GetChild(0).GetComponent<TMP_Text>();
        talkForward = tmpParent.transform.GetChild(1).GetComponent<Button>();
        tmpParent = talkOut.gameObject.transform.parent.gameObject;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(clockScript.currentTime.minute);
        if (clockScript.currentTime.hour == 8 && clockScript.currentTime.minute == 0 && !tmpParent.activeSelf)
        {
            Debug.Log("SUCCESS");
            Talk(new string[] { "Welcome back to class students.", "I hate all of you." });
        }
    }
    private Collider [] FindCloseObjects(float radius)
    {
        return Physics.OverlapSphere(transform.position, 3f);
    }
    private void Talk(string [] words)
    {
        clockScript.PauseTime();
        Debug.Log("We're here");
        tmpParent.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        if (words.Length > 0)
        {
            talkOut.text = words[0];
            string[] tempWords = new string[words.Length - 1];
            for (int i = 1; i < words.Length; i++)
                tempWords[i-1] = words[i];
            talkForward.onClick.AddListener(delegate { Talk(tempWords); });
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            tmpParent.SetActive(false);
            clockScript.ResumeTime();
            return;
        }
    }
}
