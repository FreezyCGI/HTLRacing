using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    public TMP_Text txtÜberschrift;
    public TMP_Text txtText;

    public void Open(string überschrift, string text)
    {
        txtÜberschrift.text = überschrift;
        txtText.text = text;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
