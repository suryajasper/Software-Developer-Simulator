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
}
