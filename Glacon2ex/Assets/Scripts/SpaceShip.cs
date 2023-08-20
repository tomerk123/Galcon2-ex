using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    private Planet _planet;
    [SerializeField] private float _speed = 5f;
    [SerializeField]private  GameObject destinationPlanet;



    private void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "FriendlyPlanet")
        {
            Destroy(gameObject);
            _planet.IncreaseNumber();
        }
        else if (other.gameObject.tag == "EnemyPlanet" && _planet._numOfShips > 0)
        {
            Destroy(gameObject);
            _planet.DecreaseNumber();
        }
        else if (other.gameObject.tag == "NeutralPlanet" && _planet._numOfShips > 0)
        {
            Destroy(gameObject);
            _planet.DecreaseNumber();
        }
    }

}
