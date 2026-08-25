using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System.Linq;

public class VoiceCommandHandler : MonoBehaviour
{
    [Header("Reference")]
    public HintManager hintManager;

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    private void Start()
    {
        keywords.Add("give hint", OnHintCommand);
        keywords.Add("hint", OnHintCommand);
        keywords.Add("help", OnHintCommand);
        keywords.Add("clue", OnHintCommand);

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
        keywordRecognizer.Start();

        Debug.Log("[VoiceCommandHandler] Keyword recognizer started.");
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("[Voice] Recognised: " + args.text);
        if (keywords.ContainsKey(args.text))
            keywords[args.text].Invoke();
    }

    private void OnHintCommand()
    {
        Debug.Log("[Voice] Hint command triggered.");
        hintManager?.GiveHint();
    }

    private void OnDestroy()
    {
        if (keywordRecognizer != null && keywordRecognizer.IsRunning)
        {
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
        }
    }
}