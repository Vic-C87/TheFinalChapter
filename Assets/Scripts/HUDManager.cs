using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    Image myActiveWeaponSlot;
    [SerializeField]
    Image mySecondaryWeaponSlot;
    [SerializeField]
    Image myHeathBarFill;

    [SerializeField]
    Sprite myMeleeWeaponSprite;
    [SerializeField]
    Sprite myRangedWeaponSprite;

    [SerializeField]
    GameObject myEnemiesLeftHolder;
    [SerializeField]
    TextMeshProUGUI myEnemiesLeftValue;



    Player myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = FindObjectOfType<Player>();
        SetUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetUI()
    {
        UpdateWeaponSlots();
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        myHeathBarFill.fillAmount = myPlayer.GetCurrentHP() / myPlayer.GetMaxHP();
    }

    public void UpdateWeaponSlots()
    {
        if (myPlayer.GetIsRangedWeaponActivated())
        {
            myActiveWeaponSlot.sprite = myRangedWeaponSprite;
            mySecondaryWeaponSlot.sprite = myMeleeWeaponSprite;
        }
        else
        {
            myActiveWeaponSlot.sprite = myMeleeWeaponSprite;
            mySecondaryWeaponSlot.sprite = myRangedWeaponSprite;
        }
    }

    public void ActivateEnemiesLeftHolder(bool AToogle)
    {
        myEnemiesLeftHolder.SetActive(AToogle);
    }

    public void SetEnemiesLeftValue(int anAmount)
    {
        myEnemiesLeftValue.text = anAmount.ToString();
    }

    public void SetNewMeleeSprite(Sprite aWeaponSprite)
    {
        myMeleeWeaponSprite = aWeaponSprite;
    }

    public void SetNewRangedWeaponSprite(Sprite aWeaponSprite)
    {
        myRangedWeaponSprite = aWeaponSprite;
    }

    //public void ReloadPlayer()
    //{
    //    myPlayer = GameManager.myInstance.GetPlayer();
    //}
}
