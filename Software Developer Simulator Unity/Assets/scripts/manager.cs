using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    public Player_Controller player;
    public GameObject clock;
    public List<Subject> classes;
    public displayGrades gradeDisplay;
    // Start is called before the first frame update
    void Start()
    {
        gradeDisplay.gameObject.SetActive(false);

        classes.Add(new Subject("AP Calculus AB"));
        classes.Add(new Subject("AP Euro"));
        classes.Add(new Subject("AP English Language"));
        classes.Add(new Subject("Spanish 3 Honors"));

        foreach( Subject subj in classes)
        {
            subj.addGrade("quizzes", "Quiz 1", "14/15");
            subj.addGrade("quizzes", "Quiz 2", "12/15");
            subj.addGrade("tests", "Test 1", "98/100");
            subj.addGrade("tests", "Test 2", "87/100");
            subj.addGrade("tests", "Test 3", "98/100");
            subj.addGrade("tests", "Test 4", "87/100");
            subj.addGrade("tests", "Test 5", "98/100");
            subj.addGrade("tests", "Test 6", "87/100");
            subj.addGrade("tests", "Test 7", "98/100");
            subj.addGrade("tests", "Test 8", "87/100");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            gradeDisplay.gameObject.SetActive(!gradeDisplay.gameObject.activeSelf);
            if (gradeDisplay.gameObject.activeSelf)
            {
                gradeDisplay.Display(classes);
                Cursor.lockState = CursorLockMode.None;
            }
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
