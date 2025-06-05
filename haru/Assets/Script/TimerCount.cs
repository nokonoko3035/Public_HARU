using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCount : MonoBehaviour
{
    //���[�h�̎��
    enum Mode
    {
        NORMAL,     //Normal���[�h
        HARD        //HARD���[�h
    }

    //���[�h�Z�b�g
    [SerializeField] Mode mode;

    //���ԕϐ�
    float countTime;

    // Start is called before the first frame update
    void Start()
    {
        InitMode();         //���[�h�̏������֐�
        InitText();         //�e�L�X�g�̏������֐�
    }

    void Update()
    {
        CountDownTime();    //���Ԃ̌����ƃe�L�X�g�X�V�֐�
    }

    //���[�h�̏������֐�
    void InitMode()
    {
        //���[�h���Q�Ƃ��Ď��Ԃ��Z�b�g
        switch (mode)
        {
            case Mode.NORMAL:
                countTime = 150;    //Normal�Ȃ�150�b
                break;
            case Mode.HARD:
                countTime = 600;    //HARD�Ȃ�600�b
                break;
            default:
                Debug.Log("�G���[");
                countTime = 0;
                break;
        }
    }

    //�e�L�X�g�̏������֐�
    void InitText()
    {
        GetComponent<Text>().text = "�c�莞��:" + countTime + "�b";
    }

    //���Ԃ̌����ƃe�L�X�g�X�V
    void CountDownTime()
    {
        //countTime�ɁA�Q�[�����J�n���Ă���̕b�����i�[
        countTime -= Time.deltaTime;
        //�e�L�X�g���X�V���ĕ\��
        GetComponent<Text>().text = "�c�莞��:" + countTime.ToString("00") + "�b";

    }
}
