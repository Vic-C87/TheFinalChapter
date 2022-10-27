using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    Player myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GameManager.myInstance.GetPlayer();
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

    public void SetNewMeleeSprite(Sprite aWeaponSprite)
    {
        myMeleeWeaponSprite = aWeaponSprite;
    }

    public void SetNewRangedWeaponSprite(Sprite aWeaponSprite)
    {
        myRangedWeaponSprite = aWeaponSprite;
    }
}
