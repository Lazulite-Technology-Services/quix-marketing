using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Screens;

    [SerializeField] private GameObject NavigationButtonGroup;

    [SerializeField] private Button[] buttons;

    private int currentIndex = 0;

    [SerializeField] private Button back, home;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    private void Init()
    {
        back.onClick.AddListener(Back);
        home.onClick.AddListener(Home);

        foreach (var item in buttons)
        {
            item.onClick.AddListener(() => VideoHandler.instance.PlayVideo(item.GetComponent<ButtonData>().path));

            if (item.name != "home")
            {
                Debug.Log($"{item.name}");  
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

        if(currentIndex > 0)
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
    }

    private void Home()
    {
        //Disable current screen
        Screens[currentIndex].SetActive(false);

        currentIndex = 0;

        //Enable Next screen
        Screens[currentIndex].SetActive(true);

        NavigationButtonGroup.SetActive(false);
    }
}
