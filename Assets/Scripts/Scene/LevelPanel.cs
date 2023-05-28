using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LevelPanel : MonoBehaviour
{
    public GameObject Panel;
    void Start()
    {
        Panel.SetActive(true);
        Invoke("HideWarningPanel", 1.5f);
    }
    void HideWarningPanel()
    {
        Panel.SetActive(false);
    }
}
