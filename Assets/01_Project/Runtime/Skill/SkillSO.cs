using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObject/SkillSO")]
public class SkillSO : ScriptableObject
{
    public string prefabName;
    public float staminaCost;
    public float destroyTime;
    public float skillCD;//�N�o�ɶ�
    public float skillRate;//�g�����j
    public float skillForce = 15f;
    public UnityEvent<float, float> CoolDownChange = new UnityEvent<float, float>();

    public bool CanShoot()
    {
        return skillCD <= 0;
    }

    public IEnumerator StartCoolDown()
    {
        skillCD = skillRate;
        while (true)
        {
            //���n�N yield return null ����P�_pause���U �|�L���j��
            yield return null;
            if (GameStateManager.Inst.CurrentState == GameState.Pausing)
            {
                continue;
            }
            skillCD -= Time.deltaTime;
            CoolDownChange?.Invoke(skillCD, skillRate);
            if (skillCD < 0)
            {
                break;
            }
        }
    }
}