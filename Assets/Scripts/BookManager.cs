using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    [SerializeField]
    public GameObject myBook;
    [SerializeField]
    public GameObject myC1Title;
    [SerializeField]
    public GameObject myC1V1;
    [SerializeField]
    public GameObject myC1V2;
    [SerializeField]
    public GameObject myC1V3;
    [SerializeField]
    public GameObject myC1V4;
    [SerializeField]
    public GameObject myC1V5;
    [SerializeField]
    public GameObject myC2Title;
    [SerializeField]
    public GameObject myC2V1;
    [SerializeField]
    public GameObject myC2V2;
    [SerializeField]
    public GameObject myC2V3;
    [SerializeField]
    public GameObject myC2V4;

    [SerializeField]
    public GameObject mySpeechBubble;
    [SerializeField]
    public GameObject myFirstBubble;
    [SerializeField]
    public GameObject mySecondBubble;
    [SerializeField]
    public GameObject myThirdBubble;

    [SerializeField]
    GameObject myContinueButton;

    bool myStartBookOpen = false;
    bool myFirstBubbleOpen = false;
    bool myFirstFirstVerse = false;
    bool mySecondBubbleOpen = false;
    bool mySecondFirstVerse = false;
    bool myThirdBubbleOpen = false;

    bool myStartLevel2 = false;
    bool myStartLevel3 = false;

    bool myIsLastPage = false;

    float myTimeStamp;

    float myWaitTime = 2f;

    [SerializeField]
    GameObject myCurrentOpen;
    GameObject myCurrentTitleOpen;

    List<GameObject> mySpeechBubbles = new List<GameObject>();

    Whispers myWhispers;
    bool myWhispersArePlaying = false;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Time.timeScale = 0;
        myWhispers = GetComponentInChildren<Whispers>();
        OpenBookChapterOneTitle();
    }

    // Update is called once per frame
    void Update()
    {
        if (myWaitTime < Time.realtimeSinceStartup - myTimeStamp && myStartBookOpen)
        {
            OpenSpeechBubbleOne();
        }
        if (myWaitTime < Time.realtimeSinceStartup - myTimeStamp && myFirstBubbleOpen)
        {
            Chapter1Verse1();
        }
        if (myWaitTime < Time.realtimeSinceStartup - myTimeStamp && mySecondBubbleOpen)
        {
            Chapter1Verse2();
        }
        if (myWaitTime < Time.realtimeSinceStartup - myTimeStamp && myThirdBubbleOpen)
        {
            BubblesCompleted();
        }

        if (myBook.activeSelf && !myWhispersArePlaying)
        {
            myWhispers.playWispers();
            myWhispersArePlaying = true;
        }
        else if (!myBook.activeSelf && myWhispersArePlaying)
        {
            myWhispers.StopWhispers();
            myWhispersArePlaying = false;
        }
    }



    public void OpenBookChapterOneTitle()
    {
        myStartBookOpen = true;
        myBook.SetActive(true);
        myC1Title.SetActive(true);
        myCurrentTitleOpen = myC1Title;
        myTimeStamp = Time.realtimeSinceStartup;
        
        //Play Background Music
    }

    public void OpenSpeechBubbleOne()
    {
        mySpeechBubble.SetActive(true);
        myFirstBubble.SetActive(true);
        myStartBookOpen = false;
        myTimeStamp = Time.realtimeSinceStartup;
        myFirstBubbleOpen = true;
    }
    public void Chapter1Verse1()
    {
        myFirstBubble.SetActive(false);
        mySpeechBubble.SetActive(false);
        myFirstBubbleOpen = false;
        myC1V1.SetActive(true);
        myFirstFirstVerse = true;
        myContinueButton.SetActive(true);
    }

    public void Continue()
    {
        if (myFirstFirstVerse)
        {
            mySpeechBubble.SetActive(true);
            mySecondBubble.SetActive(true);
            myFirstFirstVerse = false;
            myTimeStamp = Time.realtimeSinceStartup;
            mySecondBubbleOpen = true;
            myContinueButton.SetActive(false);
        }
        else if (mySecondFirstVerse)
        {
            mySpeechBubble.SetActive(true);
            myThirdBubble.SetActive(true);
            mySecondFirstVerse = false;
            myTimeStamp= Time.realtimeSinceStartup;
            myThirdBubbleOpen = true;
            myContinueButton.SetActive(false);
        }
        else if(myIsLastPage)
        {
            myIsLastPage = false;
            EndLevel();
        }
        else
        {
            myContinueButton.SetActive(false);
            myCurrentOpen.SetActive(false);
            myBook.SetActive(false);
            if (myStartLevel2)
            {
                FindObjectOfType<SpawnManager>().ActivateZoneOne();
                myStartLevel2 = false;
            }
            Time.timeScale = 1;
        }

 
    }

    public void EndLevel()
    {
        myContinueButton.SetActive(false);
        myCurrentOpen.SetActive(false);
        myCurrentTitleOpen.SetActive(false);
        myBook.SetActive(false);
        // !!!
        myStartLevel2 = false;
        myStartLevel3 = false;
        // !!!
        Time.timeScale = 1;
        GameManager.myInstance.myLevelManager.SetNewLevel();
    }

    public void Chapter1Verse2()
    {
        mySecondBubble.SetActive(false);
        mySpeechBubble.SetActive(false);
        myC1V1.SetActive(false);
        mySecondBubbleOpen = false;
        myC1V2.SetActive(true);
        mySecondFirstVerse = true;
        myContinueButton.SetActive(true);
    }

    public void BubblesCompleted()
    {
        myThirdBubble.SetActive(false);
        mySpeechBubble.SetActive(false);
        myC1V2.SetActive(false);
        myThirdBubbleOpen = false;
        myBook.SetActive(false);
        Time.timeScale = 1;
        FindObjectOfType<SpawnManager>().ActivateZoneOne();
    }

    public void Chapter1Verse3()
    {
        Time.timeScale = 0;
        myBook.SetActive(true);
        myC1V3.SetActive(true);
        myCurrentOpen = myC1V3;
        myContinueButton.SetActive(true);
        //Debuff: SLOW
    }

    public void Chapter1Verse4()
    {
        Time.timeScale = 0;
        myBook.SetActive(true);
        myC1V4.SetActive(true);
        myCurrentOpen = myC1V4;
        myContinueButton.SetActive(true);
        //Remove Debuff: SLOW
        //Buff: size/DMG
    }

    public void EndLevel1()
    {
        Time.timeScale = 0;
        myBook.SetActive(true);
        myC1V5.SetActive(true);
        myCurrentOpen = myC1V5;
        myContinueButton.SetActive(true);
        myIsLastPage = true;
    }

    public void Chapter2Verse1()
    {
        Time.timeScale = 0;
        myBook.SetActive(true);
        myC2Title.SetActive(true);
        myCurrentTitleOpen = myC2Title;
        myC2V1.SetActive(true);
        myCurrentOpen = myC2V1;
        myContinueButton.SetActive(true);
        myStartLevel2 = true;
        //Debuff: Zoomed In
    }

    public void Chapter2Verse2()
    {
        Time.timeScale = 0;
        myBook.SetActive(true);
        myC2V2.SetActive(true);
        myCurrentOpen = myC2V2;
        myContinueButton.SetActive(true);
        //Buff: Invulnerable for X seconds
    }

    public void Chapter2Verse3()
    {
        Time.timeScale = 0;
        myBook.SetActive(true);
        myC2V3.SetActive(true);
        myCurrentOpen = myC2V3;
        myContinueButton.SetActive(true);
        //Debuff: Health goes down X%
    }

    public void EndLevel2()
    {
        myBook.SetActive(true);
        myC2V4.SetActive(true);
        myCurrentOpen = myC2V4;
        myContinueButton.SetActive(true);
        myIsLastPage = true;
    }

}
