using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    public TMP_Text txt�berschrift;
    public TMP_Text txtText;

    public void Open(string �berschrift, string text)
    {
        txt�berschrift.text = �berschrift;
        txtText.text = text;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
