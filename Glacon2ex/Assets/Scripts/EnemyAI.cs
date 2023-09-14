using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyAI : MonoBehaviour
{
    // CR: no defaults in the code2
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
            _targetPlanet = GetRandomPlanet();
            
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

    Planet GetRandomPlanet()
    {
        List<Planet> planets = new List<Planet>();
        planets.AddRange(GameManager.Instance.GetPlanetList(PlanetState.Friendly));
        planets.AddRange(GameManager.Instance.GetPlanetList(PlanetState.Neutral));
        if (planets.Count > 0)
        {
            int randomPlanet = Random.Range(0,planets.Count);
            return planets[randomPlanet];
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
