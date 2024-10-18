//using System.Diagnostics;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using HuggingFace.API;
using System;
//using System.Diagnostics;
//using static System.Net.Mime.MediaTypeNames;

public class VoiceRecHandler : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button stopButton;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI subtext;
    [SerializeField] private string matchPhrase;
    [SerializeField] private sceneTransition transitioner;
    private AudioClip clip;
    private byte[] bytes;
    private bool recording;
    private bool youGotIt;
    private bool success = false;
    private float waitTime;
    private float timer = 0f;
    //public GameObject starsImage;
    private void Start()
    {
        startButton.onClick.AddListener(StartRecording);
        stopButton.onClick.AddListener(StopRecording);
        //turns off player movement
        PlayerMovement.changeMoving();
        //youGotIt is a variable which, when set to trues will let the game know to transition scenes
        youGotIt = false;
        waitTime = 1.5f;

    }

    private void Update()
    {
        //Debug.Log(waitTime);
        //this is some recording stuff
        if (recording && Microphone.GetPosition(null) >= clip.samples)
        {
            StopRecording();
        }


        //rn space is just a cheat button- youGotIt is the real star
        if ((Input.GetKeyDown("space") && !success) || youGotIt)
        {
            youGotIt = false;
            //essentially takes the top scene of the stack (this one) off, thereby bringing us back to the original. additionally, turns on player moving
            PlayerMovement.changeMoving();
            //Check success, delete other elements
            success = true;
            text.text = "Goodnight " + matchPhrase + "!";

            //iterate goodnighter
            questHandler.currGoodnighter++;
        }

        if (success)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            Debug.Log("waitTime: " + waitTime);
            Debug.Log("currGoodnigher: " + questHandler.currGoodnighter);
            Debug.Log("length: " + (questHandler.goodnighters.Length - 1));
            if (timer > waitTime)
            {
                /*//if (after having been iterated), currGoodnighter isn't at the last spot...
                if (questHandler.currGoodnighter < questHandler.goodnighters.Length)
                {*/
                    //we transition to the next scene
                    transitioner.transitionScene(SceneManager.GetSceneAt(SceneManager.sceneCount - 1).name);
                /*}
                else
                {
                    //if it is already the last one, it should load in a new scene where the player goes to bed
                }*/
            }
        }


    }

    private void StartRecording()
    {
        UnityEngine.Debug.Log("testing");
        UnityEngine.Debug.Log("Mics: " + Microphone.devices);
        text.color = Color.white;
        text.text = "Recording...";
        subtext.text = "";
        clip = Microphone.Start(null, false, 10, 44100);
        recording = true;
    }

    private void StopRecording()
    {
        UnityEngine.Debug.Log("Recording finished!");
        var position = Microphone.GetPosition(null);
        Microphone.End(null);
        var samples = new float[position * clip.channels];
        clip.GetData(samples, 0);
        bytes = EncodeAsWAV(samples, clip.frequency, clip.channels);
        recording = false;
        //File.WriteAllBytes(Application.dataPath + "/test.wav", bytes);
        SendRecording();

    }

    private void SendRecording()
    {
        HuggingFaceAPI.AutomaticSpeechRecognition(bytes, response => {
            text.color = Color.white;
            string responseCheck = response.ToLower();
            text.text = responseCheck;
            if ((responseCheck.Contains("goodnight") || responseCheck.Contains("good night")) && responseCheck.Contains(matchPhrase))
            {
                youGotIt = true;
                text.text = "Goodnight Bunny";
            } else
            {
                text.text = "Can you repeat that?";
                subtext.text = "gramma heard " + responseCheck;
                Debug.Log("Can you repeat that?");
            }
        }, error => {
            text.color = Color.red;
            text.text = error;
        });
    }

    private byte[] EncodeAsWAV(float[] samples, int frequency, int channels)
    {
        using (var memoryStream = new MemoryStream(44 + samples.Length * 2))
        {
            using (var writer = new BinaryWriter(memoryStream))
            {
                writer.Write("RIFF".ToCharArray());
                writer.Write(36 + samples.Length * 2);
                writer.Write("WAVE".ToCharArray());
                writer.Write("fmt ".ToCharArray());
                writer.Write(16);
                writer.Write((ushort)1);
                writer.Write((ushort)channels);
                writer.Write(frequency);
                writer.Write(frequency * channels * 2);
                writer.Write((ushort)(channels * 2));
                writer.Write((ushort)16);
                writer.Write("data".ToCharArray());
                writer.Write(samples.Length * 2);

                foreach (var sample in samples)
                {
                    writer.Write((short)(sample * short.MaxValue));
                }
            }
            return memoryStream.ToArray();
        }
    }

}
