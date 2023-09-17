using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;




public class SpaceShip : MonoBehaviour
{

    public Planet _targetPlanet;
    public SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject _explosionVFX;

    [SerializeField] private float _speed = 8f;

    private void Update()
    {
        Movement();
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = this._spriteRenderer.color;
    }
    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Planet collided = other.GetComponent<Planet>();
        if (collided == _targetPlanet)
        {
            if (collided.isFrendly && this._spriteRenderer.color == Color.blue)
            {
                collided.IncreaseNumber();
                Destroy(this.gameObject);
            }

            else if (collided.isEnemy && this._spriteRenderer.color == Color.red)
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
                    collided.SetPlanetSettings(collided.GetPlanetStateByShip(this));
                    collided.IncreaseNumber();
                }
                Destroy(explosion, 1f);
                Destroy(this.gameObject);
            }

            if (collided.isEnemy && _spriteRenderer.color == Color.blue)
            {
                GameObject explosion = Instantiate(_explosionVFX, transform.position, Quaternion.identity);
                collided.DecreaseNumber();

                if (collided.numOfShips <= 0)
                {
                    
                    collided.SetPlanetFreindlyState();
                    collided.IncreaseNumber();
                    GameManager.instance.CheckWinCondition();
                    
                }
                Destroy(explosion, 1f);
                Destroy(this.gameObject);
            }

            if (collided.isFrendly && this._spriteRenderer.color == Color.red)
            {
                GameObject explosion = Instantiate(_explosionVFX, transform.position, Quaternion.identity);
                collided.DecreaseNumber();
                if (collided.numOfShips <= 0)
                {
                    
                    collided.SetPlanetEnemyState();
                    collided.IncreaseNumber();
                    GameManager.instance.CheckWinCondition();
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
