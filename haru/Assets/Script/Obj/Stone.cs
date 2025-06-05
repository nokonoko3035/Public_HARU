using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    Player player;
    [SerializeField] AudioClip clip1;
    [SerializeField] AudioClip clip2;

    new AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //石を落とす.
        if ((Input.GetButtonUp("Fire2") || Input.GetKeyUp(KeyCode.Space)) && transform.parent != null)
        {
            if (player.getStoneFlg)
            {
                audio.PlayOneShot(clip2);
                transform.parent = null;
                gameObject.tag = "Stone";
                player.getStoneFlg = false;
                GetComponent<BoxCollider2D>().isTrigger = false;
                GetComponent<Rigidbody2D>().gravityScale = 2;
            }
        }
    } 
    /// <summary>
    /// Trigger : true
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        //タグがプレイヤーで.
        if (collision.gameObject.tag == "Player")
        {
            // コントローラーの Bボタンか スペースキーが押されていて なおかつ タグがストーン以外 かつ 卵持っていないか.
            if ((Input.GetButton("Fire2") || Input.GetKey(KeyCode.Space)) && tag != "Stone" && !player.getEggFlg)
            {
                if (!player.getStoneFlg)
                {
                    audio.PlayOneShot(clip1);
                    player.getStoneFlg = true;
                    transform.parent = collision.gameObject.transform;
                    transform.position = new Vector3(0, -0.55f, 0) + collision.transform.position;
                }
            }
        }
    }
    /// <summary>
    /// Trigger : true
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //タグがプレイヤーで.
        if (collision.gameObject.tag == "Player")
        {
            // コントローラーの Bボタンか スペースキーが押されていて なおかつ タグがストーン以外 かつ 卵持っていないか.
            if ((Input.GetButton("Fire2") || Input.GetKey(KeyCode.Space))&& tag != "Stone" && !player.getEggFlg)
            {
                if (!player.getStoneFlg)
                {              
                    audio.PlayOneShot(clip1);
                    player.getStoneFlg = true;
                    transform.parent = collision.gameObject.transform;
                    transform.position = new Vector3(0, -0.55f, 0) + collision.transform.position;
                }
            }
        }
    }
    /// <summary>
    /// Trigger : false
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //地上についたら処理を行う.
        if ((collision.gameObject.tag == "Untagged"))
        {
            audio.Stop();
            gameObject.tag = "Untagged";
            GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }
    /// <summary>
    /// Trigger : false
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay2D(Collision2D collision)
    {
        //地上についたら処理を行う.
        if ((collision.gameObject.tag == "Untagged"))
        {
            audio.Stop();
            gameObject.tag = "Untagged";
            GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

}
