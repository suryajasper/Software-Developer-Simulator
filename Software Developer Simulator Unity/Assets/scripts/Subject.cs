using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Subject
{
    public string name;
    [Range(1, 10)] public int difficulty;
    [Range(0f, 1f)] public float chanceOfPopQuiz;
    [Range(0,7)] public int testFrequency;
    [Range(0, 7)] public int essayFrequency;

    public struct Grade {
        public string assignmentTitle;
        public float possiblePoints;
        public float earnedPoints;

        public Grade(string aT, float eP, float pP)
        {
            assignmentTitle = aT;
            possiblePoints = pP;
            earnedPoints = eP;
        }
    }

    public Dictionary<string, List<Grade>> gradeDict;

    public Subject(string name)
    {
        gradeDict = new Dictionary<string, List<Grade>>();
        this.name = name;
    }
    public void addGrade(string category, string title, string grade)
    {
        bool found = false;
        foreach (string cat in gradeDict.Keys)
        {
            if (cat == category)
            {
                found = true;
                break;
            }
        }
        if (found)
            gradeDict[category].Add(new Grade(title, float.Parse(grade.Split('/')[0]), float.Parse(grade.Split('/')[1])));
        else
        {
            string[] gradeSplit = grade.Split('/');
            gradeDict.Add(category, new List<Grade> { new Grade(title, float.Parse(gradeSplit[0]), float.Parse(gradeSplit[1])) });
        }
    }

    public string getCatGradeAsString(string category)
    {
        List<float> received = new List<float>();
        List<float> possible = new List<float>();

        if (gradeDict.ContainsKey(category))
        {
            foreach (Grade grade in gradeDict[category])
            {
                received.Add(grade.earnedPoints);
                possible.Add(grade.possiblePoints);
            }
            float possibleSum = 0f;
            float receivedSum = 0f;
            for (int i =0; i < received.Count; i++)
            {
                possibleSum += possible[i];
                receivedSum += received[i];
            }
            if (System.Math.Abs(possibleSum) < 0.5f)
                return null;
            return System.Math.Round(receivedSum, 1, System.MidpointRounding.ToEven).ToString() + "/" + System.Math.Round(possibleSum, 1, System.MidpointRounding.ToEven).ToString();
        } 
        return null;
    }
    public float getCatGrade(string category)
    {
        string str = getCatGradeAsString(category);
        if (str == null) return -1f;
        return float.Parse(str.Split('/')[0]) / float.Parse(str.Split('/')[1]) * 100f;
    }
    public string getAssignmentGradeAsString(string assignmentName)
    {
        foreach (List<Grade> grades in gradeDict.Values)
            foreach (Grade grade in grades)
                if (grade.assignmentTitle == assignmentName)
                    return System.Math.Round(grade.earnedPoints, 1, System.MidpointRounding.ToEven).ToString() + "/" + System.Math.Round(grade.possiblePoints, 1, System.MidpointRounding.ToEven).ToString();
        return null;
    }
    public float getAssignmentGrade(string assignmentName)
    {
        string str = getAssignmentGradeAsString(assignmentName);
        if (str == null) return -1f;
        return float.Parse(str.Split('/')[0]) / float.Parse(str.Split('/')[1]) * 100f;
    }
    public string getGradeAsString()
    {
        float possible = 0f;
        float received = 0f;
        foreach(string cat in gradeDict.Keys)
        {
            string[] catOut = getCatGradeAsString(cat).Split('/');
            possible += float.Parse(catOut[1]);
            received += float.Parse(catOut[0]);
        }
        if (System.Math.Abs(possible) < 0.5f)
            return null;
        return System.Math.Round(received, 1, System.MidpointRounding.ToEven).ToString() + "/" + System.Math.Round(possible, 1, System.MidpointRounding.ToEven).ToString();
    }
    public float getGrade()
    {
        string str = getGradeAsString();
        if (str == null) return -1f;
        return float.Parse(str.Split('/')[0]) / float.Parse(str.Split('/')[1]) * 100f;
    }
}
