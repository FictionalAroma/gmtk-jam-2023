using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CaptainTextController : MonoBehaviour
{
    AudioSource captainAudioSource;
    public AudioClip[] dialogueClips;
    //public List<string> textWords;
    public string[] dialogues;
    public TextMeshProUGUI dialogueTextDisplay;
    public float typingDelay;
    public bool playText;
    public int audioFrequency= 2;
    [Range(1f,2f)]
    public float minPitch;
    [Range(1f, 2f)]
    public float maxPitch;
    private void Start()
    {
        captainAudioSource = GetComponent<AudioSource>();
    }
    public IEnumerator DisplayText(string text)
    {
        dialogueTextDisplay.text = text;
        dialogueTextDisplay.maxVisibleCharacters = 0;
        foreach (char c in text)
        {
            PlayAudioClips(dialogueTextDisplay.maxVisibleCharacters);
            dialogueTextDisplay.maxVisibleCharacters++;
            yield return new WaitForSeconds(typingDelay);
        }
        /*textWords = new List<string>();
        foreach (string s in splittedStringArray)
        {
            textWords.Add(s);
        }*/
        
    }
    private void Update()
    {
        if (playText)
        {
            StartCoroutine(DisplayText(dialogues[0]));
            playText = false;
        }
    }
    public void PlayAudioClips(int currentDisplayedCharacterCount)
    {
        if (currentDisplayedCharacterCount % audioFrequency == 0)
        {
            AudioClip randomClip = dialogueClips[UnityEngine.Random.Range(0, dialogueClips.Length)];
            captainAudioSource.clip = randomClip;
            captainAudioSource.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
            captainAudioSource.Play();
        }
    }
    /*public void StartDialogue()
    {
        CreateText(dialogues[0]);
        PlayDialogue(textWords.Count);
    }*/


    /*public void PlayDialogue(int sentenceLength)
    {
        StartCoroutine(PlayAudioClipsCoroutine(sentenceLength));
    }

    private IEnumerator PlayAudioClipsCoroutine(int sentenceLength)
    {
        for (int sentencePlayed = 0; sentencePlayed < sentenceLength; sentencePlayed++)
        {
            AudioClip randomClip = dialogueClips[UnityEngine.Random.Range(0, dialogueClips.Length)];
            captainAudioSource.clip = randomClip;
            captainAudioSource.Play();

            // Wait until the audio clip finishes playing
            yield return new WaitForSeconds(randomClip.length);
        }
    }*/

}
