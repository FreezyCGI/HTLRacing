using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HighscoreUI : MonoBehaviour
{
    public TMP_Text txtName;
    public TMP_Text txtTime;

    public void SetHighscore(Highscore highscore)
    {
        txtName.text = highscore.name;
        txtTime.text = highscore.time.ToString("0.00") + "s";
    }
}
