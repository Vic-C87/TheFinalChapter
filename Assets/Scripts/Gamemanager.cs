using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager myInstance;

    Queue<Seeker> myEnemies = new Queue<Seeker>();
    List<Seeker> myInactives = new List<Seeker>();
    public bool myIsFindingPath = false;

    [SerializeField]
    Player myPlayer;

    List<Vector2> myWanderDirections = new List<Vector2>();

    int myRandomizedDirectionChoice;

    public LevelManager myLevelManager;

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
        CreateDirectionList();
        myPlayer = FindObjectOfType<Player>();
        myLevelManager = FindObjectOfType<LevelManager>();
    }

    private void Update()
    {
        RequestNewPath();
    }

    public Player GetPlayer()
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
            if (seeker != null)
            {
                if (!myInactives.Contains(seeker))
                seeker.Seek();
                myIsFindingPath = true;
            }
        }
    }

    public bool CheckIfActiveSeeker(Seeker aSeeker)
    {
        return !myInactives.Contains(aSeeker);
    }

    public int GetRandomNumber(int aLowest, int aHighest)
    {
        return Random.Range(aLowest, aHighest +1);
    }

    public Vector2 GetWanderDirection()
    {
        Vector2 direction = new Vector2();
        myRandomizedDirectionChoice = GetRandomNumber(0,7);
        direction = myWanderDirections[myRandomizedDirectionChoice];

        return direction;
    }

    void CreateDirectionList()
    {
        Vector2 direction = new Vector2();

        direction = new Vector2(-1,1);
        myWanderDirections.Add(direction);

        direction = new Vector2(0,1);
        myWanderDirections.Add(direction);

        direction = new Vector2(1, 1);
        myWanderDirections.Add(direction);

        direction = new Vector2(-1, 0);
        myWanderDirections.Add(direction);

        direction = new Vector2(1, 0);
        myWanderDirections.Add(direction);

        direction = new Vector2(-1, -1);
        myWanderDirections.Add(direction);

        direction = new Vector2(0, -1);
        myWanderDirections.Add(direction);

        direction = new Vector2(1, -1);
        myWanderDirections.Add(direction);
    }

    public HUDManager GetHUDManager()
    {
        return FindObjectOfType<HUDManager>();
    }
}

public struct SWeapon
{
    public string myWeaponName;
    public bool myIsRanged;
    public float myDamage;
    public float myMeleeDistance;
    public Sprite mySprite;
    public Sprite myProjectileSprite;

    public SWeapon(string aWeaponName, bool anIsRangedToggle, float someDamage, float aMeleeDistance, Sprite aSprite, Sprite aProjectileSprite)
    {
        myWeaponName = aWeaponName;
        myIsRanged = anIsRangedToggle;
        myDamage = someDamage;
        myMeleeDistance = aMeleeDistance;
        mySprite = aSprite;
        myProjectileSprite = aProjectileSprite;
    }
}
