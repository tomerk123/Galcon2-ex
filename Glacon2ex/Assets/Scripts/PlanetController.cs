using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    private PlanetState _planetState;
    public SpriteRenderer _planetRebderer;
    public GameObject _planetPrefab;

    [SerializeField] private int _numOfPlanets;
    private float minDistance = 20f;


    private List<Planet> _nuetralPlanets = new List<Planet>();
    private List<Planet> _enemyPlanets = new List<Planet>();
    private List<Planet> _freindlylPlanets = new List<Planet>();

    public enum PlanetState
    {
        Enemy,
        Friendly,
        Neutral
    }
    void Start()
    {

        InstantiatePlanets();
    }


    void Update()
    {

    }


    // private void InstantiatePlanets()
    //{


    //  for (int i = 0; i < _numOfPlanets; i++)
    // {

    // float randomX = Random.Range(-10, 10);
    // float randomY = Random.Range(8, -8);
    // GameObject newPlanet = Instantiate(_planetPrefab, new Vector3(randomX, randomY, 0), Quaternion.identity);
    //            Planet planet = newPlanet.GetComponent<Planet>();
    // }
    // }

    void InstantiatePlanets()
    {
        InstantiatePlanet(PlanetState.Enemy);
        InstantiatePlanet(PlanetState.Friendly);

        for (int i = 0; i < _numOfPlanets; i++)
        {
            InstantiatePlanet(PlanetState.Neutral);
        }
    }

    void InstantiatePlanet(PlanetState planetState)
    {
        float randomX = Random.Range(-9, 9);
        float randomY = Random.Range(8, -8);
        GameObject newPlanet = Instantiate(_planetPrefab, new Vector3(randomX, randomY, 0), Quaternion.identity);
        Planet planet = newPlanet.GetComponent<Planet>();
        planet.SetPlanetState();
        if (planetState == PlanetState.Enemy)
        {
            _enemyPlanets.Add(planet);
        }
        else if (planetState == PlanetState.Friendly)
        {
            _freindlylPlanets.Add(planet);
        }
        else
        {
            _nuetralPlanets.Add(planet);
        }
    }
}


