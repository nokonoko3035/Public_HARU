using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    [SerializeField] string[] sceneS;

    [SerializeField] GameObject lTextBox;
    [SerializeField] GameObject rTextBox;

    [SerializeField] AudioClip clip1;
    [SerializeField] AudioClip clip2;

    new AudioSource audio;
    //選択されているカウント.
    int chCnt;

    float time;
    // Start is called before the first frame update
    void Start()
    {
        buttons[chCnt].Select();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        InputEvent();
        times();
    }
    void times()
    {
        time += Time.deltaTime;
    }

    void InputEvent()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if ((Input.GetKeyDown(KeyCode.UpArrow) || y > 0.5f) && time > 0.2f)
        {
            time = 0;
            if (chCnt >= buttons.Length - 1) { chCnt = 0; }
            else { chCnt++; }           
            audio.PlayOneShot(clip1);
            buttons[chCnt].Select();
 
        }
        if ((Input.GetKeyDown(KeyCode.DownArrow) || y < -0.5f) && time > 0.2f)
        {
            time = 0;
            if (chCnt < buttons.Length - 1) { chCnt = buttons.Length - 1; }
            else { chCnt--; }
            audio.PlayOneShot(clip1);
            buttons[chCnt].Select();
        }

        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetButtonDown("Jump") || x > 0.5f) && time > 0.5)
        {
            if (lTextBox != null)
            {
                if (!lTextBox.GetComponent<Animator>().GetBool("Flg"))
                {
                    lTextBox.GetComponent<Animator>().SetBool("Flg", true);
                }
                time = 0;
            }
        }
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || x < -0.5f) && time > 1f)
        {
            if (lTextBox != null)
            {
                if (lTextBox.GetComponent<Animator>().GetBool("Flg"))
                {
                    lTextBox.GetComponent<Animator>().SetBool("Flg", false);
                }
                time = 0;
            }
        }

        //キャンセル.
        if (Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("Fire3")){
            if(lTextBox != null)
            {
                lTextBox.GetComponent<Animator>().SetBool("Flg", false);
            }
        }


        ///決定.
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Meow"))
        {
            audio.PlayOneShot(clip2);
            buttons[chCnt].onClick.AddListener(Ct);
        }
    }
    void Ct()
    {
        Invoke("LoadScene", 0.2f);
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneS[chCnt]);
    }
}


