using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hototogisu : MonoBehaviour
{
    // カメラの範囲取得.
    Vector2 cmin, cmax;

    [SerializeField] Vector3 tagetPos;
    [SerializeField] float speed;
    [SerializeField] float plusPos;
    [SerializeField] GameObject egg;
    Rigidbody2D rigid2d;

    GameManager gameManager;
    //カメラ内に入ったらかどうかフラグで管理.
    bool cmeraFlg;
    //当たったかどうか.
    bool hitFlag;
    //エッグを持っている.
    bool getEggFlg;
    //逃げている.
    bool Onflg;
    /// <summary>
    /// 初期化処理,
    /// </summary>
    void Init()
    {        
        //ゲームマネージャーを取得.
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        rigid2d = GetComponent<Rigidbody2D>();
        //カメラの範囲取得.
        cmin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        cmax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        //位置変更.
        transform.position = Point();

        float dx, dy;
        //ターゲットの方向に移動する.
        if (gameManager.stackCnt != 0)
        {
            Vector3 Pos = tagetPos - transform.position;
            Pos = Pos.normalized;
            
            dx = Pos.x * speed;
            dy = Pos.y * speed;
            rigid2d.velocity = new Vector3(dx, dy, 0);
        }
        else
        {
            GameObject gameObject = gameManager.EggCheck();
            if (gameObject != null)
            {
                Vector3 Pos = gameObject.transform.position - transform.position;
                Pos = Pos.normalized;
                dx = Pos.x * speed;
                dy = Pos.y * speed;
                rigid2d.velocity = new Vector3(dx, dy, 0);
            }
            else
            {
                Vector3 Pos = tagetPos - transform.position;
                Pos = Pos.normalized;
                dx = Pos.x * speed;
                dy = Pos.y * speed;
                rigid2d.velocity = new Vector3(dx, dy, 0);
            }
        }
        //アングル.
        if (dx > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
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
        Chack();
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
        if (Pos.x < cmin.x - 1|| Pos.y < cmin.y - 1|| Pos.x > cmax.x + 1|| Pos.y > cmax.y + 1)
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
        switch (Random.Range(0, 3))
        {
            case 0: Pos = new Vector3(cmin.x - plusPos, Random.Range(cmin.y + 1,  cmax.y + plusPos), 0); break;  //←
            case 1: Pos = new Vector3(cmax.x + plusPos, Random.Range(cmin.y + 1,  cmax.y + plusPos), 0); break;  //→
            case 2: Pos = new Vector3(Random.Range(cmin.x + 1 , cmax.x + plusPos), cmax.y + plusPos, 0); break;  //↑
        }
        // 位置を返す.
        return Pos;
    }
    /// <summary>
    /// 当たり判定
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hitFlag)
        {
            if ((collision.gameObject.tag == "Nest" || collision.gameObject.tag == "Egg") && !Onflg)
            {
                hitFlag = true;
                //ターゲットの方向に移動する.
                Vector3 Pos = Point() - transform.position ;
                Pos = Pos.normalized;
                float dx, dy;
                dx = Pos.x * (speed * 1.25f);
                dy = Pos.y * (speed * 1.25f);
                rigid2d.velocity = new Vector3(dx, dy, 0);

                //アングル.
                if (dx > 0)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                    egg.transform.position = new Vector3(-0.2f, -0.365f, 0f) + transform.position;
                    egg.transform.rotation = Quaternion.Euler(0, 180f, 120f);
                }
                else
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                    egg.transform.position = new Vector3(0.2f, -0.36f, 0f) + transform.position;
                    egg.transform.rotation = Quaternion.Euler(0, 180f, 60f);
                }
                //卵の処理.
                if(gameManager.stackCnt > 0 || collision.gameObject.tag == "Egg")
                {
                    egg.gameObject.SetActive(true);
                    getEggFlg = true;
                    if(!(collision.gameObject.tag == "Egg"))
                    {
                        gameManager.DownStack();
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                    }
                }
            }
        }

        if (collision.gameObject.tag == "Meow" && !Onflg)
        {
            Onflg = true;
            if (getEggFlg)
            {      
                egg.transform.parent = null;
                egg.GetComponent<BoxCollider2D>().enabled = true;
                egg.GetComponent<BoxCollider2D>().isTrigger = false;
                egg.GetComponent<Rigidbody2D>().gravityScale = 3;
                egg.GetComponent<Rigidbody2D>().simulated = true;
                egg.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            //ターゲットの方向に移動する.
            Vector3 Pos = transform.position - tagetPos;
            Pos = Pos.normalized;
            float dx, dy;
            dx = Pos.x * (speed * 2);
            dy = Pos.y * (speed * 2);
            rigid2d.velocity = new Vector3(dx, dy, 0);

            //アングル.
            if (dx > 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                egg.transform.position = new Vector3(-0.2f, -0.365f, 0f) + transform.position;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
                egg.transform.position = new Vector3(0.2f, -0.36f, 0f) + transform.position;
            }
        }
    }
}
