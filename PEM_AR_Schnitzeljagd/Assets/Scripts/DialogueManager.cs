using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private readonly List<(string speaker, string text)> nextTextSegments = new List<(string speaker, string text)>();
    private Dialogue nextDialogue;

    public Text speakerTextComponent;
    public Text speakerNameComponent;
    public GameObject speakerAvatar;
    public Sprite Eddy;
    public Sprite Danny;
    public Sprite Chester;

    public GameObject dialogueGameObject;
    public Button continueButton;
    public UnityEvent dialogueSceneEndedEvent;
    public List<DialogueSceneId> dialogueSceneIds = new List<DialogueSceneId>();


    private readonly string testText = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. " +
        "Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. " +
        "At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut " +
        "labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.    Duis autem vel eum iriure dolor in hendrerit in " +
        "vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero eros et accumsan et iusto odio dignissim qui blandit praesent luptatum zzril delenit augue duis dolore te feugait nulla facilisi.Lorem ipsum dolor sit amet, " +
        "consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. " +
        "Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat.Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat " +
        "nulla facilisis at vero eros et accumsan et iusto odio dignissim qui blandit praesent luptatum zzril delenit augue duis dolore te feugait nulla facilisi. " +
        "Nam liber tempor cum soluta nobis eleifend option congue nihil imperdiet doming id quod mazim placerat facer possim assum.Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat." +
        "Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse" +
        " molestie consequat, vel illum dolore eu feugiat nulla facilisis. At vero eos et accusam et justo duo dolores et ea rebum.Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur " +
        "sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor " +
        "sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, At accusam aliquyam diam diam dolore dolores duo eirmod eos erat, et nonumy sed tempor et et invidunt justo labore Stet clita ea et gubergren, kasd magna no rebum. sanctus sea sed " +
        "takimata ut vero voluptua.est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur";
    private readonly string testSpeaker = "Eddy";

    private readonly Dictionary<DialogueSceneId, List<Dialogue>> dialogueScenes = new Dictionary<DialogueSceneId, List<Dialogue>>();

    public DialogueSceneId CurrentDialogueSceneId { get; private set; }

    public List<Dialogue> CurrentScenesDialogues { get; private set; }

    public bool Intialized { get; private set; }

    Dictionary<string, Sprite> SpeakerAvatars = new Dictionary<string, Sprite>();


    // Start is called before the first frame update
    void Start()
    {
        if (dialogueSceneEndedEvent == null)
            dialogueSceneEndedEvent = new UnityEvent();

        dialogueSceneEndedEvent.AddListener(OnDialogueEnded);
        continueButton.onClick.AddListener(Continue);

        SpeakerAvatars.Add("Eddy", Eddy);
        SpeakerAvatars.Add("Danny", Danny);
        SpeakerAvatars.Add("Chester", Chester);

        SetDialogueState(false);

        InitDialogueScenes();

        Intialized = true;
    }

    public void StartDialogueScene(DialogueSceneId dialogueSceneId)
    {
        CurrentDialogueSceneId = dialogueSceneId;
        CurrentScenesDialogues = dialogueScenes[dialogueSceneId];

        StartFirstDialogue();
    }

    public void Continue()
    {
        Debug.Log("Continue");

        var nextTextSegment = nextTextSegments.FirstOrDefault();
        if (nextTextSegment != default((string, string)))
        {
            Debug.Log("Show NextTextSegment: nextTextSegment = " + nextTextSegment);
            nextTextSegments.Remove(nextTextSegment);
            AddAndDisplayDialogue(nextTextSegment.speaker, nextTextSegment.text);
        }
        else
        {
            Debug.Log("Text Ended");
            if (nextDialogue != null)
            {
                StartFirstDialogue();
            }
            else
            {
                Debug.Log("Dialogue Scene Ended");
                SetDialogueState(false);
                dialogueSceneEndedEvent.Invoke();
            }
        }
    }

    private void StartFirstDialogue()
    {
        var firstDialogue = CurrentScenesDialogues.FirstOrDefault();

        Debug.Log("StartFirstDialogue: speaker = " + firstDialogue.Speaker + " text = " + firstDialogue.Text);

        CurrentScenesDialogues.Remove(firstDialogue);
        nextDialogue = CurrentScenesDialogues.FirstOrDefault();

        AddAndDisplayDialogue(firstDialogue.Speaker, firstDialogue.Text);
    }

    private void AddAndDisplayDialogue(string speaker, string text)
    {
        try
        {
            SetDialogueState(true);

            Debug.Log("AddAndDisplayDialogue: speaker = " + speaker + " text = " + text);
            speakerNameComponent.text = speaker;
            speakerTextComponent.text = text;

            Debug.Log("We are here");

            var avatarComponent = speakerAvatar.GetComponentInChildren<Image>();
            avatarComponent.sprite = null;

            if (SpeakerAvatars.TryGetValue(speaker, out var newSpeakerAvatar))
            {
                speakerAvatar.SetActive(true);
                avatarComponent.sprite = newSpeakerAvatar;
            }
            else
            {
                speakerAvatar.SetActive(false);
            }

            Canvas.ForceUpdateCanvases();
            int truncateIndex = speakerTextComponent.cachedTextGenerator.characterCountVisible;
            var nextText = text.Substring(truncateIndex);
            Debug.Log("nextText=" + nextText);

            if (nextText != "" && speaker != "")
                nextTextSegments.Add((speaker, nextText));
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }


    private void SetDialogueState(bool enabled)
    {
        dialogueGameObject.SetActive(enabled);
    }

    private void OnDialogueEnded()
    {
        Debug.Log("OnDialogueEnded");
    }

    private void InitDialogueScenes()
    {
        CreateDialogueScene(DialogueSceneId.PresentSearchPortal, DialogueScene.PresentSearchPortalDialogueScene);
        CreateDialogueScene(DialogueSceneId.Fire, DialogueScene.FireDialogueScene);
        CreateDialogueScene(DialogueSceneId.AfterFire, DialogueScene.AfterFireDialogueScene);
        CreateDialogueScene(DialogueSceneId.RebuildTower, DialogueScene.RebuildTowerDialogueScene);
        CreateDialogueScene(DialogueSceneId.AfterRebuildTower, DialogueScene.AfterRebuildTowerDialogueScene);
    }

    private void CreateDialogueScene(DialogueSceneId dialogueSceneId, List<Dialogue> dialogues)
    {
        dialogueSceneIds.Add(dialogueSceneId);
        dialogueScenes.Add(dialogueSceneId, dialogues);
    }

}


