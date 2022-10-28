using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    [SerializeField]
    GameObject myBook;
    [SerializeField]
    GameObject myC1Title;
    [SerializeField]
    GameObject myC1V1;
    [SerializeField]
    GameObject myC1V2;
    [SerializeField]
    GameObject myC1V3;
    [SerializeField]
    GameObject myC1V4;
    [SerializeField]
    GameObject myC1V5;
    [SerializeField]
    GameObject myC2Title;
    [SerializeField]
    GameObject myC2V1;
    [SerializeField]
    GameObject myC2V2;
    [SerializeField]
    GameObject myC2V3;
    [SerializeField]
    GameObject myC2V4;

    [SerializeField]
    GameObject mySpeechBubble;
    [SerializeField]
    GameObject myFirstBubble;
    [SerializeField]
    GameObject mySecondBubble;
    [SerializeField]
    GameObject myThirdBubble;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenBook()
    {

    }
}
