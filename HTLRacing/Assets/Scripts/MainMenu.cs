using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public InputField inputFieldName;
    public Animator anim;

    public GameObject track1UI;
    public GameObject track2UI;
    public GameObject mainMenuUI;
    public GameObject carUI;

    public Transform car;
    public Transform track1CarSpawn;
    public Transform track2CarSpawn;

    public Camera mainMenuCamera;
    public Camera carCamera;

    public TrackManager track1Manager;
    public TrackManager track2Manager;

    float timer = 0;

    private void Start()
    {
        DisableAllUI();
        mainMenuUI.SetActive(true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && car.gameObject.activeSelf)
        {
            GoToMenu();
        }

        if(!mainMenuUI.activeSelf)
        {
            timer += Time.deltaTime;
        }
        if(Input.anyKey)
        {
            timer = 0;
        }
        if(timer > 60)
        {
            GoToMenu();
            timer = 0;
        }
        Debug.Log(timer);
    }

    enum animTrigger
    {
        toMenu, toTrack1, toTrack2
    }

    public void OnBtnStartClicked()
    {
        string playerName = inputFieldName.text;
        CarBestLap.PlayerHighscore.name = playerName;
    }

    public void GoToTrack1()
    {
        anim.SetTrigger(animTrigger.toTrack1.ToString());

        DisableAllUI();
        track1UI.SetActive(true);
    }

    public void GoToTrack2()
    {
        anim.SetTrigger(animTrigger.toTrack2.ToString());

        DisableAllUI();
        track2UI.SetActive(true);
    }

    public void GoToMenu()
    {
        anim.SetTrigger(animTrigger.toMenu.ToString());

        DisableAllUI();
        mainMenuUI.SetActive(true);
        mainMenuCamera.gameObject.SetActive(true);
        car.gameObject.SetActive(false);
        carCamera.gameObject.SetActive(false);
    }

    void DisableAllUI()
    {
        mainMenuUI.SetActive(false);
        track1UI.SetActive(false);
        track2UI.SetActive(false);
        carUI.SetActive(false);
    }

    public void StartTrack1()
    {
        DisableAllUI();
        carUI.SetActive(true);
        car.position = track1CarSpawn.position;
        car.rotation = track1CarSpawn.rotation;
        mainMenuCamera.gameObject.SetActive(false);
        car.gameObject.SetActive(true);
        carCamera.gameObject.SetActive(true);
        track1Manager.InitializeTrack();
    }

    public void StartTrack2()
    {
        DisableAllUI();
        carUI.SetActive(true);
        car.position = track2CarSpawn.position;
        car.rotation = track2CarSpawn.rotation;
        mainMenuCamera.gameObject.SetActive(false);
        car.gameObject.SetActive(true);
        carCamera.gameObject.SetActive(true);
        track2Manager.InitializeTrack();
    }
}
