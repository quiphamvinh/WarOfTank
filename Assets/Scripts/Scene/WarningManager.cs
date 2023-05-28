using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningManager : MonoBehaviour
{
    public GameObject warningPanel;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        warningPanel.SetActive(true);
        audioManager.PlaySFX(audioManager.warning);
        Invoke("HideWarningPanel", 1.5f);
    }

    void HideWarningPanel()
    {
        warningPanel.SetActive(false);
    }
}
