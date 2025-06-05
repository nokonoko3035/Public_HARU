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

    [SerializeField] GameObject gameRuleUI; //�Q�[�����[��.
    [SerializeField] GameObject playRuleUI; //�������.
    

    [SerializeField] GameObject[] countdownUI;
    Animator gameRuleUI_animator;
    bool gameRuleUI_flg;
    Animator playRuleUI_animator;
    bool playRuleUI_flg;
    bool endFlg;
    float times = 0;

    Player player;
    /// <summary>
    /// ����������.
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
    /// �L�[ & �R���g���[���[����.
    /// </summary>
    void InputEvent()
    {
        //�Q�[���J�n�t���O��ON�ɂȂ����ꍇ�������s��Ȃ��悤�ɂ���.
        if (gameRuleUI_flg)
        {
            return;
        }

        ///�L�[�{�[�h.

        //��������\������.
        if ((playRuleUI_flg && Input.GetKeyDown(KeyCode.X)) || 
           (!playRuleUI_flg && Input.GetKeyDown(KeyCode.Y)) && !gameRuleUI_flg)
        {
            playRuleUI_flg = !playRuleUI_flg;
            playRuleUI_animator.SetBool("Flg", playRuleUI_flg);
        }
        //�Q�[���J�n����.
        if (Input.GetKeyDown(KeyCode.A)&& !playRuleUI_flg)
        {
            gameRuleUI_flg = true;
            gameRuleUI_animator.SetBool("Flg", gameRuleUI_flg);
        }

        ///�p�b�h
        //��������\������.
        if ((playRuleUI_flg && Input.GetButtonDown("Fire3")) ||
           (!playRuleUI_flg && Input.GetButtonDown("Jump")) && !gameRuleUI_flg)
        {
            playRuleUI_flg = !playRuleUI_flg;
            playRuleUI_animator.SetBool("Flg", playRuleUI_flg);
        }
        //�Q�[���J�n����.
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
        //�J�n����.
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
    /// �Q�[���J�n�̑O�ɗl�X�ȏ������J�n������.
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
