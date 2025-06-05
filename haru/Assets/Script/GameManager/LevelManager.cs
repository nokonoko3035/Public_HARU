using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelManager : MonoBehaviour
{
    [SerializeField] TextAsset csvData = null;
    [SerializeField] float levelUpTime;
    [SerializeField] int Level;
    [SerializeField] GameObject[] Enemys;
    [SerializeField] string[] lines;
    [SerializeField] GameManager gameManager;
    [SerializeField] StartManager startManager;
    [SerializeField] SoundManager soundManager;
    //time.
    float Leveltime;
    float Time01;
    float Time02;

    float ct01;
    float ct02;

    //スタート時処理を開始するためのフラグ.
    public bool stratFlg;
    /// <summary>
    /// Csvなどの読み込み処理です.
    /// </summary>
    void Lood()
    {
        // CSV読み込み.
        lines = csvData.text.Split('\n');

        gameManager = GetComponent<GameManager>();
    }
    /// <summary>
    /// 時間処理.
    /// </summary>
    void Times()
    {
        Time01 += Time.deltaTime;
        Time02 += Time.deltaTime;
        Leveltime += Time.deltaTime;
    }
    /// <summary>
    /// レベルアップ処理.
    /// </summary>
    void LevelUp()
    {
        if(Leveltime > levelUpTime)
        {
            if(Level < lines.Length - 3)
            {
                Level++;
            }
            else
            {
                //クリア.
                gameManager.GameClear();
            }
            Leveltime = 0;
            CtUpdate(0);
        }
    }
    /// <summary>
    /// クールタイムを初期化させる. 0: 全て 1:一番目 2:二番目
    /// </summary>
    void CtUpdate(int data)
    {
        // 何行目?
        string[] values = lines[Level + 1].Split(',');
        bool flag = bool.Parse(values[1]);     
        if (data == 0 || data == 1)
        {
            if (flag)
            {
                ct01 = Random.Range(float.Parse(values[2]), float.Parse(values[3]));
            }
            else
            {
                ct01 = float.Parse(values[2]);
            }
        }
        //error対策.
        if(string.Format(null, values[4]) == "")
        {
            return;
        }
        flag = bool.Parse(values[5]);
        if (data == 0 || data == 2)
        {
            if (flag)
            {
                ct02 = Random.Range(float.Parse(values[6]), float.Parse(values[7]));
            }
            else
            {
                ct02 = float.Parse(values[6]);
            }
        }
    }
    /// <summary>
    /// メインとなる出現処理。
    /// </summary>
    void MeinEnter()
    {
        // 何行目?
        string[] values = lines[Level + 1].Split(',');

        GameObject enemy = SearchObj(string.Format(null, values[0]));
        bool flag = bool.Parse(values[1]);
        if (enemy != null && Time01 > ct01)
        {
            EnterObj(enemy);
            Time01 = 0;
            CtUpdate(1);
        }
        enemy = SearchObj(string.Format(null, values[4]));
        if (enemy != null && Time02 > ct02)
        {
            EnterObj(enemy);
            Time02 = 0;
            CtUpdate(2);
        }
    }
    /// <summary>
    /// 出現処理.
    /// </summary>
    /// <param name="obj"></param>
    void EnterObj(GameObject obj)
    {
       GameObject data = Instantiate(obj);
       //親子関係.
       data.transform.parent = transform;
    }
    /// <summary>
    /// 対象の名前を送ることで配列内のゲームオブジェクトを引っ張ることができます。
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    GameObject SearchObj(string name)
    {
        GameObject obj = null;
        
        for(int i = 0; i < Enemys.Length; i++)
        {
            if(Enemys[i].name == name)
            {
                return Enemys[i];
            }
        }
        return obj;
    }
    /// <summary>
    /// 初期化処理.
    /// </summary>
    void Init()
    {
        // スタートマネージャーがないなら取得処理.
        if (startManager == null)
        {
            startManager = GetComponent<StartManager>();
        }
        // それでもないならゲームを開始する.
        if(startManager == null)
        {
            stratFlg = false;
            soundManager.Stage_Audio_Play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Lood();
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(stratFlg)
        {
            Times();
            LevelUp();
            MeinEnter();
        }

    }
}
