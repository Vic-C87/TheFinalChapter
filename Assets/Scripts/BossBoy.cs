using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBoy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Win()
    {
        Destroy(GameObject.Find("Audio Manager"));
        Destroy(GameObject.Find("LevelManager"));
        Destroy(GameObject.Find("Player"));
        Destroy(GameObject.Find("HUD"));

        SceneManager.LoadScene("WinScreen");
    }
}
