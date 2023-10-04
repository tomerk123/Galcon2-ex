using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlanetController : MonoBehaviour
{

    [SerializeField]
    private Planet _planetPrefab;

    [SerializeField]
    private int _numOfPlanets;

    [SerializeField]
    private LayerMask _layerMask;

   
    public static PlanetController Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        InstantiateNuetralPlanets();
        InstantiateEnemyPlanet();
        InstantiateFriendlyPlanet();
    }

    void Update()
    {
    }

    // CR: [discuss] radius and pi
    void InstantiateNuetralPlanets()
    {
        for (int i = 0; i < StartScreen.Instance.NumOfPlanets - 2; i++)
        {
            Vector3 spwanPos = new Vector3(Random.Range(-6.5f, 10f), Random.Range(-6.5f, 10f), 0);
            float randomSize = Random.Range(1f, 3f);
            float radius = randomSize / Mathf.PI;

            while (CheckOverLap(spwanPos, radius)) {
                spwanPos = new Vector3(Random.Range(-6.5f, 10f), Random.Range(-6.5f, 10f), 0);
                randomSize = Random.Range(1f, 3f);
                radius = randomSize / Mathf.PI;
            }

            Planet planetN = Instantiate(_planetPrefab, spwanPos, Quaternion.identity);
            planetN.transform.localScale = new Vector3(randomSize, randomSize, 1);
            planetN.SetPlanetState(PlanetState.Neutral);
        }
    }

    void InstantiateEnemyPlanet()
    {
        Vector3 spwanPos = new Vector3(Random.Range(-6.5f,9f), Random.Range(-5.5f, 9f), 0);
        float StartSize = 2f;
        float radius = StartSize * 2 / (2 * Mathf.PI);
        while (CheckOverLap(spwanPos, radius))
        {
            spwanPos = new Vector3(Random.Range(-6.5f, 9f), Random.Range(-5.5f, 9f), 0);
        }
        Planet planetE = Instantiate(_planetPrefab, spwanPos, Quaternion.identity);
        planetE.transform.localScale = new Vector3(StartSize, StartSize, StartSize);
        planetE.SetPlanetState(PlanetState.Enemy);
       
    }

    void InstantiateFriendlyPlanet()
    {
        Vector3 spwanPos = new Vector3(Random.Range(-6.5f, 9f), Random.Range(-6.5f, 9f), 0);

        float StartSize = 2f;
        float radius = StartSize * 2 / (2 * Mathf.PI);
        while (CheckOverLap(spwanPos, radius))
        {
            spwanPos = new Vector3(Random.Range(-6.5f,9f), Random.Range(-6.5f, 9f), 0);
        }
        Planet planetF = Instantiate(_planetPrefab, spwanPos, Quaternion.identity);
        planetF.transform.localScale = new Vector3(StartSize, StartSize, StartSize);
        planetF.SetPlanetState(PlanetState.Friendly);
        
    }


/*
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
*/
    // CR: unused, delete.
    private void GetRandomPosition()
    {
        Vector3 position = new Vector3(Random.Range(-8, 8), Random.Range(-9, 9), 0);
    }


    private bool CheckOverLap(Vector2 position, float radius)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius + 1f, _layerMask);
        return colliders.Length > 0;
    }


    // CR: delete
    Vector3 GenerateRandomPosition()
    {
        float randomX = Random.Range(-7, 7);
        float randomY = Random.Range(-7, 7);
        return new Vector3(randomX, randomY, 0);
    }




}


