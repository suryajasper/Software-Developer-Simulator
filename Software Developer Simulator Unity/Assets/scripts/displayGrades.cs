using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class displayGrades : MonoBehaviour
{
    public GameObject buttonToInstantiate;
    public GameObject model;
    public GameObject catToInstantiate;
    public GameObject modelCat;
    public GameObject gradeToInstantiate;
    public GameObject modelGrade;

    public Transform parent;

    public float offset;
    List<Button> buttons;

    public void Display(List<Subject> classes)
    {
        if (buttons != null)
            Clear();
        else
            buttons = new List<Button>();
        foreach (Subject subject in classes)
        {
            Button button = Instantiate(buttonToInstantiate, parent).GetComponent<Button>();
            if (buttons.Count > 0)
            {
                button.transform.position = model.transform.position + new Vector3(0, (-button.GetComponent<RectTransform>().rect.height-offset)*buttons.Count, 0);
            }
            else
            {
                button.transform.position = model.transform.position;
            }
            button.onClick.AddListener(delegate { DisplaySubject(subject); } );
            button.transform.GetChild(0).GetComponent<TMP_Text>().text = subject.name;
            button.transform.GetChild(1).GetComponent<TMP_Text>().text = System.Math.Round(subject.getGrade(), 1, System.MidpointRounding.ToEven).ToString();
            buttons.Add(button);
        }
    }
    public void Clear()
    {
        foreach (Button button in buttons)
        {
            Destroy(button.gameObject);
            buttons = new List<Button>();
        }
    }
    public void DisplaySubject(Subject course)
    {
        Clear();
        List<TMP_Text> stuff = new List<TMP_Text>();

        foreach (string cat in course.gradeDict.Keys)
        {
            TMP_Text catUI = Instantiate(catToInstantiate, parent).GetComponent<TMP_Text>();
            if (stuff.Count == 0)
            {
                catUI.gameObject.transform.position = modelCat.transform.position;
            }
            else
            {
                catUI.gameObject.transform.position = stuff[stuff.Count - 1].gameObject.transform.position - new Vector3(0, catUI.GetComponent<RectTransform>().rect.height, 0);
            }
            catUI.gameObject.GetComponent<TMP_Text>().text = cat;
            catUI.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = course.getCatGrade(cat).ToString() + "%";
            Vector3 vec3C = catUI.gameObject.transform.position;
            catUI.gameObject.transform.position = new Vector3(modelCat.gameObject.transform.position.x, vec3C.y, vec3C.z);
            stuff.Add(catUI);
            foreach(Subject.Grade grade in course.gradeDict[cat])
            {
                TMP_Text gradeUI = Instantiate(gradeToInstantiate, parent).GetComponent<TMP_Text>();
                gradeUI.gameObject.transform.position = stuff[stuff.Count - 1].gameObject.transform.position - new Vector3(0, gradeUI.GetComponent<RectTransform>().rect.height, 0);
                Vector3 vec3 = gradeUI.gameObject.transform.position;
                gradeUI.gameObject.transform.position = new Vector3(modelGrade.gameObject.transform.position.x, vec3.y, vec3.z);
                gradeUI.gameObject.GetComponent<TMP_Text>().text = grade.assignmentTitle;
                gradeUI.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = course.getAssignmentGradeAsString(grade.assignmentTitle).ToString() + "%";
                stuff.Add(gradeUI);
            }
        }
    }
}
