using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class customizeTraits : MonoBehaviour
{
    public int max;
    public TMP_Text howManyLeft;
    public Button startButton;
    private List<Slider> skillSliders;
    private List<TMP_Text> counts;
    // Start is called before the first frame update
    void Start()
    {
        startButton.interactable = false;
        howManyLeft.text = max.ToString() + " Left";
        skillSliders = new List<Slider>();
        counts = new List<TMP_Text>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Slider tempSlider = transform.GetChild(i).GetComponent<Slider>();
            TMP_Text tempText = tempSlider.transform.GetChild(0).GetComponent<TMP_Text>();
            tempSlider.onValueChanged.AddListener(delegate { changeTextValue(tempText, tempSlider.value); });
            counts.Add(tempText);
            skillSliders.Add(tempSlider);
        }
    }
    void changeTextValue(TMP_Text text, float value)
    {
        text.text = ((int)value).ToString();
        int sum = 0;
        foreach (TMP_Text indText in counts)
            sum += Int32.Parse(indText.text);
        if (sum > max)
        {
            int currIndex = counts.IndexOf(text);
            for (int i = 0; i < counts.Count; i++)
            {
                if (i != currIndex && Int32.Parse(counts[i].text) > 0)
                {
                    counts[i].text = (Int32.Parse(counts[i].text) - 1).ToString();
                    skillSliders[i].value -= 1;
                    break;
                }
            }
        }
        startButton.interactable = sum >= 20;
        howManyLeft.text = howManyLeft.text = (max- ((sum > max) ? max : sum) ).ToString() + " Left"; ;
    }
}
