using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    private static StartScreen _instance;

    public int NumOfPlanets { get; private set; }
    public static StartScreen Instance => _instance;

    private void Awake()
    {

        if (_instance == null)
        {
            _instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }


    void Start()
    {

    }

    public void SetNumOfPlanets(int num)
    {
        NumOfPlanets = num;
    }
}
