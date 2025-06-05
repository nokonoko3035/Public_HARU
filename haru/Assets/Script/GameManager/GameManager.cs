using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int stackCnt;
    [SerializeField] SpriteRenderer imgObj;
    [SerializeField] Sprite[] imgs;
    [SerializeField] StartManager startManager;
    // Start is called before the first frame update
    void Start()
    {
        ImgUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if(stackCnt == 0)
        {
            EggCheck();
        }
    }
    /// <summary>
    /// 現在卵があるかどうか.
    /// </summary>
    public GameObject EggCheck()
    {
        GameObject egg = null;
        egg = GameObject.FindGameObjectWithTag("Egg");
        if(egg == null)
        {
            print("ゲーム終了");
            GameOver();
        }
        return egg;
    }

    /// <summary>
    /// 画像データをアップデート.
    /// </summary>
    void ImgUpdate()
    {
        imgObj.sprite = imgs[stackCnt];
    }
    /// <summary>
    /// 残機を減らす処理.
    /// </summary>
    public void DownStack()
    {
        stackCnt--;
        ImgUpdate();
        if(stackCnt == 0)
        {
            EggCheck();
        }
    }
    /// <summary>
    /// 残機を増やす処理.
    /// </summary>
    public void UpStack() 
    {
        stackCnt++;
        ImgUpdate();
    }
    /// <summary>
    /// ゲームクリア処理 巣の中にいくつかるか, 0だったらゲームオーバー.
    /// </summary>
    public void GameClear()
    {
        //残機類の保存.
        if (stackCnt == 0)
        {
            GameOver();
            return;
        }
        //ゲームクリア.
        PlayerPrefs.SetInt("EggCnt", stackCnt);
        //シーン切り替え.
        SceneManager.LoadScene("ClearScene");
    }
    /// <summary>
    /// ゲームオーバー.
    /// </summary>
    public void GameOver()
    {
        //シーン切り替え.
        SceneManager.LoadScene("GameOverScene");
    }
}
