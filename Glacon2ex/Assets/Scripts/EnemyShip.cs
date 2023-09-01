using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    public Planet _targetPlanet;
    
    [SerializeField] private float _speed = 5f;


    private void Start()
    {
        
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPlanet.transform.position, _speed * Time.deltaTime);
        transform.up = _targetPlanet.transform.position - transform.position;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Planet collided = other.GetComponent<Planet>();
        if (_targetPlanet.isEnemy() && collided == _targetPlanet)
        {

            _targetPlanet.IncreaseNumber();
        }
        else if (_targetPlanet.isFrendly() && collided == _targetPlanet)
        {

            _targetPlanet.DecreaseNumber();
            if (_targetPlanet._numOfShips <= 0)
            {
                _targetPlanet._numOfShips = 0;
                _targetPlanet.SetPlanetEnemyState();
                PlanetController.Instance._enemyPlanets.Add(_targetPlanet);
            }
        }
        else if (_targetPlanet.isNuetral())
        {

            _targetPlanet.DecreaseNumber();
            if (_targetPlanet._numOfShips <= 0)
            {
                _targetPlanet.SetPlanetEnemyState();
                PlanetController.Instance._enemyPlanets.Add(_targetPlanet);
            }
        }
        Destroy(gameObject);
    }


}
