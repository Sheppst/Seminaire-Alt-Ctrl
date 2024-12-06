using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMonster", menuName = "Scriptable Objects/Monsters")]
public class InGameMonster : ScriptableObject
{
    [Header("Data")]
    public int id;
    public int PV;
    public string BaseName;
    public InGameSkills[] Skills;
    [Header("UI")]
    public AnimatorController Ctrl;
    public Sprite sprt;
    public string Description;
}
