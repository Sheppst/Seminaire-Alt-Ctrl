using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    InGameMonster M;
    MonsterScript MS;
    [SerializeField] float AttackTime;
    float Timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        MS = GetComponent<MonsterScript>();
        M = MS.Monst;
        
    }
    private void OnEnable()
    {
        RandomSkills();
        MS.GoToSkill();
        enabled = false;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomSkills() 
    {
        int id = Random.Range(0, M.Skills.Length);
        if (id == 0)
            MS.Skill1();
        if (id == 1)
            MS.Skill2();
        if (id == 2)
            MS.Skill3();
    }
}
