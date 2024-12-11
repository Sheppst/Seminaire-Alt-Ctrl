using UnityEngine;

[CreateAssetMenu (fileName = "NewClasse" , menuName = "Scriptable Objects/Classes")]
public class InGameClass : ScriptableObject
{
    [Header("UI")]
    public string Description;
    [Header("Data")]
    public int id;
    public int PV;
    public string ClassName;
    public string SpecialAccess;
    public InGameSkills[] ClasseSkills;
}
