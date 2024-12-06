using NUnit.Framework.Internal;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.Windows.Speech;

public class ManagerVocal : MonoBehaviour
{
    //Le KeywordRecognizer écoute et reconnais des mots clée pré enregistrer dans la liste "actions"
    [SerializeField] GameObject Player;
    //[SerializeField] GameObject Monster;
    [HideInInspector]public MonsterScript MonstScript;
    [HideInInspector]public MonsterScript DestroyedLoad;
    [HideInInspector]public KeywordRecognizer keywordRecognizer;
    [HideInInspector]public Dictionary<string,Action> PlayerActions = new Dictionary<string, Action>();
    [HideInInspector]public Dictionary<string,Action> MonsterActions = new Dictionary<string, Action>();
    [HideInInspector]public Dictionary<string,Action> actions = new Dictionary<string, Action>();

    public List<string> listStringActions;
    private float delay = 0.02f; // délaie pour l'apparition progressif des mots

    int test;
    bool locked;

    [Header("UI")]
    public TextMeshProUGUI Text;

    float currentDelay;

    private void Awake()
    {
        //actions.Add(listStringActions[0], Hello);
        //actions.Add(listStringActions[1], HowAreU);
        //actions.Add(listStringActions[2], WhoAreU);
        //actions.Add(listStringActions[3], WhatAreWeDoing);
        //
        //keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        //keywordRecognizer.OnPhraseRecognized += DebugRecognizedSpeech;
        //keywordRecognizer.Start();
        print("a");
    }

    private void OnEnable()
    {
        PlayerFight PF = Player.GetComponent<PlayerFight>();
        InGameClass C = PF.Class;

        int i = 0;
        //actions.Add(listStringActions[0], Hello);
        //actions.Add(listStringActions[1], HowAreU);
        //actions.Add(listStringActions[2], WhoAreU);
        //actions.Add(listStringActions[3], WhatAreWeDoing);
        //
        //keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        //keywordRecognizer.OnPhraseRecognized += DebugRecognizedSpeech;
        PlayerActions.Add(i < C.ClasseSkills.Length ? C.ClasseSkills[i++].ActivationSentence : UnityEngine.Random.Range(0, 1000000000).ToString(), PF.Skill1);
        PlayerActions.Add(i < C.ClasseSkills.Length ? C.ClasseSkills[i++].ActivationSentence : UnityEngine.Random.Range(0, 1000000000).ToString(), PF.Skill2);
        PlayerActions.Add(i < C.ClasseSkills.Length ? C.ClasseSkills[i++].ActivationSentence : UnityEngine.Random.Range(0, 1000000000).ToString(), PF.Skill3);
        keywordRecognizer = new KeywordRecognizer(PlayerActions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }


    private void Update()
    {
        PlayerFight PF = Player.GetComponent<PlayerFight>();
        InGameClass C = PF.Class;
        /*if (MonstScript)
        {
            MonsterActions.Clear();
            MonsterActions.Add(MonstScript.Monst.Skills[0].ActivationSentence, MonstScript.Skill1);
            MonsterActions.Add(MonstScript.Monst.Skills[1].ActivationSentence, MonstScript.Skill2);
            MonsterActions.Add(MonstScript.Monst.Skills[2].ActivationSentence, MonstScript.Skill3);

            keywordRecognizer = new KeywordRecognizer(MonsterActions.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized += MRecognizedSpeech;

            MonstScript = null;
        }*/
        /*if (C && !locked)
        {
            int i = 0;
            actions.Add(listStringActions[0], Hello);
            actions.Add(listStringActions[1], HowAreU);
            actions.Add(listStringActions[2], WhoAreU);
            actions.Add(listStringActions[3], WhatAreWeDoing);

            keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized += DebugRecognizedSpeech;
            PlayerActions.Add(i < C.ClasseSkills.Length ? C.ClasseSkills[i++].ActivationSentence : UnityEngine.Random.Range(0, 1000000000).ToString(), Test *//*PF.Skill1*//*);
            PlayerActions.Add(i < C.ClasseSkills.Length ? C.ClasseSkills[i++].ActivationSentence : UnityEngine.Random.Range(0, 1000000000).ToString(), PF.Skill2);
            PlayerActions.Add(i < C.ClasseSkills.Length ? C.ClasseSkills[i++].ActivationSentence : UnityEngine.Random.Range(0, 1000000000).ToString(), PF.Skill3);
            keywordRecognizer = new KeywordRecognizer(PlayerActions.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
            keywordRecognizer.Start();
            locked = true;
        }*/
        print(keywordRecognizer.IsRunning);
        /*if (DestroyedLoad)
        {
            MonsterActions.Clear();
            MonsterActions.Add(DestroyedLoad.Monst.Skills[0].ActivationSentence, MonstScript.Skill1);
            MonsterActions.Add(DestroyedLoad.Monst.Skills[1].ActivationSentence, MonstScript.Skill2);
            MonsterActions.Add(DestroyedLoad.Monst.Skills[2].ActivationSentence, MonstScript.Skill3);

            keywordRecognizer = new KeywordRecognizer(MonsterActions.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized -= MRecognizedSpeech;
        }*/
    }

    /// <summary>
    /// Fonction appelé quand mot clée enregistrer repéré
    /// </summary>
    /// <param name="speech"></param>
    /// 

    private void DebugRecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text]?.Invoke();
        print(1);
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        PlayerActions[speech.text]?.Invoke();
        print(1);
    }

    //private void MRecognizedSpeech(PhraseRecognizedEventArgs speech)
    //{
    //    Debug.Log(speech.text);
    //    MonsterActions[speech.text]?.Invoke();
    //    print(1);
    //}

    private void Test()
    {
        print(2);
    }

    private void Hello()
    {
        StartCoroutine(AppearText("Bonjour !"));
    }
    
    private void HowAreU()
    {
        StartCoroutine(AppearText("Super ! Et vous comment allez vous ? :) "));
    }
    
    private void WhoAreU()
    {
        StartCoroutine(AppearText("A proprement parlé je suis un système de détection vocal, pour faire simple, je détecte des mots clées déjà renseigner, et j'apelle une fonction"));
        
    }
    
    private void WhatAreWeDoing()
    {
        StartCoroutine(AppearText("C'est très simple ! Aujourd'hui nous allons commencer le séminaire sur le thème Alternative Controller ! Comme vous vous en doutez nous allons nous servir de la detection vocal comme controller de notre jeu !"));
    }

    /// <summary>
    /// Fonction qui affiche le texte, lettre par lettre
    /// </summary>
    /// <param name="fulltext"></param>
    /// <returns></returns>
    public IEnumerator AppearText(string fulltext)
    {
        string currenttext = "";
        for (int i = 0; i < fulltext.Length; i++)
        {
            currenttext = fulltext.Substring(0,i+1);
            Text.text = currenttext;
            yield return new WaitForSeconds(delay);
        }
    }
    private void OnDisable()
    {
        keywordRecognizer.OnPhraseRecognized -= RecognizedSpeech;
        keywordRecognizer.Stop();
    }
}
