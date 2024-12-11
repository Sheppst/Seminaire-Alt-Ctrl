using Cainos.PixelArtTopDown_Basic;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{
    List<string> VocalCommand;
    [SerializeField] ManagerVocal V;
    [SerializeField] DuelTurnBehavior DTB;
    Dictionary<string, Action> actions = new Dictionary<string, Action>();
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

    public InGameClass Class;
    public int PV {  get; private set; }
    public MonsterScript Monster;
    [HideInInspector] public InGameSkills SkillsUsed;
    public TextMeshPro PlayerFText;
    TopDownCharacterController PM;
    PlayerManager PMA;
    private void Start()
    {
        PM = GetComponent<TopDownCharacterController>();
        PMA = GetComponent<PlayerManager>();
        if (!Class)
            Name = "";
        Skills = new InGameSkills[1];
        Skills[0] = null;
        
    }

    private void Update()
    {
        AT = DTB.AuthorizedTurn;
        DamageEncounter = DTB.PlayerDamage;
        SubDelay = StaticFloat.instance.DelaySubtext;
        if (Class && Name != Class.ClassName) 
        {
            Name = Class.ClassName;
            Skills = new InGameSkills[Class.ClasseSkills.Length];
            for (int i = 0; i < Skills.Length; i++) 
            {
                Skills[i] = Class.ClasseSkills[i];
            }
            PV = Class.PV;
            V.enabled = true;
        }
        if (!SkillsUsed && id <= Skills.Length - 1)
            GoToSkill();
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
        }
        

        ActionOn();
        /*GoToSkill();
        if (Block)
            DamageBlock(BlockDamageCount, BlockDelayCount);
        if (Dash != 0)
            DamageDash(DashCount,DashDelayCount);
        if (Bonus != 0)
            DamageBonus(Bonus,BonusDelayCount);
        if (Heal)
            DamageHealt(HealtDamageCount, HealDelayCount);

        ActionOn();*/
    }

    void GoToSkill()
    {
        if (!(Skills[id].ID == 0))
        {
            SkillsUsed = Skills[id];
        }
        id = Skills.Length + 1;

        /*if (id > Skills.Length - 1)
        {
            SkillsUsed = null;
            return;
        }
        else if (Skills[id].ID == 0)
        {
            Monster.SkillsUsed = Skills[id];
        }
        else
        {
            SkillsUsed = Skills[id];
        }
        id = Skills.Length + 1;*/

        //Va selectionner la compétence et l'exécuter 
    }

    public void Skill1()
    {
        id = 0;
        StartCoroutine(AppearText(Skills[id].name));
        print(Skills[0].name);
    }
    public void Skill2()
    {
        id = 1;
        StartCoroutine(AppearText(Skills[id].name));
        print(Skills[1].name);
    }
    public void Skill3()
    {
        id = 2;
        StartCoroutine(AppearText(Skills[id].name));
        print(Skills[2].name);
    }
    public void DamageDealt(int Damage)
    {
        if (Damage == 0)
            return;
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

        PV -= Damage;
    }

    public void DamageBlock(int Damage, float Turn = 0)
    {
        Block = true;
        if (Turn >= 0)
        {
            if (Damage == 0)
                BlockD = 50000 * PV;
            else
                BlockD = Damage;
            BlockDelayCount--;
        }
        else if (Turn < 0)
        {
            Block = false;
        }
    }

    public void DamageBonus(int Damage, float Turn = 0)
    {
        Bonus = Damage;
        if (Turn < 0)
        {
            Bonus = 0;
        }
        BonusDelayCount--;
    }

    public void DamageHealt(int Damage, float Turn = 0)
    {
        Heal = true;
        if (Turn > 0)
        {
            PV += Damage;
            HealDelayCount--;
        }
        if (Turn < 0)
        {
            Heal = false;
        }
    }
    public void DamageDash(int Number, float Turn = 0)
    {
        Dash = Number;
        if (Turn < 0)
        {
            Dash = 0;
        }
        DashDelayCount--;
    }
/*    public void DamageDealt(int Damage)
    {
        if (Dash != 0)
        {
            Dash --;
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

        PV -= Damage;
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
                BlockD = 50000 * PV;
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
        if  (Delay < 0)
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
            PV += Damage;
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
        /*if (!SkillsUsed)
            return;
        else if (SkillsUsed.ID == 0)
            DamageDealt(SkillsUsed.Damage);*/
        if (SkillsUsed.ID == 1)
            DamageBlock(SkillsUsed.Damage, SkillsUsed.delay);
        else if (SkillsUsed.ID == 2)
            DamageBonus(SkillsUsed.Damage, SkillsUsed.delay);
        else if (SkillsUsed.ID == 3)
            DamageHealt(SkillsUsed.Damage, SkillsUsed.delay);
        else if (SkillsUsed.ID == 4)
            DamageDash(SkillsUsed.Damage, SkillsUsed.delay);
    }
    public IEnumerator AppearText(string fulltext)
    {
        string currenttext = "";
        for (int i = 0; i < fulltext.Length; i++)
        {
            currenttext = fulltext.Substring(0, i + 1);
            PlayerFText.text = currenttext;
            yield return new WaitForSeconds(SubDelay);
        }
    }
}
