using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("TitleSceneLoad", 7);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void TitleSceneLoad()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
