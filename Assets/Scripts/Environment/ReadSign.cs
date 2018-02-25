using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadSign : MonoBehaviour {

    private Image buttonImage;
    private TextMesh displayText;
    [TextArea]
    public string signText = string.Empty;

    public SpriteRenderer image = null;

    private Text objectives;
    public string task = string.Empty;

    private Text announcer;
    private AudioSource announcerAudio;

    private bool isPlayerNearSign = false;

    public AudioSource audio = null;
    private bool isPlayed = false;

    private void Start()
    {
        objectives = GameObject.FindGameObjectWithTag("HUD_Objectives").GetComponent<Text>();
        announcer = GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<Text>(); //Helper.GetObject<Text>("Announcer");

        buttonImage = GameObject.FindGameObjectWithTag("Player_Buttons").GetComponent<Image>(); //Helper.GetObject<Image>("Player Button Image");
        displayText = GameObject.FindGameObjectWithTag("Player_Answer").GetComponent<TextMesh>(); //Helper.GetObject<TextMesh>("Player's answer");

        announcerAudio = GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<AudioSource>(); //Helper.GetObject<AudioSource>("Announcer");

        if (task != string.Empty && objectives.text.Contains(task))
        {
            image.enabled = false;
        }
    }

    private void Update()
    {
        if (isPlayerNearSign)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerAnswer();
                StartCoroutine(Task());
            }
        }
    }

    private void PlayerAnswer()
    {
        if (displayText != null)
        {
            signText = signText.Replace("<br>", "\n");
            displayText.text = signText;
        }
    }

    private IEnumerator Task()
    {
        if (image != null)
        {
            if (image.enabled)
            {
                image.enabled = false;

                if (objectives != null)
                {
                    objectives.text += task + "\n";

                    yield return DisplayTaskAnnouncer();
                }
            }
        }
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && audio != null && !isPlayed)
        {
            audio.Play();
            isPlayed = true;
        }

        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            yield return ShowInteractionButton();
        }
    }

    private IEnumerator OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            yield return HideInteraction();
        }
    }

    private IEnumerator DisplayTaskAnnouncer()
    {
        if (announcer != null)
        {
            announcer.text = "New task added";

            if (announcerAudio != null)
            {
                var path = "Sound/bookFlip1";
                var sound = Resources.Load<AudioClip>(path);

                if (sound != null)
                {
                    Debug.Log(path);
                    announcerAudio.clip = sound;
                    announcerAudio.Play();
                }
            }

            yield return new WaitForSeconds(5);

            announcer.text = string.Empty;
        }
    }

    private IEnumerator ShowInteractionButton()
    {
        if (buttonImage != null)
        {
            var path = "Button/button_E";
            var image = Resources.Load<Sprite>(path);

            buttonImage.sprite = image;
            buttonImage.enabled = true;
            isPlayerNearSign = true;

            yield return null;
        }
    }

    private IEnumerator HideInteraction()
    {
        if (buttonImage != null)
        {
            buttonImage.enabled = false;
        }

        if (displayText != null)
        {
            displayText.text = string.Empty;
        }

        isPlayerNearSign = false;

        yield return null;
    }
}
