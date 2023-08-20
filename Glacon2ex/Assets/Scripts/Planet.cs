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
    private int _numOfShips;
    [SerializeField]
    private int _shipSpwanRate;
   
    private int _size;
    private float _spwanNewShipTimer;

    public GameObject _selectionIndicator;



    private enum PlanetColor
    {
        Red,
        Blue,
        Nuetral // gray
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
        _numOfShips = 100;
        _numOfshipText.text = _numOfShips.ToString();

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
        _selectionIndicator.SetActive(false);

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
