using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class DetectionVocal : MonoBehaviour
{
    #region VARIABLE

    public int microphone;

    public bool DetectVocal;
    public string keyWord;

    private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
    private KeywordRecognizer keywordRecognizer;

    public List<string> listCallFonction;

    public Blob blob;

    #endregion



    #region MES FONCTIONS
    public void OnKeyWordRecognized(PhraseRecognizedEventArgs args)
    {
        print(args.text);
        if (keywordActions.ContainsKey(args.text))
        {
            keywordActions[args.text].Invoke();
        }
    }

    public event Action fonction1, fonction2, fonction3, fonction4, fonction5, fonction6, fonction7, fonction8, fonction9, fonction10;
    private void Fonction1()
    {
        print("fonction 1");

        if(fonction1 != null)
        {
            fonction1();
        }
    }

    private void Fonction2()
    {
        print("fonction 2");

        if (fonction2 != null)
        {
            fonction2();
        }
    }

    private void Fonction3()
    {
        print("fonction 3");

        if (fonction3 != null)
        {
            fonction3();
        }
    }

    private void Fonction4()
    {
        print("fonction 4");

        if (fonction4 != null)
        {
            fonction4();
        }
    }

    private void Fonction5()
    {
        print("fonction 5");

        if (fonction5 != null)
        {
            fonction5();
        }
    }

    private void Fonction6()
    {
        print("fonction 6");

        if (fonction6 != null)
        {
            fonction6();
        }
    }

    private void Fonction7()
    {
        print("fonction 7");

        if (fonction7 != null)
        {
            fonction7();
        }
    }
    private void Fonction8()
    {
        print("fonction 8");

        if (fonction8 != null)
        {
            fonction8();
        }
    }

    private void Fonction9()
    {
        print("fonction 9");

        if (fonction9 != null)
        {
            fonction9();
        }
    }

    private void Fonction10()
    {
        print("fonction 10");

        if (fonction10 != null)
        {
            fonction10();
        }
    }

    #endregion

    public void DetectMicrophone()
    {
        string microphoneName = Microphone.devices[microphone];
        print(Microphone.devices[microphone]);

        //Microphone.Start()

    }


    #region FONCTION BASES
    void Start()
    {
        //keywordActions.Add(keyWord + listCallFonction[0], Fonction1);
        keywordActions.Add(keyWord + listCallFonction[1], Fonction1);
        keywordActions.Add(keyWord + listCallFonction[2], Fonction2);
        keywordActions.Add(keyWord + listCallFonction[3], Fonction3);
        keywordActions.Add(keyWord + listCallFonction[4], Fonction4);
        keywordActions.Add(keyWord + listCallFonction[5], Fonction5);
        keywordActions.Add(keyWord + listCallFonction[6], Fonction6);
        keywordActions.Add(keyWord + listCallFonction[7], Fonction7);
        keywordActions.Add(keyWord + listCallFonction[8], Fonction8);
        keywordActions.Add(keyWord + listCallFonction[9], Fonction9);
        keywordActions.Add(keyWord + listCallFonction[10], Fonction10);


        keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnKeyWordRecognized;

        if (DetectVocal == true)
        {
            print("arrive ici");
            keywordRecognizer.Start();

            DetectMicrophone();
        }
    }

    #endregion




}
