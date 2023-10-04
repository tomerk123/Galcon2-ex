using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// CR: [discuss] missing button
public class MenuScript : MonoBehaviour
{
   [SerializeField ] private GameObject _startScreen;
   [SerializeField] private GameObject _toGameScreen;



    public void StartGame()
    {
        _startScreen.gameObject.SetActive(true);
       
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    


    public void ToGameSettings()
    {
        _toGameScreen.gameObject.SetActive(true);
        _startScreen.gameObject.SetActive(false);
    }

    public void Play(int numOfPlanets)
    {
        StartScreen.Instance.SetNumOfPlanets(numOfPlanets);
       SceneManager.LoadScene("Level1");
    }
}
