using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public GameObject Main;
    public GameObject GameOver;
    public GameObject Difficulty;

    private void Awake()
    {
        // Ensure there is only one instance of MenuController
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        // Don't destroy the MenuManager when loading new scenes
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

    }

    public void LoadMain(){
        SceneManager.LoadSceneAsync(0);
    }

    public void LoadGameOver(){
        SceneManager.LoadSceneAsync(0);
        Main.SetActive(false);
        GameOver.SetActive(true);
    }
    
    public void PlayGame(){
        SceneManager.LoadSceneAsync(1);
        Main.SetActive(false);
        GameOver.SetActive(false);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
