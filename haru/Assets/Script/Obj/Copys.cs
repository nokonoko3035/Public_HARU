using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Copys : MonoBehaviour
{
    [SerializeField] GameObject[] Eggs;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 3 - PlayerPrefs.GetInt("EggCnt"); i++)
        {
            Eggs[i].SetActive(false);
        }
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
