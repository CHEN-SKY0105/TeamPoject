using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Events;

public class SkillSelector : MonoBehaviour
{
    private int previousIndex;
    private PlayerControl playerControl;
    private Animator animator;
    private PlayerStats playerStats;
    [field: SerializeField, ReadOnly] public int CurrentIndex { get; private set; }
    public UnityEvent<int, int> ChangeSelectSkill = new UnityEvent<int, int>();
    [SerializeField] private SkillSO[] skillSOArray;
    private SkillShooter skillShooter;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerControl = GetComponentInChildren<PlayerControl>();
        playerStats = GetComponent<PlayerStats>();
        skillShooter = GetComponent<SkillShooter>();
    }

    private void Start()
    {
        //�����m���ܭ��s�}�l�C���|�û�����I�k
        for (int i = 0; i < skillSOArray.Length; i++)
        {
            skillSOArray[i].skillCD = 0;
        }

        ChangeSelectSkill?.Invoke(0, 0);
    }

    private void Update()
    {
        if (playerControl.isAttack || playerStats.IsDead())
        {
            return;
        }

        if (GameStateManager.Inst.IsMobile)
        {
            return;
        }

        ScrollSelect();
        KeyboardTriggerSkill();
        MiddleMouseButtonTriggerSkill();
    }

    private void MiddleMouseButtonTriggerSkill()
    {
        if (Input.GetMouseButtonDown(2))
        {
            SkillPrepare(CurrentIndex);
            return;
        }

        if (Input.GetMouseButtonUp(2))
        {
            SkillShoot();
        }
    }

    private void KeyboardTriggerSkill()
    {
        int inputKey = -1;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inputKey = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inputKey = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            inputKey = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            inputKey = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            inputKey = 4;
        }
        if (inputKey == -1)
        {
            return;
        }
        CurrentIndex = inputKey;

        ChangeSelectSkill?.Invoke(CurrentIndex, previousIndex);
        previousIndex = CurrentIndex;
        //�Q����L�I�k ������n�˷� �ҥH���U�h�N�����o��
        if (skillSOArray[inputKey].CanShoot())
        {
            animator.SetTrigger("Magic");
        }
    }

    public bool IsSkillCanShoot(int index)
    {
        return skillSOArray[index].CanShoot();
    }

    public void SkillPrepare(int index)
    {
        if (!skillSOArray[index].CanShoot())
        {
            return;
        }
        CurrentIndex = index;
        skillShooter.ShowAimMesh(CurrentIndex);

        //�������@�w�n�o�ˤ~��Ū����
        //playerControl.isAttack = true;
        animator.SetBool("Magic_Prepare", true);
    }

    public void SkillShoot()
    {
        if (!animator.GetBool("Magic_Prepare"))
        {
            return;
        }
        animator.SetBool("Magic_Prepare", false);
    }

    private void ScrollSelect()
    {
        float scrollSelect = Input.GetAxis("Mouse ScrollWheel");
        if (scrollSelect != 0)
        {
            if (scrollSelect > 0.0f)
            {
                CurrentIndex--;
            }
            else if (scrollSelect < 0.0f)
            {
                CurrentIndex++;
            }

            CurrentIndex = Math.Clamp(CurrentIndex, 0, skillSOArray.Length - 1);

            if (previousIndex != CurrentIndex)
            {
                ChangeSelectSkill?.Invoke(CurrentIndex, previousIndex);
                previousIndex = CurrentIndex;
            }
        }
        return;
    }
}