using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrapDescription : MonoBehaviour
{
    [SerializeField] TMP_Text firstLine;
    [SerializeField] TMP_Text secondLine;
    [SerializeField] TMP_Text description;

    public void SetTrapDescription(string firstLine, string secondLine, string description)
    {
        this.firstLine.text = firstLine;
        this.secondLine.text = secondLine;
        this.description.text = description;
    }
}
