using Cainos.PixelArtTopDown_Basic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class PlayerFight : MonoBehaviour
{
    List<string> VocalCommand;
    [SerializeField] ManagerVocal V;
    Dictionary<string, Action> actions = new Dictionary<string, Action>();
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

    public InGameClass Class;
    [HideInInspector] public int PV;
    public MonsterScript Monster;
    [HideInInspector] public InGameSkills SkillsUsed;
    public TextMeshPro PlayerFText;
    public TopDownCharacterController PM;
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
        if (Monster == null)
        {

        }
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
            if (Class.sprt)
                GetComponent<SpriteRenderer>().sprite = Class.sprt;
            if (Class.Ctrl)
                GetComponent<Animator>().runtimeAnimatorController = Class.Ctrl;
            V.enabled = true;
        }
        GoToSkill();
        if (Block)
            DamageBlock(BlockDamageCount, BlockDelayCount);
        if (Dash != 0)
            DamageDash(DashCount,DashDelayCount);
        if (Bonus != 0)
            DamageBonus(Bonus,BonusDelayCount);
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
        //Va selectionner la compétence et l'exécuter 
        Monster.SkillsUsed = Skills[id];
        id = Skills.Length + 1;
    }

    public void Skill1()
    {
        id = 0;
        StartCoroutine(AppearText(Skills[id].name));
    }
    public void Skill2()
    {
        id = 1;
        StartCoroutine(AppearText(Skills[id].name));
    }
    public void Skill3()
    {
        id = 2;
        StartCoroutine(AppearText(Skills[id].name));
    }

    public void DamageDealt(int Damage)
    {
        if (Dash != 0)
        {
            Dash --;
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
        if  (Delay < 0)
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
            PlayerFText.text = currenttext;
            yield return new WaitForSeconds(SubDelay);
        }
    }
}
