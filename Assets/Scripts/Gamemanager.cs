using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager myInstance;

    Queue<Seeker> myEnemies = new Queue<Seeker>();
    List<Seeker> myInactives = new List<Seeker>();
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

    public void DequeueMe(Seeker aSeeker)
    {
        if (!myInactives.Contains(aSeeker))
        {
            myInactives.Add(aSeeker);
        }

    }

    public void AddNewPathSeeker(Seeker aSeeker)
    {
        if (!myEnemies.Contains(aSeeker))
        {
            if (myInactives.Contains(aSeeker))
                myInactives.Remove(aSeeker);
            myEnemies.Enqueue(aSeeker);
        }
    }

    public void RequestNewPath()
    {
        if (/*!myIsFindingPath && */myEnemies.Count > 0)
        {
            Seeker seeker = myEnemies.Dequeue();
            if (!myInactives.Contains(seeker))
            seeker.Seek();
            myIsFindingPath = true;
        }
    }

    public bool CheckIfActiveSeeker(Seeker aSeeker)
    {
        return !myInactives.Contains(aSeeker);
    }



}
