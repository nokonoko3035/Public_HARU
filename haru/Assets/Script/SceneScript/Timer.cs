using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
   
    //�b���J�E���g
    [SerializeField]private float seconds;

    //�@�O��Update�̎��̕b��
    private float oldSeconds;

    //�@�^�C�}�[�\���p�e�L�X�g
    private Text timer;

    // Start is called before the first frame update
    void Start()
    {
        seconds = 0f;
        oldSeconds = 0f;
        timer = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;
      
        //�@�l���ς�����������e�L�X�gUI���X�V
        if ((int)seconds != (int)oldSeconds)
        {
            timer.text = ((int)7-seconds).ToString("0");
        }
        oldSeconds = seconds;
    }
}
