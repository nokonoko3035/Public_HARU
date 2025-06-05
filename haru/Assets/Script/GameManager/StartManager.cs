using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    [SerializeField] GameObject MainTextBox;
    [SerializeField] LevelManager levelManager;
    [SerializeField] SoundManager soundManager;
    [SerializeField] GameObject timerText;

    [SerializeField] GameObject gameRuleUI; //ゲームルール.
    [SerializeField] GameObject playRuleUI; //操作説明.
    

    [SerializeField] GameObject[] countdownUI;
    Animator gameRuleUI_animator;
    bool gameRuleUI_flg;
    Animator playRuleUI_animator;
    bool playRuleUI_flg;
    bool endFlg;
    float times = 0;

    Player player;
    /// <summary>
    /// 初期化処理.
    /// </summary>
    void Init()
    {
        if (levelManager == null)
        {
            levelManager = GetComponent<LevelManager>();
        }
        if (soundManager == null)
        {
            soundManager = GetComponent<SoundManager>();
        }
        if(gameRuleUI != null)
        {
            gameRuleUI_animator = gameRuleUI.GetComponent<Animator>();
            gameRuleUI_flg = gameRuleUI_animator.GetBool("Flg");
        }

        if(playRuleUI != null)
        {
            playRuleUI_animator = playRuleUI.GetComponent<Animator>();
            playRuleUI_flg = playRuleUI_animator.GetBool("Flg");
        }

        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    
    
    /// <summary>
    /// キー & コントローラー処理.
    /// </summary>
    void InputEvent()
    {
        //ゲーム開始フラグがONになった場合処理を行わないようにする.
        if (gameRuleUI_flg)
        {
            return;
        }

        ///キーボード.

        //操作説明表示処理.
        if ((playRuleUI_flg && Input.GetKeyDown(KeyCode.X)) || 
           (!playRuleUI_flg && Input.GetKeyDown(KeyCode.Y)) && !gameRuleUI_flg)
        {
            playRuleUI_flg = !playRuleUI_flg;
            playRuleUI_animator.SetBool("Flg", playRuleUI_flg);
        }
        //ゲーム開始処理.
        if (Input.GetKeyDown(KeyCode.A)&& !playRuleUI_flg)
        {
            gameRuleUI_flg = true;
            gameRuleUI_animator.SetBool("Flg", gameRuleUI_flg);
        }

        ///パッド
        //操作説明表示処理.
        if ((playRuleUI_flg && Input.GetButtonDown("Fire3")) ||
           (!playRuleUI_flg && Input.GetButtonDown("Jump")) && !gameRuleUI_flg)
        {
            playRuleUI_flg = !playRuleUI_flg;
            playRuleUI_animator.SetBool("Flg", playRuleUI_flg);
        }
        //ゲーム開始処理.
        if (Input.GetButtonDown("Meow") && !playRuleUI_flg)
        {
            gameRuleUI_flg = true;
            gameRuleUI_animator.SetBool("Flg", gameRuleUI_flg);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        InputEvent();
        //開始処理.
        if (gameRuleUI_flg && !endFlg)
        {
            StratCheck();
            times += Time.deltaTime;
        }
        if (endFlg)
        {
            times += Time.deltaTime;
            if(times > 3)
            {
                MainTextBox.SetActive(false);
            }
        }
        
    }
    /// <summary>
    /// ゲーム開始の前に様々な処理を開始させる.
    /// </summary>
    void StratCheck()
    {
        if(times < 0.5f)
        {
            return;
        }
        if(soundManager != null)
        {
            if (soundManager.CountDownSE_Play() == 1)
            {
                if(countdownUI[soundManager.CountDown] != null)
                {
                   countdownUI[soundManager.CountDown].SetActive(true);          
                }
                
            }
            else if(soundManager.CountDownSE_Play() == 0)
            {
                endFlg = true;
                soundManager.Stage_Audio_Play();
                levelManager.stratFlg = true;
                times = 0;
                player.playFlg = true;
                timerText.SetActive(true);
            }
        }
    }

}
