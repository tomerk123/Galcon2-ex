using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Inputs : MonoBehaviour
{

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (Input.GetMouseButtonDown(0))
        {
            Planet ClickedObject = (hit.collider != null) ? hit.collider.gameObject.GetComponent<Planet>() : null;

            if (ClickedObject != null && ClickedObject.isFrendly)
            {
                GameManager.instance.Select(ClickedObject);
            }
            else if (ClickedObject == null)
            {
                GameManager.instance.ClearSelection();
            }
        }

        if (hit.collider == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Planet planetHit = hit.collider.gameObject.GetComponent<Planet>();
            if (planetHit.isEnemy || planetHit.isNuetral || planetHit.isFrendly)
            {
                Planet enemyPlanet = hit.collider.gameObject.GetComponent<Planet>();
                foreach (Planet planet in GameManager.instance.selectedPlanets)
                {
                    planet.DeployShips(enemyPlanet);
                }
                GameManager.instance.ClearSelection();
            }
        }
    }



}

