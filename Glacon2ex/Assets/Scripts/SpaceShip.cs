using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    public Planet _targetPlanet;
    [SerializeField] private float _speed = 8f;





    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPlanet.transform.position, _speed * Time.deltaTime);
        transform.up = _targetPlanet.transform.position - transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Planet collided = other.GetComponent<Planet>();
        if (_targetPlanet.isFrendly() && collided == _targetPlanet)
        {
            Destroy(gameObject);
            _targetPlanet.IncreaseNumber();
        }
        else if (_targetPlanet.isEnemy() && collided == _targetPlanet)
        {
            Destroy(gameObject);
            _targetPlanet.DecreaseNumber();
            if (_targetPlanet._numOfShips <= 0)
            {
                _targetPlanet._numOfShips = 0;
                _targetPlanet.SetPlanetState();
            }
        }
        else if (_targetPlanet.isNuetral() && collided == _targetPlanet)
        {
            Destroy(gameObject);
            _targetPlanet.DecreaseNumber();
            if (_targetPlanet._numOfShips <= 0)
            {
                _targetPlanet.SetPlanetState();
            }
        }
        
    }


}
