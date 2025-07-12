using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class CustomButton
{
    public Button button;
    public string name;
}
public class MenuController : MonoBehaviour
{
    [SerializeField] private string perfixSceneText;
    [SerializeField] private CustomButton[] customButtons;
    [SerializeField] private Button exitButton;
    void Start()
    {
        foreach (var customButton in customButtons) 
        {
            if (customButton?.button != null)
            {
                customButton.button.onClick.AddListener(() => LoadScene($"{perfixSceneText}{customButton.name}"));
                customButton.button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = customButton.name;
            }
        }
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(Exit);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    private void Exit()
    {
        Application.Quit();
    }
}
