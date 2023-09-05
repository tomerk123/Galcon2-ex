using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
    public SpriteRenderer _spriteRenderer;

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
        if(collided == _targetPlanet)
        {
        if (collided.isFrendly && this._spriteRenderer.color == Color.blue)
        {
            collided.IncreaseNumber();
            collided._numOfshipText.text = collided._numOfShips.ToString();
            Destroy(this.gameObject);
        }

        else if (collided.isEnemy && this._spriteRenderer.color == Color.red)
        {
            collided.IncreaseNumber();
            collided._numOfshipText.text = collided._numOfShips.ToString();
            Destroy(this.gameObject);
        }

        if (collided.isNuetral)
        {
            
            collided.DecreaseNumber();
            collided._numOfshipText.text = collided._numOfShips.ToString();
            if (collided._numOfShips <= 0)
            {
                collided._numOfShips = 1;
                collided.SetPlanetSettings(collided.GetPlanetStateByShip(this));
                collided.IncreaseNumber();
            }
            Destroy(this.gameObject);
        }

        if (collided.isEnemy && _spriteRenderer.color == Color.blue)
        {
            collided.DecreaseNumber();
            collided._numOfshipText.text = collided._numOfShips.ToString();

            if (collided._numOfShips <= 0)
            {
                collided._numOfShips = 1;
                collided.SetPlanetFreindlyState();
                collided.IncreaseNumber();
            }
            Destroy(this.gameObject);
        }

        // if (collided.isFrendly && this._spriteRenderer.color == Color.red)
        // {
        //     collided.DecreaseNumber();
        //     collided._numOfshipText.text = collided._numOfShips.ToString();
        //     if (collided._numOfShips <= 0)
        //     {
        //         collided._numOfShips = 1;
        //         collided.SetPlanetEnemyState();
        //         collided.IncreaseNumber();
        //     }
        //     Destroy(this.gameObject);
        // }
    }
    }


    void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPlanet.transform.position, _speed * Time.deltaTime);
        transform.up = _targetPlanet.transform.position - transform.position;
    }




}
