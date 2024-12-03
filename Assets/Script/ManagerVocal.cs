using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows.Speech;

public class ManagerVocal : MonoBehaviour
{
    //Le KeywordRecognizer �coute et reconnais des mots cl�e pr� enregistrer dans la liste "actions"
    public KeywordRecognizer keywordRecognizer;
    public Dictionary<string,Action> actions = new Dictionary<string, Action>();

    public List<string> listStringActions;
    private float delay = 0.02f;

    [Header("UI")]
    public TextMeshProUGUI text;
    public string a;


    private void Awake()
    {
        actions.Add(listStringActions[0], Hello);
        actions.Add(listStringActions[1], HowAreU);
        actions.Add(listStringActions[2], WhoAreU);
        actions.Add(listStringActions[3], WhatAreWeDoing);


        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    /// <summary>
    /// Fonction appel� quand mot cl�e enregistrer rep�r�
    /// </summary>
    /// <param name="speech"></param>
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void Hello()
    {
        StartCoroutine(AppearText("Bonjour !" + a));
    }

    private void HowAreU()
    {
        StartCoroutine(AppearText("Super ! Et vous comment allez vous ? :) "));
    }

    private void WhoAreU()
    {
        StartCoroutine(AppearText("A proprement parl� je suis un syst�me de d�tection vocal, pour faire simple, je d�tecte des mots cl�es d�j� renseigner, et j'apelle une fonction"));

    }

    private void WhatAreWeDoing()
    {
        StartCoroutine(AppearText("C'est tr�s simple ! Aujourd'hui nous allons commencer le s�minaire sur le th�me Alternative Controller ! Comme vous vous en doutez nous allons nous servir de la detection vocal comme controller de notre jeu !"));
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
            currenttext = fulltext.Substring(0, i + 1);
            text.text = currenttext;
            yield return new WaitForSeconds(delay);
        }
    }

}
