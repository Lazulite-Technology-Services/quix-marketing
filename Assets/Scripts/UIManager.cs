using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Screens;

    [SerializeField] private GameObject NavigationButtonGroup;

    [SerializeField] private Button[] buttons;

    private int currentIndex = 0;

    [SerializeField] private Button back, home, pause, play;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    private void Init()
    {
        back.onClick.AddListener(Back);
        home.onClick.AddListener(Home);
        pause.onClick.AddListener(Pause);
        play.onClick.AddListener(Play);

        foreach (var item in buttons)
        {
            item.onClick.AddListener(() => VideoHandler.instance.PlayVideo(item.GetComponent<ButtonData>().path));

            if (item.name != "home")
            {
                //Debug.Log($"{item.name}");  
                item.onClick.AddListener(() => OnButtonClicked(item));
            }
        }
    }

    public void LoadNextScreen()
    {
        //Disable Last screen
        Screens[currentIndex].SetActive(false);

        currentIndex++;        

        //Enable next screen
        Screens[currentIndex].SetActive(true);

        if (currentIndex == 2)
            //back.gameObject.SetActive(true);
            back.interactable = true;
        else
            //back.gameObject.SetActive(false);
            back.interactable = false;

        if (currentIndex > 0)
        {
            NavigationButtonGroup.SetActive(true);            
        }
    }

    void OnButtonClicked(Button clickedbutton)
    {
        foreach (var item in buttons)
        {
            if (item == clickedbutton)
            {
                item.GetComponent<Image>().sprite = item.GetComponent<ButtonData>().selectedImage; // select clicked
            }
            else
            {
                if (item.name != "home")
                    item.GetComponent<Image>().sprite = item.GetComponent<ButtonData>().deselectedImage;   // deselect others
            }
        }
    }

    private void ResetButtonStateImages()
    {
        foreach (var item in buttons)
        {
            if (item.name != "home")
                item.GetComponent<Image>().sprite = item.GetComponent<ButtonData>().deselectedImage;
        }
    }

    private void Back()
    {
        //Disable current screen
        Screens[currentIndex].SetActive(false);

        currentIndex--;

        //Enable Next screen
        Screens[currentIndex].SetActive(true);

        if (currentIndex == 0)
        {
            NavigationButtonGroup.SetActive(false);
        }

        if (currentIndex == 2)
            //back.gameObject.SetActive(true);
            back.interactable = true;
        else
            //back.gameObject.SetActive(false);
            back.interactable = false;
    }

    private void Home()
    {
        //Disable current screen
        Screens[currentIndex].SetActive(false);

        currentIndex = 0;

        //Enable Next screen
        Screens[currentIndex].SetActive(true);

        NavigationButtonGroup.SetActive(false);

        ResetButtonStateImages();
    }

    private void Pause()
    {
        pause.interactable = false;
        play.interactable = true;

        VideoHandler.instance.PauseOrPlayTheVideo();
    }

    private void Play()
    {
        play.interactable = false;
        pause.interactable = true;

        VideoHandler.instance.PauseOrPlayTheVideo();
    }
}
