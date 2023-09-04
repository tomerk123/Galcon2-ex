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
    [SerializeField]
    private SpaceShip _spaceShipPrefab;
    [SerializeField]
    private Transform _spawnPosition;

    private Planet _thisPlanet;
    private Planet _targetPlanet;



    void Start()
    {
        _thisPlanet = GetComponent<Planet>();
    }

    void Attack()
    {
        if (_thisPlanet.isEnemy && _thisPlanet._numOfShips > 0)
        {
            _targetPlanet = GetRandomNuetralPlanet();
            if (_targetPlanet != null)
            {
                DeployEnemyShips(_targetPlanet);
            }
        }
     
    }
    private void Update()
    {
        if (!_thisPlanet.isEnemy)
        {
            return;
        }

        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnInterval)
        {
            Attack();
            timeSinceLastSpawn = 0;
        }
    }


    Planet GetRandomFriendlyPlanet()
    {
        if (PlanetController.Instance.friendlyPlanets.Count > 0)
        {
            int randomPlanet = Random.Range(0, PlanetController.Instance.friendlyPlanets.Count);
            return PlanetController.Instance.friendlyPlanets[randomPlanet];
        }

        else
        {
            return null;
        }
    }

    Planet GetRandomNuetralPlanet()
    {
        if (PlanetController.Instance.neutralPlanets.Count > 0)
        {
            int randomPlanet = Random.Range(0, PlanetController.Instance.neutralPlanets.Count);
            return PlanetController.Instance.neutralPlanets[randomPlanet];
        }
        else
        {
            return null;
        }
    }

    Planet GetRandomEnemyPlanet()
    {
        if (PlanetController.Instance.enemyPlanets.Count > 0)
        {
            int randomPlanet = Random.Range(0, PlanetController.Instance.enemyPlanets.Count);
            return PlanetController.Instance.enemyPlanets[randomPlanet];
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
            SpaceShip Ship = Instantiate(_spaceShipPrefab, _spawnPosition.position, Quaternion.identity);
            Ship.GetComponent<SpaceShip>()._targetPlanet = targetPlanet;
            Ship._spriteRenderer.color = Color.red;
        }
    }

}
