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

    //�X�^�[�g���������J�n���邽�߂̃t���O.
    public bool stratFlg;
    /// <summary>
    /// Csv�Ȃǂ̓ǂݍ��ݏ����ł�.
    /// </summary>
    void Lood()
    {
        // CSV�ǂݍ���.
        lines = csvData.text.Split('\n');

        gameManager = GetComponent<GameManager>();
    }
    /// <summary>
    /// ���ԏ���.
    /// </summary>
    void Times()
    {
        Time01 += Time.deltaTime;
        Time02 += Time.deltaTime;
        Leveltime += Time.deltaTime;
    }
    /// <summary>
    /// ���x���A�b�v����.
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
                //�N���A.
                gameManager.GameClear();
            }
            Leveltime = 0;
            CtUpdate(0);
        }
    }
    /// <summary>
    /// �N�[���^�C����������������. 0: �S�� 1:��Ԗ� 2:��Ԗ�
    /// </summary>
    void CtUpdate(int data)
    {
        // ���s��?
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
        //error�΍�.
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
    /// ���C���ƂȂ�o�������B
    /// </summary>
    void MeinEnter()
    {
        // ���s��?
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
    /// �o������.
    /// </summary>
    /// <param name="obj"></param>
    void EnterObj(GameObject obj)
    {
       GameObject data = Instantiate(obj);
       //�e�q�֌W.
       data.transform.parent = transform;
    }
    /// <summary>
    /// �Ώۂ̖��O�𑗂邱�ƂŔz����̃Q�[���I�u�W�F�N�g���������邱�Ƃ��ł��܂��B
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
    /// ����������.
    /// </summary>
    void Init()
    {
        // �X�^�[�g�}�l�[�W���[���Ȃ��Ȃ�擾����.
        if (startManager == null)
        {
            startManager = GetComponent<StartManager>();
        }
        // ����ł��Ȃ��Ȃ�Q�[�����J�n����.
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
