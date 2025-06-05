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
    /// ���ݗ������邩�ǂ���.
    /// </summary>
    public GameObject EggCheck()
    {
        GameObject egg = null;
        egg = GameObject.FindGameObjectWithTag("Egg");
        if(egg == null)
        {
            print("�Q�[���I��");
            GameOver();
        }
        return egg;
    }

    /// <summary>
    /// �摜�f�[�^���A�b�v�f�[�g.
    /// </summary>
    void ImgUpdate()
    {
        imgObj.sprite = imgs[stackCnt];
    }
    /// <summary>
    /// �c�@�����炷����.
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
    /// �c�@�𑝂₷����.
    /// </summary>
    public void UpStack() 
    {
        stackCnt++;
        ImgUpdate();
    }
    /// <summary>
    /// �Q�[���N���A���� ���̒��ɂ������邩, 0��������Q�[���I�[�o�[.
    /// </summary>
    public void GameClear()
    {
        //�c�@�ނ̕ۑ�.
        if (stackCnt == 0)
        {
            GameOver();
            return;
        }
        //�Q�[���N���A.
        PlayerPrefs.SetInt("EggCnt", stackCnt);
        //�V�[���؂�ւ�.
        SceneManager.LoadScene("ClearScene");
    }
    /// <summary>
    /// �Q�[���I�[�o�[.
    /// </summary>
    public void GameOver()
    {
        //�V�[���؂�ւ�.
        SceneManager.LoadScene("GameOverScene");
    }
}
