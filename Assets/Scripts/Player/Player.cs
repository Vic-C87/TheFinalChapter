using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField]
    List<Seeker> myEnemies;
    [SerializeField]
    Vector3 myPosition;

    // Start is called before the first frame update
    void Start()
    {
        myPosition = transform.position;
        SetEnemies();
    }

    void Update()
    {
        if (transform.position != myPosition)
        {
            //NewPosition();
            myPosition = transform.position;
        }
        else
        {
            //NewPosition();
        }


    }

    void NewPosition()
    {
        foreach (Seeker enemy in myEnemies)
        {
            GameManager.myInstance.AddNewPathSeeker(enemy);
        }
    }

    public void SetEnemies()
    {
        Seeker[] enemies = FindObjectsOfType<Seeker>();
        for (int i = 0; i < enemies.Length; i++)
        {
            myEnemies.Add(enemies[i]);
        }
    }
}
