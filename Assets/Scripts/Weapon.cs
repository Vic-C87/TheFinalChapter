using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    string myWeaponName;
    [SerializeField]
    bool myIsRanged;
    [SerializeField]
    float myDamage;
    [SerializeField]
    float myMeleeDistance;
    [SerializeField]
    Sprite mySprite;
    [SerializeField]
    Sprite myProjectileSprite;

    SWeapon myWeapon;
    SpriteRenderer mySpriteRenderer;

    [SerializeField]
    GameObject myPickUpText;

    bool myWeaponPickedUp = false;

    // Start is called before the first frame update
    void Start()
    {
        myWeapon = new SWeapon(myWeaponName, myIsRanged, myDamage, myMeleeDistance, mySprite, myProjectileSprite);
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        mySpriteRenderer.sprite = mySprite;
        myPickUpText.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (myWeaponPickedUp)
        {
            Destroy(this.gameObject);
        }
    }

    public SWeapon PickUpWeapon()
    {
        myWeaponPickedUp = true;
        return myWeapon;
    }

    public void ActivatePickUpText(bool aToogle)
    {
        myPickUpText.SetActive(aToogle);
    }


}
