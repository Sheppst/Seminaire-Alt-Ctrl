using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Threading;
using static Unity.Collections.AllocatorManager;

public class MonsterScript : MonoBehaviour
{
    bool Heal;
    bool Block;
    int id;
    int Dash;
    int BlockD;
    int Bonus;
    int BlockDamageCount;
    int DashCount;
    int HealtDamageCount;
    float SubDelay;
    float BlockDelayCount;
    float DashDelayCount;
    float HealDelayCount;
    float BonusDelayCount;
    string Name;
    InGameSkills[] Skills;

    [SerializeField] PlayerFight Player;
    [SerializeField] ManagerVocal V;

    [HideInInspector] public int PV;
    [HideInInspector] public InGameSkills SkillsUsed;
    public InGameMonster Monst;
    public TextMeshProUGUI MText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        Skills = new InGameSkills[1];

        ManagerVocal M = GameObject.Find("ManagerVocal").GetComponent<ManagerVocal>();
        M.MonstScript = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SubDelay = StaticFloat.instance.DelaySubtext;
        if (Monst && Name != Monst.BaseName )
        {
            Name = Monst.BaseName;
            Skills = new InGameSkills[Monst.Skills.Length];
            for (int i = 0; i < Skills.Length; i++)
            {
                Skills[i] = Monst.Skills[i];
            }
            PV = Monst.PV;
            if (Monst.sprt)
                GetComponent<SpriteRenderer>().sprite = Monst.sprt;
            if (Monst.Ctrl)
                GetComponent<Animator>().runtimeAnimatorController = Monst.Ctrl;
        }
        GoToSkill();
        if (Block)
            DamageBlock(BlockDamageCount, BlockDelayCount);
        if (Dash != 0)
            DamageDash(DashCount, DashDelayCount);
        if (Bonus != 0)
            DamageBonus(Bonus, BonusDelayCount);
        if (Heal)
            DamageHealt(HealtDamageCount, HealDelayCount);
        ActionOn();
    }
    void GoToSkill()
    {
        if (id > Skills.Length - 1)
        {
            SkillsUsed = null;
            return;
        }
        Player.SkillsUsed = Skills[id];
        //Va selectionner la compétence et l'exécuter 
        id = Skills.Length + 1;
    }
    public void Skill1()
    {
        id = 0;
    }
    public void Skill2()
    {
        id = 1;
    }
    public void Skill3()
    {
        id = 2;
    }
    public void DamageDealt(int Damage)
    {
        if (Dash != 0)
        {
            Dash--;
            return;
        }
        if (BlockD != 0)
        {
            Damage -= BlockD;
            Mathf.Clamp(Damage, 0, Mathf.Infinity);
        }
        if (Bonus != 0)
        {
            Damage += Bonus;
        }

        PV -= Damage;
    }

    public void DamageBlock(int Damage, float Delay = 0)
    {
        Block = true;
        if (Delay >= 0)
        {
            BlockDelayCount = Delay - Time.deltaTime;
            if (Damage == 0)
                BlockD = 50000 * PV;
            else
                BlockD = Damage;
        }
        else if (Delay < 0)
        {
            Block = false;
        }
    }

    public void DamageBonus(int Damage, float Delay = 0)
    {
        Bonus = Damage;
        if (Delay < 0)
        {
            Bonus = 0;
        }
        BonusDelayCount = Delay - Time.deltaTime;
    }

    public void DamageHealt(int Damage, float Delay = 0)
    {
        Heal = true;
        if (Delay > 0)
        {
            PV += Damage;
            HealDelayCount = Delay - Time.deltaTime;
        }
        if (Delay < 0)
        {
            Heal = false;
        }
    }
    public void DamageDash(int Number, float Delay = 0)
    {
        Dash = Number;
        if (Delay < 0)
        {
            Dash = 0;
        }
        DashDelayCount = Delay - Time.deltaTime;
    }
    void ActionOn()
    {
        if (!SkillsUsed)
            return;
        if (SkillsUsed.ID == 0)
            DamageDealt(SkillsUsed.Damage);
        if (SkillsUsed.ID == 1)
            DamageBlock(SkillsUsed.Damage, SkillsUsed.delay);
        if (SkillsUsed.ID == 2)
            DamageBonus(SkillsUsed.Damage, SkillsUsed.delay);
        if (SkillsUsed.ID == 3)
            DamageHealt(SkillsUsed.Damage, SkillsUsed.delay);
        if (SkillsUsed.ID == 4)
            DamageDash(SkillsUsed.Damage, SkillsUsed.delay);
    }
    public IEnumerator AppearText(string fulltext)
    {
        string currenttext = "";
        for (int i = 0; i < fulltext.Length; i++)
        {
            currenttext = fulltext.Substring(0, i + 1);
            MText.text = currenttext;
            yield return new WaitForSeconds(SubDelay);
        }
    }

    private void OnDestroy()
    {
        V.DestroyedLoad = this;
    }
}
