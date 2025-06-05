using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // �J�����͈͎̔擾.
    Vector2 cmin, cmax;
    [SerializeField] string tagName;
    [SerializeField] float speed;
    [SerializeField] float plusPos;

    GameObject target;
    GameManager gameManager;
    bool speedDownFlg;
    //�J�������ɓ������炩�ǂ����t���O�ŊǗ�.
    bool cmeraFlg;
    //�����������ǂ���.
    bool hitFlag;
    /// <summary>
    /// ����������,
    /// </summary>
    void Init()
    {
        //�J�����͈͎̔擾.
        cmin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        cmax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        //�ʒu�ύX.
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
        //�Ώۂ̃^�O���Q�Ƃ���.
        target = GameObject.FindWithTag(tagName);

        //�Q�[���}�l�[�W���[���擾.
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
    /// �ړ�����.
    /// </summary>
    void Move()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }
    /// <summary>
    /// angle����.
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
        if (Pos.x < cmin.x - 1 || Pos.y < cmin.y - 1 || Pos.x > cmax.x + 1 || Pos.y > cmax.y + 1)
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
        switch (Random.Range(0, 2))
        {
            case 0: Pos = new Vector3(cmin.x - plusPos, -4.4f, 0); break;  //��
            case 1: Pos = new Vector3(cmax.x + plusPos, -4.4f, 0); break;  //��
        }
        // �ʒu��Ԃ�.
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
