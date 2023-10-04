using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System;


public class SpaceShip : MonoBehaviour
{

    private Planet _targetPlanet;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject _explosionVFX;

    private PlanetState _state;
    [SerializeField] private float _speed = 8f; // CR: no defaults in the code - put the '8' in the SpaceShip prefab.
    
    private void Update()
    {
        Movement();
    }

    public void Init(PlanetState state, Planet targetPlanet) {
         _spriteRenderer = GetComponent<SpriteRenderer>();

        _state = state;
        _targetPlanet = targetPlanet;

        switch (state) {
            case PlanetState.Friendly:
                _spriteRenderer.color = Color.blue;
                break;
            case PlanetState.Enemy:
                _spriteRenderer.color = Color.red;
                break;
            case PlanetState.Neutral:
                _spriteRenderer.color = Color.gray;
                break;
        }
    }
    
    // CR: you can simplify with something like
    // if _state == cllided.state {
    //    collided.IncreaseShips();
    // } 
    // else {
    //     collided.DecreaseShips();
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Planet collided = other.GetComponent<Planet>();
        if (collided == _targetPlanet)
        {
            if (collided.isFrendly && _state == PlanetState.Friendly)
            {
                collided.IncreaseNumber();
                Destroy(this.gameObject);
            }

            else if (collided.isEnemy && _state == PlanetState.Enemy)
            {
                collided.IncreaseNumber();
                Destroy(this.gameObject);
            }

            if (collided.isNuetral)
            {
                GameObject explosion = Instantiate(_explosionVFX, transform.position, Quaternion.identity);

                collided.DecreaseNumber();
                if (collided.numOfShips <= 0)
                {
                    collided.SetPlanetState(_state);
                    collided.IncreaseNumber();
                }
                Destroy(explosion, 1f);
                Destroy(this.gameObject);
            }

            if (collided.isEnemy && _state == PlanetState.Friendly)
            {
                GameObject explosion = Instantiate(_explosionVFX, transform.position, Quaternion.identity);
                collided.DecreaseNumber();

                if (collided.numOfShips <= 0)
                {
                    
                    collided.SetPlanetState(PlanetState.Friendly);
                    collided.IncreaseNumber();
                    GameManager.instance.CheckWinCondition();
                    
                }
                Destroy(explosion, 1f);
                Destroy(this.gameObject);
            }

            if (collided.isFrendly && _state == PlanetState.Enemy)
            {
                GameObject explosion = Instantiate(_explosionVFX, transform.position, Quaternion.identity);
                collided.DecreaseNumber();
                if (collided.numOfShips <= 0)
                {
                    
                    collided.SetPlanetState(PlanetState.Enemy);
                    collided.IncreaseNumber();
                    GameManager.instance.CheckWinCondition();
                    GameManager.instance.Unselect(collided);
                }
                Destroy(explosion, 1f);
                Destroy(this.gameObject);
            }
        }
    }


    void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPlanet.transform.position, _speed * Time.deltaTime);
        transform.up = _targetPlanet.transform.position - transform.position;
    }


}
