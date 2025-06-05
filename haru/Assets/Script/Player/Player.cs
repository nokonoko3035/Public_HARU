using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 cmin, cmax;
    [SerializeField] GameObject meow;
    [SerializeField] float width;
    [SerializeField] float height;

    [SerializeField] SpriteRenderer imgObj;
    [SerializeField] Sprite L;
    [SerializeField] Sprite R;
    float xspeed, yspeed;
    float speed = 5;

    public bool getStoneFlg;
    public bool getEggFlg;

    Animator animator;

    public bool playFlg;
    //bool IsSound = false;
    // Start is called before the first frame update
    void Start()
    {
        //�J�����͈͎̔擾.
        cmin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        cmax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame]
    void Update()
    {
        if (!playFlg)
        {
            return;
        }

        InputPad();

        Move();

        
        //A�{�^���������ꂽ�Ƃ���Sound���Đ����łȂ��Ƃ���Sound���Đ�
        if ((Input.GetKeyUp(KeyCode.M)||Input.GetButtonDown("Meow")) && !GetComponent<AudioSource>().isPlaying )
        {
            //�������Đ�
            GetComponent<AudioSource>().Play();
        }

        //�����o�Ă���Ԃ��������蔻���ON�ɂ���
        if(GetComponent<AudioSource>().isPlaying)
        { 
            meow.SetActive(true);
        }
        else
        {
            meow.SetActive(false);
        }
    }
    /// <summary>
    /// �p�b�h����.
    /// </summary>
    void InputPad()
    {

        float x = Input.GetAxisRaw("Horizontal");

        //���E���]������������ς���
        if(x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            imgObj.sprite = R;
        }
        if(x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            imgObj.sprite = L;
        }
        float y = Input.GetAxisRaw("Vertical");

        // ������.
        if (transform.position.x < cmax.x + (width / 2) && x > 0)
        {
            xspeed = x * speed;
        }
        if (transform.position.x > cmin.x - (width / 2) && x < 0)
        {
            xspeed = x * speed;
        }

        // �c����.
        if (transform.position.y < cmax.y - (height / 2) && y > 0)
        {
            yspeed = y * speed;
        }
        if (transform.position.y > cmin.y + 0.45f + (height / 2) && y < 0)
        {
            yspeed = y * speed;

        }
        else if(transform.position.y < cmin.y + 0.45f + (height / 2))
        {
            transform.position = new Vector3(transform.position.x, cmin.y + 0.45f + (height / 2), transform.position.z);
        }
    }

    /// <summary>
    /// �ړ�����
    /// </summary>
    void Move()
    {
        //�ړ�����
        if (!getStoneFlg)
        {
            transform.position += new Vector3(xspeed, yspeed, 0) * Time.deltaTime;
            animator.speed = 1;
        }
        else
        {
            transform.position += new Vector3(xspeed * 0.5f, (yspeed * 0.5f) - 0.2f, 0) * Time.deltaTime;
            animator.speed = 1.5f;
        }

        xspeed = 0;
        yspeed = 0;
    }
}

