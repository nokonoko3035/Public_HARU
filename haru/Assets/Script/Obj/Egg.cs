using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    bool getflg;
    bool grandFlg;

    GameManager gameManager;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        //�Q�[���}�l�[�W���[���擾.
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //�v���C���[�̃X�N���v�g���擾.
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// �v���C���[�Ƒ��̓����蔻��. Trigger : true
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.tag == "Player") && !getflg && !player.getStoneFlg && grandFlg)
        {
            getflg = true;
            if (player != null)
            {
                player.getEggFlg = true;
            }
            transform.parent = collision.gameObject.transform;
            transform.position = new Vector3(0, -0.4f,0) + collision.transform.position;
            //25�b��ɏ���.
            Invoke("Destroy_Player", 25);
        }
        if( collision.gameObject.tag == "Nest")
        {
            if (player != null)
            {
                player.getEggFlg = false;
            }
            gameManager.UpStack();
            Destroy(gameObject);
        }
        if( collision.gameObject.tag == "Stone")
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// �n��Ƒ��̓����蔻�菈��. Trigger : false
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Untagged") && !getflg)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            grandFlg = true;
        }
        if (collision.gameObject.tag == "Nest")
        {
            gameManager.UpStack();
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// �I�u�W�F�N�g���폜����.
    /// </summary>
    void Destroy_Player()
    {
        if(player != null)
        {
            player.getEggFlg = false;
        }
        Destroy(gameObject);
    }
}
