using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObject/SkillSO")]
public class SkillSO : ScriptableObject
{
    public string prefabName;
    public float staminaCost;
    public float destroyTime;
    public float skillCD;//�N�o�ɶ�
    public float skillRate;//�g�����j
    public float skillForce = 15f;

    public bool CanShoot()
    {
        return skillCD <= 0;
    }
}