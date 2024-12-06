using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Scriptable Objects/Skills")]
public class InGameSkills : ScriptableObject
{
    [Header("Data")]
    [Tooltip ("0 = Damage Dealt, 1 = Damage Block, 2 = Damage Bonus, 3 = Damage Healt, 4 = Damage Dodge")]
    public int ID;
    [Tooltip ("Valeur entière renseignant la quantité de dégâts ingligé, soigné ou bloqué en fonction du type de la compétence")]
    public int Damage;
    public float delay;
    public string ActivationSentence;
    public string Special;
    [Header("UI")]
    public Sprite sprt;
    public string Description;
}
