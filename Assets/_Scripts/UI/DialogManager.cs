using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [Header("Dialog Display")]
    [SerializeField] GameObject _dialogPanel;
    [SerializeField] TextMeshProUGUI _dialogText;
    [SerializeField] TextMeshProUGUI _speakerText;
    [SerializeField] GameObject _continueIcon;
    [SerializeField] float _typingSpeed = 0.04f;
    
    Story _currentStory;
    Coroutine _displayLineCoroutine;
    bool canContinueToNextLine = false;
    const string SPEAKER_TAG = "speaker";
    const string EVENT_TAG = "event";

    public bool DialogIsPlaying { get; private set; }
    public event Action RumbleEvent;

    public static DialogManager Instance;

    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start() => _dialogPanel.SetActive(false);

    void Update()
    {
        if (!DialogIsPlaying)
            return;

        if (Input.anyKeyDown && canContinueToNextLine)
            ContinueStory();
    }

    // Load the first line of the Story and display the dialog window
    public void EnterDialongMode(TextAsset inkJSON)
    {
        _currentStory = new Story(inkJSON.text);
        DialogIsPlaying = true;
        _dialogPanel.SetActive(true);

        ContinueStory();
    }

    // Reset the dialog window after dialog is finished
    IEnumerator ExitDialogMode()
    {
        yield return new WaitForSeconds(0.2f);
        DialogIsPlaying = false;
        _dialogPanel.SetActive(false);
        _dialogText.text = "";
    }

    // Load the next line of dialog from the Story if the story isn't completed
    void ContinueStory()
    {
        if (_currentStory.canContinue)
        {
            if (_displayLineCoroutine != null)
                StopCoroutine(_displayLineCoroutine);

            _displayLineCoroutine = StartCoroutine(DisplayLine(_currentStory.Continue()));
            HandleTags(_currentStory.currentTags);
        }
        else
            StartCoroutine(ExitDialogMode());
    }

    // Display the current line form the Story 1 letter at a time
    IEnumerator DisplayLine(string line)
    {
        _dialogText.text = line;
        _dialogText.maxVisibleCharacters = 0;
        canContinueToNextLine = false;
        _continueIcon.SetActive(false);
        int counter = 0;

        foreach (char letter in line.ToCharArray())
        {
            counter++;

            // Display entire line all at once if player presses a button to skip
            if (Input.anyKeyDown && counter > 5)
            {
                _dialogText.maxVisibleCharacters = line.Length;
                break;
            }

            // Display the next letter of the dialog line
            _dialogText.maxVisibleCharacters++;
            yield return new WaitForSeconds(_typingSpeed);
        }

        _continueIcon.SetActive(true);
        canContinueToNextLine = true;
    }

    // Read the tags written in the Story to set the speaker's name and trigger events in the dialog window
    void HandleTags(List<string> currentTags)
    {
        // Parse each tag included in the Story line
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2 )
                Debug.Log("Can't parse tag: " + tag);

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            // Process each tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    _speakerText.text = tagValue;
                    break;
                case EVENT_TAG:
                    RumbleEvent?.Invoke();
                    break;
                default:
                    Debug.Log("Tag not handled: " + tag);
                    break;
            }
        }
    }
}
