using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Inputs : MonoBehaviour
{

    private List<Planet> _selectedPlanets = new List<Planet>();
    


    private void Start()
    {

    }
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (Input.GetMouseButtonDown(0))
        {
            Planet ClickedObject = (hit.collider != null) ? hit.collider.gameObject.GetComponent<Planet>() : null;
            
            if (ClickedObject != null && ClickedObject.isFrendly)
            {
                _selectedPlanets.Add(ClickedObject);
                ClickedObject.SelectionIndicator.SetActive(true);
                ClickedObject._isClicked = true;
            }
            else if (ClickedObject == null)
            {
                foreach (Planet planet in _selectedPlanets)
                {
                    planet.SelectionIndicator.SetActive(false);
                    planet._isClicked = false;
                }
            }
        }

        if (hit.collider == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {

            if (hit.collider.tag == "EnemyPlanet" || hit.collider.tag == "NeutralPlanet" || hit.collider.tag == "FriendlyPlanet")
            {

                if (_selectedPlanets.Count > 0)
                {
                    Planet enemyPlanet = hit.collider.gameObject.GetComponent<Planet>();
                    foreach (Planet planet in _selectedPlanets)
                    {

                        planet.DeployShips(enemyPlanet);
                    }

                }
                _selectedPlanets.Clear();

            }
        }
    }



}

