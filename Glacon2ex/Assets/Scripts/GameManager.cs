using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Win/Lose Screens")]
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private GameObject _winScreen;



    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {

    }
    public void Win()
    {
        _winScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }


    public void Lose()
    {
        _loseScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }


    public void Restart()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public List<Planet> GetPlanetList(PlanetState state)
    {
        var Planets = new List<Planet>();
        foreach (Planet planet in FindObjectsOfType<Planet>())
        {
            if (planet._planetState == state)
            {
                Planets.Add(planet);
            }
        }
        return Planets;
    }

    public void CheckWinCondition()
    {
        Debug.Log(GetPlanetList(PlanetState.Enemy).Count);
        
        if (GetPlanetList(PlanetState.Enemy).Count == 0)
        {
            Win();
            Debug.Log("win");
        }
        else if (GetPlanetList(PlanetState.Friendly).Count == 0)
        {
            Lose();
            Debug.Log("lose");
        }
    }
}


