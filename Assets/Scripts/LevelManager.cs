using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    int myEnemiesInLevel = 0;
    bool myLevelIsComplete = false;

    bool myIsLevel1 = false;
    bool myIsLevel2 = false;
    bool myIsLevel3 = false;


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        myIsLevel1 = true;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (myLevelIsComplete)
        {
            if (myIsLevel1)
            {
                FindObjectOfType<BookManager>().EndLevel1();
            }
            else if (myIsLevel2)
            {
                FindObjectOfType<BookManager>().EndLevel2();
            }
            else if(myIsLevel3)
            {
                
            }

        }
    }

    public void AddEnemyToList(int aCountOfEnemies)
    {
        myEnemiesInLevel += aCountOfEnemies;
    }

    public void RemoveEnemyFromList()
    {
        myEnemiesInLevel--;
        if (myEnemiesInLevel < 6)
        {
            if (!myIsLevel3)
            {
                FindObjectOfType<HUDManager>().ActivateEnemiesLeftHolder(true);
                FindObjectOfType<HUDManager>().SetEnemiesLeftValue(myEnemiesInLevel);
            }
            if (myEnemiesInLevel < 1)
            {
                myLevelIsComplete = true;
            }
        }
        
    }

    public void SetNewLevel()
    {
        if (myIsLevel1)
        {
            myIsLevel1 = false;
            myIsLevel2 = true;
            myLevelIsComplete = false;
            SceneManager.LoadScene("Level-2");
            GameManager.myInstance.GetPlayer().SetLevel(2);
            FindObjectOfType<BookManager>().Chapter2Verse1();
        }
        else if (myIsLevel2)
        {
            myIsLevel2 = false;
            myIsLevel3 = true;
            myLevelIsComplete = false;
            SceneManager.LoadScene("Level-3 BOSS");
            GameManager.myInstance.GetPlayer().SetLevel(3);
            FindObjectOfType<BookManager>().Chapter3Verse1();
        }
        FindObjectOfType<HUDManager>().ActivateEnemiesLeftHolder(false);
    }
}
