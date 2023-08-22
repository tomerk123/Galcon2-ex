using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    public GameObject _targetPlanet;
    [SerializeField] private float _speed = 5f;
    
    



    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPlanet.transform.position, _speed * Time.deltaTime);
        transform.up = _targetPlanet.transform.position - transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Planet targetPlanet = _targetPlanet.GetComponent<Planet>();
        if (other.gameObject.tag == "FriendlyPlanet")
        {
            Destroy(gameObject);
            targetPlanet.IncreaseNumber();
        }
        else if (other.gameObject.tag == "EnemyPlanet")
        {
            Destroy(gameObject);
            targetPlanet.DecreaseNumber();
            if(targetPlanet._numOfShips <= 0)
            {
                targetPlanet._numOfShips =0;
                targetPlanet.SetPlanetState();
            }
        }
        else if (other.gameObject.tag == "NeutralPlanet")
        {
            Destroy(gameObject);
            targetPlanet.DecreaseNumber();
            if (targetPlanet._numOfShips <= 0)
            {
                targetPlanet.SetPlanetState();
            }
        }
    }


}
