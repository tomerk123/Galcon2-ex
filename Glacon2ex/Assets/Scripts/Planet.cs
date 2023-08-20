using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class Planet : MonoBehaviour
{
    [SerializeField]
    public SpriteRenderer _planetRebderer;
    [SerializeField]
    private GameObject _shipPrefab;
    [SerializeField]
    private PlanetColor _planetColor;
    [SerializeField]
    private TMPro.TextMeshPro _numOfshipText;
    [SerializeField]
    public int _numOfShips;
    [SerializeField]
    private int _shipSpwanRate;

    private PlanetState _planetState;
    public GameObject _selectionIndicator;
    
//    private int _size = Random.Range(1, 5);
    private float _spwanNewShipTimer;
    public bool _isClicked = false;
    public bool _isFrendly = false;
    private int _startingShips = 100;


    public enum PlanetState
    {
        Friendly,
        Enemy,
        Neutral
    }

    public enum PlanetColor
    {
        Red,
        Blue,
        Nuetral
    }

    private void Update()
    {
        SpwanNewShipTimer();
    }

    void SpwanNewShipTimer()
    {
        _spwanNewShipTimer += Time.deltaTime;
        if (_spwanNewShipTimer >= _shipSpwanRate)
        {
            _numOfShips++;
            _numOfshipText.text = _numOfShips.ToString();
            _spwanNewShipTimer = 0;
        }
    }

    void Start()
    {
        SetPlanetColor();
        setPlanetState();
        _numOfShips =_startingShips;
        _numOfshipText.text = _numOfShips.ToString();
    }

    private void setPlanetState()
    {
        switch (_planetState)
        {
            case PlanetState.Friendly:
                _isFrendly = true;
                break;
            case PlanetState.Enemy:
                _isFrendly = false;
                break;
            case PlanetState.Neutral:
                _isFrendly = false;
                break;
        }
    }
    private void SetPlanetColor()
    {
        switch (_planetColor)
        {
            case PlanetColor.Red:
                _planetRebderer.material.color = Color.red;
                break;
            case PlanetColor.Blue:
                _planetRebderer.material.color = Color.blue;
                break;
            case PlanetColor.Nuetral:
                _planetRebderer.material.color = Color.gray;
                break;
        }
    }

    public void DeployShips(Planet targetPlanet)
    {
        for (int i = 0; i < _numOfShips; i++)
        {
            Instantiate(_shipPrefab, transform.position, Quaternion.identity);
        }
        _numOfShips = _numOfShips / 2;
        _numOfshipText.text = _numOfShips.ToString();
    }


    private void OnMouseEnter()
    {
        _selectionIndicator.SetActive(true);
    }

    private void OnMouseExit()
    {
        if (!_isClicked)
        {
            _selectionIndicator.SetActive(false);
        }
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
}
