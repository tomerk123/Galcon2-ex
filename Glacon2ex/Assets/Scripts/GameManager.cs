using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Win/Lose Screens")]
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private GameObject _winScreen;

    private List<Planet> _selectedPlanets;
    public List<Planet> selectedPlanets => _selectedPlanets;

    
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        _selectedPlanets = new List<Planet>();
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
            if (planet.planetState == state)
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


    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);   
    }

    void QuitGame()
    {
        Application.Quit();
    }

    public void ClearSelection() {
      _selectedPlanets.Clear();
    }

    public void Select(Planet selectedPlanet) {
      foreach (Planet planet in _selectedPlanets) {
        if (planet.gameObject.GetInstanceID() == selectedPlanet.gameObject.GetInstanceID()) {
          return;
        }
      }

      selectedPlanets.Add(selectedPlanet);
    }

}


