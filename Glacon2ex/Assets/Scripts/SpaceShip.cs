using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] GameObject destinationPlanet;
    float arrivalDistance = 0.1f;


    private void Update()
    {
        if (destinationPlanet != null)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, destinationPlanet.transform.position, Time.deltaTime * _speed);
            if (Vector3.Distance(transform.position, destinationPlanet.transform.position) < arrivalDistance)
            {
                HandleArrival();
            }
        }
    }

    private void HandleArrival()
    {
        if (destinationPlanet.CompareTag("FriendlyPlanet"))
        {
            destinationPlanet.GetComponent<Planet>().IncreaseNumber();
        }
        else if (destinationPlanet.CompareTag("EnemyPlanet") || destinationPlanet.CompareTag("NeutralPlanet"))
        {
            destinationPlanet.GetComponent<Planet>().DecreaseNumber();
        }

        Destroy(gameObject);
    }



}
