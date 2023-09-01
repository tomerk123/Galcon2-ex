using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float spawnInterval = 5;

    [SerializeField]
    private float timeSinceLastSpawn = 0f;
    public GameObject _EnemyShip;
    [SerializeField]
    private Transform _spawnPosition;

    private Planet _thisPlanet;
    private Planet _targetPlanet;
    

    void Start()
    {
        _thisPlanet = GetComponent<Planet>();
        PlanetController.Instance._enemyPlanets.Add(_thisPlanet);
    }

    void Attack()
    {
        if (this.gameObject.tag == "EnemyPlanet")
        {
            if (_thisPlanet._numOfShips > 0)
            {
                _targetPlanet = GetRandomNuetralPlanet();
                if (_targetPlanet != null)
                {
                    DeployEnemyShips(_targetPlanet);
                }
            }
        }
    }
    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnInterval)
        {
            Attack();
            timeSinceLastSpawn = 0;
        }
    }


    Planet GetRandomFriendlyPlanet()
    {
        if (PlanetController.Instance._freindlylPlanets.Count > 0)
        {
            int randomPlanet = Random.Range(0, PlanetController.Instance._freindlylPlanets.Count);
            return PlanetController.Instance._freindlylPlanets[randomPlanet];
        }

        else
        {
            return null;
        }
    }

    Planet GetRandomNuetralPlanet()
    {
        if (PlanetController.Instance._nuetralPlanets.Count > 0)
        {
            int randomPlanet = Random.Range(0, PlanetController.Instance._nuetralPlanets.Count);
            return PlanetController.Instance._nuetralPlanets[randomPlanet];
        }
        else
        {
            return null;
        }
    }

    Planet GetRandomEnemyPlanet()
    {
        if (PlanetController.Instance._enemyPlanets.Count > 0)
        {
            int randomPlanet = Random.Range(0, PlanetController.Instance._enemyPlanets.Count);
            return PlanetController.Instance._enemyPlanets[randomPlanet];
        }
        else
        {
            return null;
        }
    }

    public void DeployEnemyShips(Planet targetPlanet)
    {

        _thisPlanet._numOfShips = _thisPlanet._numOfShips / 2;
        _thisPlanet._numOfshipText.text = _thisPlanet._numOfShips.ToString();
        for (int i = 0; i < _thisPlanet._numOfShips; i++)
        {
            GameObject Ship = Instantiate(_EnemyShip, _spawnPosition.position, Quaternion.identity);
            Ship.GetComponent<EnemyShip>()._targetPlanet = targetPlanet;
        }
    }

}
