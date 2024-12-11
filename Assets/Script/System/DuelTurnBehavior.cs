using UnityEngine;

public class DuelTurnBehavior : MonoBehaviour
{
    [SerializeField] PlayerFight PF;
    MonsterScript MS;
    public bool AuthorizedTurn {  get; private set; }
    public int MonsterDamage {  get; private set; }
    public int PlayerDamage {  get; private set; }
    public int TurnFinish;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MS = PF.Monster;
        if (PF.SkillsUsed && MS.SkillsUsed) 
        {
            DoTurn();
        }
        if (TurnFinish == 2)
        {
            PF.SkillsUsed = null; MS.SkillsUsed = null;
        }
    }

    void DoTurn()
    {
        if (PF.SkillsUsed.ID == 0)
            MonsterDamage = PF.SkillsUsed.Damage;
        else
            MonsterDamage = 0;
        if (MS.SkillsUsed.ID == 0)
            PlayerDamage = MS.SkillsUsed.Damage;
        else
            PlayerDamage = 0;
        AuthorizedTurn = true;
    }
}
