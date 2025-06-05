using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip   Stage_Audio;
    [SerializeField] AudioClip[] CountDownSEs;

    new AudioSource audio;

    //カウントダウンの処理.
    public int CountDown = 0;

    /// <summary>
    /// 初期化処理.
    /// </summary>
    void Init()
    {
        //カウントダウン SEの最大数.
        CountDown = CountDownSEs.Length;
        audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    /// <summary>
    /// カウントダウン用の関数.
    /// </summary>
    /// <returns></returns>
    public int CountDownSE_Play()
    {
        //もしもサウンドが再生されているなら処理中として 1を返す.
        if (audio.isPlaying)
        {
            return 2;
        }
        CountDown--;
        if (CountDown > -1)
        {
            //サウンドを流す.
            audio.clip = CountDownSEs[CountDown];
            audio.Play();
            return 1;
        }
        else
        {
            //これ以上のサウンドがありません.
            return 0;
        }
    }
    
    /// <summary>
    /// ステージBGMを流す.
    /// </summary>
    public void Stage_Audio_Play()
    {
        audio.loop = true;
        audio.clip = Stage_Audio;
        audio.Play();
    }
}
