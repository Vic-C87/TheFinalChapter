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

    bool myStartBookOpen = false;
    bool myFirstBubbleOpen = false;
    bool myFirstFirstVerse = false;

    float myTimeStamp;

    float myWaitTime = 2f;

    GameObject nextToOpen = null;

    GameObject myCurrentOpen;

    List<GameObject> mySpeechBubbles = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Time.timeScale = 0;
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
    }



    public void OpenBookChapterOneTitle()
    {
        myStartBookOpen = true;
        myBook.SetActive(true);
        myC1Title.SetActive(true);
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
        myCurrentOpen = myC1V1;
        myFirstFirstVerse = true;
    }

}
