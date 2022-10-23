using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    static Gamemanager myGamemanager;

    void Awake()
    {
        if (myGamemanager != null && myGamemanager != this)
        {
            Destroy(this.gameObject);         
        }
        else
        {
            myGamemanager = this;
        }
    }

}
