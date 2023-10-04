using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum PlanetState
{
    Friendly,
    Enemy,
    Neutral
}

public class Planet : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshPro _numOfshipText;
    [SerializeField]
    private int _numOfShips;
    [SerializeField]
    private float _shipSpawnDelay;
    [SerializeField]
    private Transform _spawnPosition;
    [SerializeField]
    private SpaceShip _shipPrefab;

    [SerializeField]
    private int _startingShips = 100; // CR: no defaults in the code

    [SerializeField] private GameObject _selectionIndicator;
    private bool _isHovered;

    private PlanetState _planetState;
    private SpriteRenderer _spriteRenderer;
    private float _spwanNewShipTimer;

    public PlanetState planetState => _planetState;
    
    private float _size;

    public int numOfShips => _numOfShips;

    private void Update()
    {
        ShipSpwanUpdate();
        if (isClicked || _isHovered)
        {
            _selectionIndicator.SetActive(true);
        }
        else
        {
            _selectionIndicator.SetActive(false);
        }
    }

    void SpwanNewShipTimer()
    {
        _spwanNewShipTimer += Time.deltaTime;
        if (_spwanNewShipTimer >= _shipSpawnDelay)
        {
            _numOfShips++;
            _numOfshipText.text = _numOfShips.ToString();
            _spwanNewShipTimer = 0;
        }
    }

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        ShipUpdate();
    }

    // CR: [discuss] single source of truth
    void ShipUpdate()
    {
        if (isFrendly || isEnemy)
        {
            _size = transform.localScale.x;
            _shipSpawnDelay /= _size;
            _numOfShips = _startingShips;
            _numOfshipText.text = _numOfShips.ToString();
        }
        else
        {
            _numOfShips = Random.Range(10, 25);
            _numOfshipText.text = _numOfShips.ToString();
        }

    }
    public void DeployShips(Planet targetPlanet)
    {
        int numShipsToDeploy = _numOfShips / 2;
        _numOfShips -= numShipsToDeploy;
        _numOfshipText.text = _numOfShips.ToString();
        for (int i = 0; i < numShipsToDeploy; i++)
        {
            // CR: rename Ship -> ship
            // CR: [discuss] _spriteRenderer and _targetPlanet shouldn't be public - replace with
            //     a public Init function.
            SpaceShip Ship = Instantiate(_shipPrefab, _spawnPosition.position, Quaternion.identity);
            Ship._spriteRenderer.color = this._spriteRenderer.color;
            Ship.GetComponent<SpaceShip>()._targetPlanet = targetPlanet;
        }
    }

    private void OnMouseEnter()
    {
        _isHovered = true;
    }

    private void OnMouseExit()
    {
        _isHovered = false;
    }

    public void IncreaseNumber()
    {
        _numOfShips++;
        _numOfshipText.text = _numOfShips.ToString();
    }

    public void DecreaseNumber()
    {
        _numOfShips--;
        _numOfshipText.text = _numOfShips.ToString();
    }

    public void SetNumOfShips(int numOfShips)
    {
        _numOfShips = numOfShips;
    }

    public bool isFrendly => _planetState == PlanetState.Friendly;
    public bool isEnemy => _planetState == PlanetState.Enemy;
    public bool isNuetral => _planetState == PlanetState.Neutral;

    void ShipSpwanUpdate()
    {
        if (_planetState == PlanetState.Friendly || _planetState == PlanetState.Enemy)
        {
            SpwanNewShipTimer();
        }
    }

    public void SetPlanetState(PlanetState planetState) {
        _planetState = planetState;

        switch (_planetState)
        {
            case PlanetState.Enemy:
                _planetState = PlanetState.Enemy; // CR: delete this (already done in "_planetState = planetState"). same below.
                _spriteRenderer.color = Color.red;
                break;
            case PlanetState.Friendly:
                _planetState = PlanetState.Friendly;
                _spriteRenderer.color = Color.blue;
                break;
            case PlanetState.Neutral:
                _planetState = PlanetState.Neutral;
                _spriteRenderer.color = Color.gray;
                break;
        }
    }

    public bool isClicked
    {
        get
        {
            foreach (Planet planet in GameManager.instance.selectedPlanets)
            {
                if (this.gameObject.GetInstanceID() == planet.gameObject.GetInstanceID())
                {
                    return true;
                }
            }
            return false;
        }
    }


}
