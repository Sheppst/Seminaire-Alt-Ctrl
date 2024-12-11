using TMPro;
using System.Collections;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    bool Heal;
    bool Block;
    bool AT;
    int id;
    int DamageEncounter;
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
    MonsterManager MM;

    [SerializeField] PlayerFight Player;
    [SerializeField] ManagerVocal V;
    [SerializeField] DuelTurnBehavior DTB;

    public int MPV {  get; private set; }
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
        MM = GetComponent<MonsterManager>();
        MM.enabled = false;
        Name = Monst.BaseName;
        Skills = new InGameSkills[Monst.Skills.Length];
        for (int i = 0; i < Skills.Length; i++)
        {
            Skills[i] = Monst.Skills[i];
        }
        MPV = Monst.PV;
    }

    // Update is called once per frame
    void Update()
    {
        AT = DTB.AuthorizedTurn;
        DamageEncounter = DTB.MonsterDamage;
        if (Monst && MPV <= 0)
        {
            Destroy(gameObject);
        }
        SubDelay = StaticFloat.instance.DelaySubtext;
        if (Monst && Name != Monst.BaseName )
        {
            Name = Monst.BaseName;
            Skills = new InGameSkills[Monst.Skills.Length];
            for (int i = 0; i < Skills.Length; i++)
            {
                Skills[i] = Monst.Skills[i];
            }
            MPV = Monst.PV;
        }

        if(SkillsUsed == null)
            MM.enabled = true;

        if (AT)
        {
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
        
    }
    public void GoToSkill()
    {
        SkillsUsed = Skills[id];
        /*if (SkillsUsed && SkillsUsed.ID == 0)
        {
            return;
        }
        else if (id > Skills.Length - 1)
        {
            SkillsUsed = null;
            return;
        }
        else if (Skills[id].ID == 0)
        {
            Player.SkillsUsed = Skills[id];
            id = Skills.Length + 1;
        }
        else
        {
            SkillsUsed = Skills[id];
        }*/
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
        if (Damage == 0)
            return;
        if (Dash != 0)
        {
            Dash--;
            if (SkillsUsed && SkillsUsed.ID == 0)
                SkillsUsed = null;
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

        MPV -= Damage;
        if (SkillsUsed && SkillsUsed.ID == 0)
            SkillsUsed = null;
    }

    public void DamageBlock(int Damage, float Turn = 0)
    {
        Block = true;
        if (Turn >= 0)
        {
            //BlockDelayCount = Turn - Time.deltaTime;
            if (Damage == 0)
                BlockD = 50000 * MPV;
            else
                BlockD = Damage;
        }
        else if (Turn < 0)
        {
            Block = false;
        }
        if (SkillsUsed && SkillsUsed.ID == 1)
            SkillsUsed = null;
    }

    public void DamageBonus(int Damage, float Turn = 0)
    {
        Bonus = Damage;
        if (Turn < 0)
        {
            Bonus = 0;
        }
        BonusDelayCount = Turn - Time.deltaTime;
        if (SkillsUsed && SkillsUsed.ID == 2)
            SkillsUsed = null;
    }

    public void DamageHealt(int Damage, float Turn = 0)
    {
        Heal = true;
        if (Turn > 0)
        {
            MPV += Damage;
            HealDelayCount = Turn - Time.deltaTime;
        }
        if (Turn < 0)
        {
            Heal = false;
        }
        if (SkillsUsed && SkillsUsed.ID == 3)
            SkillsUsed = null;
    }
    public void DamageDash(int Number, float Turn = 0)
    {
        Dash = Number;
        if (Turn < 0)
        {
            Dash = 0;
        }
        DashDelayCount = Turn - Time.deltaTime;
        if (SkillsUsed && SkillsUsed.ID == 4)
            SkillsUsed = null;
    }
    /*public void DamageDealt(int Damage)
    {
        if (Dash != 0)
        {
            Dash--;
            if (SkillsUsed && SkillsUsed.ID == 0)
                SkillsUsed = null;
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

        MPV -= Damage;
        if (SkillsUsed && SkillsUsed.ID == 0)
            SkillsUsed = null;
    }

    public void DamageBlock(int Damage, float Delay = 0)
    {
        Block = true;
        if (Delay >= 0)
        {
            BlockDelayCount = Delay - Time.deltaTime;
            if (Damage == 0)
                BlockD = 50000 * MPV;
            else
                BlockD = Damage;
        }
        else if (Delay < 0)
        {
            Block = false;
        }
        if (SkillsUsed && SkillsUsed.ID == 1)
            SkillsUsed = null;
    }

    public void DamageBonus(int Damage, float Delay = 0)
    {
        Bonus = Damage;
        if (Delay < 0)
        {
            Bonus = 0;
        }
        BonusDelayCount = Delay - Time.deltaTime;
        if (SkillsUsed && SkillsUsed.ID == 2)
            SkillsUsed = null;
    }

    public void DamageHealt(int Damage, float Delay = 0)
    {
        Heal = true;
        if (Delay > 0)
        {
            MPV += Damage;
            HealDelayCount = Delay - Time.deltaTime;
        }
        if (Delay < 0)
        {
            Heal = false;
        }
        if (SkillsUsed && SkillsUsed.ID == 3)
            SkillsUsed = null;
    }
    public void DamageDash(int Number, float Delay = 0)
    {
        Dash = Number;
        if (Delay < 0)
        {
            Dash = 0;
        }
        DashDelayCount = Delay - Time.deltaTime;
        if (SkillsUsed && SkillsUsed.ID == 4)
            SkillsUsed = null;
    }*/
    void ActionOn()
    {
        DamageDealt(DamageEncounter);
        /*if (!SkillsUsed )
            return;
        if (SkillsUsed.ID == 0)
            DamageDealt(SkillsUsed.Damage);*/
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
