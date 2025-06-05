using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
   
    //秒数カウント
    [SerializeField]private float seconds;

    //　前のUpdateの時の秒数
    private float oldSeconds;

    //　タイマー表示用テキスト
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
      
        //　値が変わった時だけテキストUIを更新
        if ((int)seconds != (int)oldSeconds)
        {
            timer.text = ((int)7-seconds).ToString("0");
        }
        oldSeconds = seconds;
    }
}
