
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
     public void Menu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
