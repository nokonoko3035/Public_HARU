using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hototogisu : MonoBehaviour
{
    // �J�����͈͎̔擾.
    Vector2 cmin, cmax;

    [SerializeField] Vector3 tagetPos;
    [SerializeField] float speed;
    [SerializeField] float plusPos;
    [SerializeField] GameObject egg;
    Rigidbody2D rigid2d;

    GameManager gameManager;
    //�J�������ɓ������炩�ǂ����t���O�ŊǗ�.
    bool cmeraFlg;
    //�����������ǂ���.
    bool hitFlag;
    //�G�b�O�������Ă���.
    bool getEggFlg;
    //�����Ă���.
    bool Onflg;
    /// <summary>
    /// ����������,
    /// </summary>
    void Init()
    {        
        //�Q�[���}�l�[�W���[���擾.
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        rigid2d = GetComponent<Rigidbody2D>();
        //�J�����͈͎̔擾.
        cmin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        cmax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        //�ʒu�ύX.
        transform.position = Point();

        float dx, dy;
        //�^�[�Q�b�g�̕����Ɉړ�����.
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
        //�A���O��.
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
    /// ���̃I�u�W�F�N�g�̏�ԏ���.
    /// </summary>
    void Chack()
    {
        //�J�����O�ɂȂ�����.
        if (Camera_Chack() != 0 && cmeraFlg)
        {
            Destroy(gameObject);
        }
        //�J�������ɓ�������.
        if (Camera_Chack() == 0 && !cmeraFlg)
        {
            cmeraFlg = true;
        }
    }
    /// <summary>
    /// ��ʊO���ǂ����m�F����.
    /// ���� : 0 / -1  (�J������ / �J�����O).
    /// </summary>
    /// <returns></returns>
    int Camera_Chack()
    {
        //���� ���W���X�V.
        Vector2 Pos = transform.position;
        if (Pos.x < cmin.x - 1|| Pos.y < cmin.y - 1|| Pos.x > cmax.x + 1|| Pos.y > cmax.y + 1)
        {
            //�J�����O.
            return -1;
        }
        //�J������.
        return 0;
    }
    /// <summary>
    /// camera�͈͊O���烉���_���̍��W�l��Ԃ�.
    /// </summary>
    /// <returns></returns>
    Vector3 Point()
    {
        //�O�̂��߂̏�����.
        Vector3 Pos = Vector3.zero;
        //�����_���ɏo���ʒu��ύX.
        switch (Random.Range(0, 3))
        {
            case 0: Pos = new Vector3(cmin.x - plusPos, Random.Range(cmin.y + 1,  cmax.y + plusPos), 0); break;  //��
            case 1: Pos = new Vector3(cmax.x + plusPos, Random.Range(cmin.y + 1,  cmax.y + plusPos), 0); break;  //��
            case 2: Pos = new Vector3(Random.Range(cmin.x + 1 , cmax.x + plusPos), cmax.y + plusPos, 0); break;  //��
        }
        // �ʒu��Ԃ�.
        return Pos;
    }
    /// <summary>
    /// �����蔻��
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hitFlag)
        {
            if ((collision.gameObject.tag == "Nest" || collision.gameObject.tag == "Egg") && !Onflg)
            {
                hitFlag = true;
                //�^�[�Q�b�g�̕����Ɉړ�����.
                Vector3 Pos = Point() - transform.position ;
                Pos = Pos.normalized;
                float dx, dy;
                dx = Pos.x * (speed * 1.25f);
                dy = Pos.y * (speed * 1.25f);
                rigid2d.velocity = new Vector3(dx, dy, 0);

                //�A���O��.
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
                //���̏���.
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

            //�^�[�Q�b�g�̕����Ɉړ�����.
            Vector3 Pos = transform.position - tagetPos;
            Pos = Pos.normalized;
            float dx, dy;
            dx = Pos.x * (speed * 2);
            dy = Pos.y * (speed * 2);
            rigid2d.velocity = new Vector3(dx, dy, 0);

            //�A���O��.
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
