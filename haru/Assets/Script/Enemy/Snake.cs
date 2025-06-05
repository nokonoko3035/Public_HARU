using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // カメラの範囲取得.
    Vector2 cmin, cmax;
    [SerializeField] string tagName;
    [SerializeField] float speed;
    [SerializeField] float plusPos;

    GameObject target;
    GameManager gameManager;
    bool speedDownFlg;
    //カメラ内に入ったらかどうかフラグで管理.
    bool cmeraFlg;
    //当たったかどうか.
    bool hitFlag;
    /// <summary>
    /// 初期化処理,
    /// </summary>
    void Init()
    {
        //カメラの範囲取得.
        cmin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        cmax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        //位置変更.
        transform.position = Point();

        float px = transform.position.x;
        if(px < 0)
        {
            transform.eulerAngles = new Vector3(0, 0,-90);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        //対象のタグを参照する.
        target = GameObject.FindWithTag(tagName);

        //ゲームマネージャーを取得.
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        MoveAngle();
        Chack();
    }
    /// <summary>
    /// 移動処理.
    /// </summary>
    void Move()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }
    /// <summary>
    /// angle処理.
    /// </summary>
    void MoveAngle()
    {
        float tagX = target.transform.position.x;
        float posX = transform.position.x;
        if(tagX < posX + 1.2f && tagX > posX - 1.5f)
        {
            transform.up = Vector3.MoveTowards(transform.up,
                                target.transform.position - transform.position,
                                1 * Time.deltaTime
                                );
            if (!speedDownFlg)
            {
                speedDownFlg = true;
                speed = speed * 0.2f;
            } 
        }
    }

    /// <summary>
    /// このオブジェクトの状態処理.
    /// </summary>
    void Chack()
    {
        //カメラ外になったら.
        if (Camera_Chack() != 0 && cmeraFlg)
        {
            Destroy(gameObject);
        }
        //カメラ内に入ったか.
        if (Camera_Chack() == 0 && !cmeraFlg)
        {
            cmeraFlg = true;
        }
    }
    /// <summary>
    /// 画面外かどうか確認する.
    /// 引数 : 0 / -1  (カメラ内 / カメラ外).
    /// </summary>
    /// <returns></returns>
    int Camera_Chack()
    {
        //現在 座標を更新.
        Vector2 Pos = transform.position;
        if (Pos.x < cmin.x - 1 || Pos.y < cmin.y - 1 || Pos.x > cmax.x + 1 || Pos.y > cmax.y + 1)
        {
            //カメラ外.
            return -1;
        }
        //カメラ内.
        return 0;
    }

    /// <summary>
    /// camera範囲外からランダムの座標値を返す.
    /// </summary>
    /// <returns></returns>
    Vector3 Point()
    {
        //念のための初期化.
        Vector3 Pos = Vector3.zero;
        //ランダムに出現位置を変更.
        switch (Random.Range(0, 2))
        {
            case 0: Pos = new Vector3(cmin.x - plusPos, -4.4f, 0); break;  //←
            case 1: Pos = new Vector3(cmax.x + plusPos, -4.4f, 0); break;  //→
        }
        // 位置を返す.
        return Pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit");
        if(collision.gameObject.tag == tagName)
        {
            speed = -(speed * 3);
            if(gameManager.stackCnt != 0)
            {
                gameManager.DownStack();
            }
        }

        if(collision.gameObject.tag == "Egg")
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Stone" && !hitFlag)
        {
            hitFlag = true;
            GetComponent<AudioSource>().Play();
            if (speedDownFlg)
            {
                speed = -(speed * 3);
            }
            else
            {
                speed = -speed;
            }
        }
    }
}
