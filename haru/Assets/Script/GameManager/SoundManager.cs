using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip   Stage_Audio;
    [SerializeField] AudioClip[] CountDownSEs;

    new AudioSource audio;

    //�J�E���g�_�E���̏���.
    public int CountDown = 0;

    /// <summary>
    /// ����������.
    /// </summary>
    void Init()
    {
        //�J�E���g�_�E�� SE�̍ő吔.
        CountDown = CountDownSEs.Length;
        audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    /// <summary>
    /// �J�E���g�_�E���p�̊֐�.
    /// </summary>
    /// <returns></returns>
    public int CountDownSE_Play()
    {
        //�������T�E���h���Đ�����Ă���Ȃ珈�����Ƃ��� 1��Ԃ�.
        if (audio.isPlaying)
        {
            return 2;
        }
        CountDown--;
        if (CountDown > -1)
        {
            //�T�E���h�𗬂�.
            audio.clip = CountDownSEs[CountDown];
            audio.Play();
            return 1;
        }
        else
        {
            //����ȏ�̃T�E���h������܂���.
            return 0;
        }
    }
    
    /// <summary>
    /// �X�e�[�WBGM�𗬂�.
    /// </summary>
    public void Stage_Audio_Play()
    {
        audio.loop = true;
        audio.clip = Stage_Audio;
        audio.Play();
    }
}
