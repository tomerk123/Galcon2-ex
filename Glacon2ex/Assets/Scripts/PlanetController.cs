using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{

    [SerializeField]
    // CR: like we talked about in the lesson, prefabs don't have to be kept as 'GameObject' - you can do 
    //     private Planet _planetPrefab; 
    private Planet _planetPrefab;

    [SerializeField]
    private int _numOfNuetralPlanets;

    public List<Planet> _nuetralPlanets = new List<Planet>();
    public List<Planet> _enemyPlanets = new List<Planet>();
    public List<Planet> _freindlylPlanets = new List<Planet>();
    public List<Vector3> planetPositions = new List<Vector3>();
    public static PlanetController Instance;
    public PlanetState _planetState;
    
    
    



     private void Awake()
    {
        Instance = this;
    }




    void Start()
    {

        // Vector3 randomPos = GenerateRandomPosition();
        //bool isOverlapping = CheckForOverlap(randomPos);
        // planetPositions.Add(randomPos);

        // while (!isOverlapping)
        //{
        InstantiateNuetralPlanets();
        InstantiateEnemyPlanet();
        InstantiateFriendlyPlanet();
        // }


    }

    void Update()
    {

    }   

    void InstantiateNuetralPlanets()
    {
        for (int i = 0; i < _numOfNuetralPlanets - 2; i++)
        {

            Planet planet = Instantiate(_planetPrefab, new Vector3(Random.Range(-7, 7), Random.Range(-7, 7), 0), Quaternion.identity);
            float randomSize = Random.Range(1f, 3f);
            planet.transform.localScale = new Vector3(randomSize, randomSize, 1);
            Planet planetN = planet.GetComponent<Planet>();
            _planetState = PlanetState.Neutral;
            AddPlanet(planetN);
            planetN.SetPlanetVisuals();
            Debug.Log(_nuetralPlanets.Count);
        }
    }

    public void InstantiateEnemyPlanet()
    {
       Planet planet = Instantiate(_planetPrefab, new Vector3(Random.Range(-7, 7), Random.Range(-7, 7)), Quaternion.identity);
        float StartSize = 2f;
        planet.transform.localScale = new Vector3(StartSize, StartSize, StartSize);
        Planet planetE = planet.GetComponent<Planet>();
        _planetState = PlanetState.Enemy;
        planetE.SetPlanetVisuals();
        _enemyPlanets.Add(planetE);
    }

    public void InstantiateFriendlyPlanet()
    {
        Planet planet = Instantiate(_planetPrefab, new Vector3(Random.Range(-7, 7), Random.Range(-7, 7)), Quaternion.identity);
        float StartSize = 2f;
        planet.transform.localScale = new Vector3(StartSize, StartSize, StartSize);
        Planet planetF = planet.GetComponent<Planet>();
        _planetState = PlanetState.Friendly;
        planetF.SetPlanetVisuals();
        _freindlylPlanets.Add(planetF);
    }

    public void AddPlanet(Planet planet)
    {
        if (_planetState == PlanetState.Neutral)
        {
            _nuetralPlanets.Add(planet);
        }
        else if (_planetState == PlanetState.Enemy)
        {
            _enemyPlanets.Add(planet);
        }
        else if (_planetState == PlanetState.Friendly)
        {
            _freindlylPlanets.Add(planet);
        }
    }
    public void RemovePlanet(Planet planet)
    {
        if (_planetState == PlanetState.Neutral)
        {
            _nuetralPlanets.Remove(planet);
        }
        else if (_planetState == PlanetState.Enemy)
        {
            _enemyPlanets.Remove(planet);
        }
        else if (_planetState == PlanetState.Friendly)
        {
            _freindlylPlanets.Remove(planet);
        }
    }

    private void GetRandomPosition()
    {
        Vector3 position = new Vector3(Random.Range(-8, 8), Random.Range(-9, 9), 0);
    }


    // private bool CheckOverLap(Vector3 position, float radius)
    // {
    //     Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 1f, _overlapLayerMask);
    //     return colliders.Length > 0;

    //     if(!CheckOverLap(position,radius))
    //     {
    //         GameObject NewPlanet = Instantiate(_planetPrefab, position, Quaternion.identity);
    //         Planet planet = NewPlanet.GetComponent<Planet>();
    //         Instantiate(_planetPrefab, position, Quaternion.identity);
    //     }
    // }

    // bool CheckForOverlap(Vector3 position)
    // {

    //     foreach (Vector3 existingPosition in planetPositions)
    //     {
    //         if (Vector3.Distance(position, existingPosition) < minDistance)
    //         {
    //             return true;
    //         }
    //     }
    //     return false;
    // }

    Vector3 GenerateRandomPosition()
    {
        float randomX = Random.Range(-7, 7);
        float randomY = Random.Range(-7, 7);
        return new Vector3(randomX, randomY, 0);
    }


 

}


