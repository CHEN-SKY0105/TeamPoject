using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Events;

public class SkillSelector : MonoBehaviour
{
    private int previousIndex;
    private PlayerControl playerControl;
    private GetHitEffect getHitEffect;
    private Animator animator;
    [field: SerializeField, ReadOnly] public int CurrentIndex { get; private set; }
    public UnityEvent<int, int> ChangeSelectSkill = new UnityEvent<int, int>();
    [SerializeField] private SkillSO[] skillSOArray;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        getHitEffect = GetComponentInChildren<GetHitEffect>();
        playerControl = GetComponentInChildren<PlayerControl>();
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
        if (playerControl.isAttack || getHitEffect.playerHealth < 0)
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
            if (skillSOArray[CurrentIndex].CanShoot())
            {
                //�������@�w�n�o�ˤ~��Ū����
                //playerControl.isAttack = true;
                animator.SetBool("Magic_Prepare", true);
            }
            return;
        }
        if (Input.GetMouseButtonUp(2))
        {
            if (animator.GetBool("Magic_Prepare"))
            {
                animator.SetBool("Magic_Prepare", false);
            }
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