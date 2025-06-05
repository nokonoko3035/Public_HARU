using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCount : MonoBehaviour
{
    //モードの種類
    enum Mode
    {
        NORMAL,     //Normalモード
        HARD        //HARDモード
    }

    //モードセット
    [SerializeField] Mode mode;

    //時間変数
    float countTime;

    // Start is called before the first frame update
    void Start()
    {
        InitMode();         //モードの初期化関数
        InitText();         //テキストの初期化関数
    }

    void Update()
    {
        CountDownTime();    //時間の減少とテキスト更新関数
    }

    //モードの初期化関数
    void InitMode()
    {
        //モードを参照して時間をセット
        switch (mode)
        {
            case Mode.NORMAL:
                countTime = 150;    //Normalなら150秒
                break;
            case Mode.HARD:
                countTime = 600;    //HARDなら600秒
                break;
            default:
                Debug.Log("エラー");
                countTime = 0;
                break;
        }
    }

    //テキストの初期化関数
    void InitText()
    {
        GetComponent<Text>().text = "残り時間:" + countTime + "秒";
    }

    //時間の減少とテキスト更新
    void CountDownTime()
    {
        //countTimeに、ゲームが開始してからの秒数を格納
        countTime -= Time.deltaTime;
        //テキストを更新して表示
        GetComponent<Text>().text = "残り時間:" + countTime.ToString("00") + "秒";

    }
}
