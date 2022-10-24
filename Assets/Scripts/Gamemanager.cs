using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager myInstance;

    Queue<Seeker> myEnemies = new Queue<Seeker>();
    public bool myIsFindingPath = false;

    [SerializeField]
    Transform myPlayer;

    void Awake()
    {
        if (myInstance != null && myInstance != this)
        {
            Destroy(this.gameObject);         
        }
        else
        {
            myInstance = this;
        }
    }

    private void Update()
    {
        RequestNewPath();
    }

    public Transform GetPlayer()
    {
        return myPlayer;
    }

    public void AddNewPathSeeker(Seeker aSeeker)
    {
        if (!myEnemies.Contains(aSeeker))
        {
            myEnemies.Enqueue(aSeeker);
        }
    }

    public void RequestNewPath()
    {
        if (!myIsFindingPath && myEnemies.Count > 0)
        {
            myEnemies.Dequeue().Seek();
            myIsFindingPath = true;
        }
    }

}
