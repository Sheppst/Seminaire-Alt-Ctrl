using Cainos.PixelArtTopDown_Basic;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public KeywordRecognizer keywordRecognizer;
    [SerializeField] PlayerFight P;
    [SerializeField] TopDownCharacterController PM;
    PlayerManager PMA;
    public List<string> listStringActions;
    public InGameClass[] Class;
    [HideInInspector] public Dictionary<string, int> actions = new Dictionary<string, int>();
    public TextMeshProUGUI Text;
    public TextMeshPro PText;
    private float delay = 0.02f;
    bool isPrinting;
    bool locked;
    private void Awake()
    {
        P.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        PM.enabled = false;
        P.enabled = false;
        for (int i = 0; i < listStringActions.Count; i++)
            actions.Add(listStringActions[i], i);
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    private void Start()
    {
        PMA = PM.gameObject.GetComponent<PlayerManager>();
    }

    private void Update()
    {
        if (string.IsNullOrEmpty(PText.text) || isPrinting)
        {
            locked = false;
            StopAllCoroutines();
        }
        else if (!locked)
        {
            locked = true;
            StartCoroutine(Fade());
        }
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        ChooseClasse(actions[speech.text]);
        //print(1);
    }

    private void ChooseClasse(int id)
    {
        P.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        P.Class = Class[id];
        PM.enabled = true;
        PMA.enabled = true;
        keywordRecognizer.Stop();
        enabled = false;
    }

    public IEnumerator AppearText(string fulltext)
    {
        string currenttext = "";
        isPrinting = true;
        for (int i = 0; i < fulltext.Length; i++)
        {
            currenttext = fulltext.Substring(0, i + 1);
            Text.text = currenttext;
            yield return new WaitForSeconds(delay);
        }
        isPrinting = false;
    }

    private IEnumerator Fade()
    {
        if (string.IsNullOrEmpty(PText.text) || isPrinting) 
        {
            yield return null;
        }
        else if (PText.alpha != 0)
        {
            PText.alpha--;
            yield return Fade();
        }
    }
}
