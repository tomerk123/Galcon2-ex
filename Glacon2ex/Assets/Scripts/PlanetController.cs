using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlanetController : MonoBehaviour
{
   
    [SerializeField]
    private Planet _planetPrefab;

    [SerializeField]
    private int _numOfPlanets;

    public List<Planet> allPlanets = new List<Planet>();
    public static PlanetController Instance;

    private PlanetState _planetState;
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
    
        for (int i = 0; i < _numOfPlanets - 2; i++)
        {
            Planet planetN = Instantiate(_planetPrefab, new Vector3(Random.Range(-7, 7), Random.Range(-7, 7), 0), Quaternion.identity);
            float randomSize = Random.Range(1f, 3f);
            planetN.transform.localScale = new Vector3(randomSize, randomSize, 1);
            planetN._planetState = PlanetState.Neutral;
            planetN.SetPlanetSettings(planetN._planetState);
            allPlanets.Add(planetN);
        }
    }

        void InstantiateEnemyPlanet()
        {
            Planet planetE = Instantiate(_planetPrefab, new Vector3(Random.Range(-7, 7), Random.Range(-7, 7)), Quaternion.identity);
            float StartSize = 2f;
            planetE.transform.localScale = new Vector3(StartSize, StartSize, StartSize);
            planetE._planetState = PlanetState.Enemy;
            planetE.SetPlanetSettings(planetE._planetState);
            allPlanets.Add(planetE);
        }

        void InstantiateFriendlyPlanet()
        {
            Planet planetF = Instantiate(_planetPrefab, new Vector3(Random.Range(-7, 7), Random.Range(-7, 7)), Quaternion.identity);
            float StartSize = 2f;
            planetF.transform.localScale = new Vector3(StartSize, StartSize, StartSize);
            planetF._planetState = PlanetState.Friendly;
            planetF.SetPlanetSettings(planetF._planetState);
            allPlanets.Add(planetF);
        }
    


    public List<Planet> friendlyPlanets
    {
        get
        {
            var friendlyPlanets = new List<Planet>();
            foreach (var planet in allPlanets)
            {
                if (planet.isFrendly)
                {
                    friendlyPlanets.Add(planet);
                }
            }
            return friendlyPlanets;
        }
    }

    public List<Planet> enemyPlanets
    {
        get
        {
            var enemyPlanets = new List<Planet>();
            foreach (var planet in allPlanets)
            {
                if (planet.isEnemy)
                {
                    enemyPlanets.Add(planet);
                }
            }
            return enemyPlanets;
        }
    }

    public List<Planet> neutralPlanets
    {
        get
        {
            var neutralPlanets = new List<Planet>();
            foreach (var planet in allPlanets)
            {
                if (planet.isNuetral)
                {
                    neutralPlanets.Add(planet);
                }
            }
            return neutralPlanets;
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


