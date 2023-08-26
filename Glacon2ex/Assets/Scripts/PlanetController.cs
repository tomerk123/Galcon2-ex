using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField]
    // CR: like we talked about in the lesson, prefabs don't have to be kept as 'GameObject' - you can do 
    //     private Planet _planetPrefab; 
    private GameObject _planetPrefab;

    [SerializeField]
    private int _numOfNuetralPlanets;

    // CR: delete these - you are not using them anywhere..
    //     like i always say - keep as little state as possible :) 
    private List<Planet> _nuetralPlanets = new List<Planet>();
    private List<Planet> _enemyPlanets = new List<Planet>();
    private List<Planet> _freindlylPlanets = new List<Planet>();



    void Start()
    {
        InstantiateNuetralPlanets();
        InstantiateEnemyPlanet();
        InstantiateFriendlyPlanet();
    }


    void Update()
    {

    }   


    void InstantiateNuetralPlanets()
    {
        for (int i = 0; i < _numOfNuetralPlanets - 2; i++)
        {
            // CR: you don't need 2 separate lines:
            //     var planet = Instantiate<Planet>(...);
            GameObject planet = Instantiate(_planetPrefab, new Vector3(Random.Range(-8, 8), Random.Range(-9, 9), 0), Quaternion.identity);
            Planet planetN = planet.GetComponent<Planet>();
            // CR: swap these two lines
            planetN.SetPlanetVisuals();
            planetN._planetState = Planet.PlanetState.Neutral;
        }
    }

    public void InstantiateEnemyPlanet()
    {
        GameObject planet = Instantiate(_planetPrefab, new Vector3(Random.Range(-8,8),Random.Range(-9,9)), Quaternion.identity);
        Planet planetE = planet.GetComponent<Planet>();
        planetE._planetState = Planet.PlanetState.Enemy;
        planetE.SetPlanetVisuals();
        _enemyPlanets.Add(planetE);
    }

    public void InstantiateFriendlyPlanet()
    {
        GameObject planet = Instantiate(_planetPrefab,new Vector3(Random.Range(-8,8),Random.Range(-9,9)), Quaternion.identity);
        Planet planetF = planet.GetComponent<Planet>();
        planetF._planetState = Planet.PlanetState.Friendly;
        planetF.SetPlanetVisuals();
        _freindlylPlanets.Add(planetF);
    }

    public void AddPlanet(Planet planet)
    {
        if (planet._planetState == Planet.PlanetState.Neutral)
        {
            _nuetralPlanets.Add(planet);
        }
        else if (planet._planetState == Planet.PlanetState.Enemy)
        {
            _enemyPlanets.Add(planet);
        }
        else if (planet._planetState == Planet.PlanetState.Friendly)
        {
            _freindlylPlanets.Add(planet);
        }
    }

    public void RemovePlanet(Planet planet)
    {
        if (planet._planetState == Planet.PlanetState.Neutral)
        {
            _nuetralPlanets.Remove(planet);
        }
        else if (planet._planetState == Planet.PlanetState.Enemy)
        {
            _enemyPlanets.Remove(planet);
        }
        else if (planet._planetState == Planet.PlanetState.Friendly)
        {
            _freindlylPlanets.Remove(planet);
        }
    }


    private void GetRandomPosition()
    {
        Vector3 position = new Vector3(Random.Range(-8, 8), Random.Range(-9, 9), 0);
    }
}


