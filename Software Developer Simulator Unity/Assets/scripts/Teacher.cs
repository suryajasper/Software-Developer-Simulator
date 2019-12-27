using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class Teacher : MonoBehaviour
{
    private NavMeshAgent agent;

    private TMP_Text talkOut;
    private Button talkForward;
    private Animator anim;

    public Classroom classroom;
    public GameObject tmpParent;
    public TMP_Text blackboard;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
            Vector3 destination = classroom.podium.transform.position;
            agent.SetDestination(new Vector3(destination.x, destination.y, destination.z + classroom.podium.GetComponent<Collider>().bounds.size.z));
        }
        anim.SetBool("moving", agent.hasPath);
        if (agent.hasPath && agent.remainingDistance < 0.25f)
        {
            agent.isStopped = true;
            Talk("Welcome back to class students.", "I hate all of you.");
        }

    }
    private Collider [] FindCloseObjects(float radius)
    {
        return Physics.OverlapSphere(transform.position, 3f);
    }
    private void WriteToBlackboard(string text)
    {
        agent.SetDestination(blackboard.transform.parent.gameObject.transform.position);
        anim.SetBool("isDrawing", true);
        blackboard.text = text;
    }
    private void Talk(params string [] words)
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
