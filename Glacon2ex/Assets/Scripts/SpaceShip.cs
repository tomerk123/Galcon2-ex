using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CR:
// Don't keep default-values in the code - keep them in prefabs.
// When you have default values in the code, the value of a specific object depends on 3 
// things:
//   * value in code.
//   * value in prefab.
//   * value in scene.
// By never having default-values in the code, you remove the complexity of this to 2 things:
//   * value in prefab
//   * value in scene.
// Note also the Unity will highlight in blue when the value in the scene is different 
// from the value in the prefab - but there's no indication if the value in the prefab
// is the same as the value in the code or not...


// CR: Don't split the code into 'EnemyShip' and 'SpaceShip' - just us a single SpaceShip class.
//     To differentiate between those cases, add a PlanetColor so you can tell if it is friendly/enemy/neutral.
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
        if (_targetPlanet.isFrendly && collided == _targetPlanet)
        {
            Destroy(gameObject);
            _targetPlanet.IncreaseNumber();
        }
        else if (_targetPlanet.isEnemy && collided == _targetPlanet)
        {
            Destroy(gameObject);
            _targetPlanet.DecreaseNumber();
            if (_targetPlanet._numOfShips <= 0)
            {
                _targetPlanet._numOfShips = 0;
                _targetPlanet.SetPlanetFriendlyState();
               PlanetController.Instance._freindlylPlanets.Add(_targetPlanet);
            }
        }
        else if (_targetPlanet.isNuetral && collided == _targetPlanet)
        {

            Destroy(gameObject);
            _targetPlanet.DecreaseNumber();
            if (_targetPlanet._numOfShips <= 0)
            {
                _targetPlanet.SetPlanetFriendlyState();
                PlanetController.Instance._freindlylPlanets.Add(_targetPlanet);
            }
        }

    }


}
