using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public DialogueManager dialogueManager;

    public YearBehaviourScript yearBehaviour;

    public PortalManager portalManager;

    public FireExtinguishManager fireExtinguishManager;

    public TowerRebuildController rebuildController;

    public GameStage CurrentGame { get; private set; }

    public InventoryManager inventoryManager;
    public GameItem binocularsToAdd;

    public GameObject EndScreen;

    // Start is called before the first frame update
    void Start()
    {
        // Debug
        //StartCoroutine(WaitForDialogueManager());
    }

    IEnumerator WaitForDialogueManager()
    {
        yield return new WaitUntil(() => dialogueManager.Intialized);
        StartFirstScene();
    }

    public void StartFirstScene()
    {
        yearBehaviour.ChangeYear(SceneYear.Present);
        portalManager.SetPortalSetting(true);
        portalManager.SetTowerState(true);
        portalManager.SetPortalState(false);
        dialogueManager.StartDialogueScene(DialogueSceneId.PresentSearchPortal);
    }

    public void StartGameStage(GameStage gameType)
    {
        CurrentGame = gameType;
        Debug.Log("StartGameStage: CurrentGame = " + CurrentGame);
        switch (gameType)
        {
            case GameStage.FindPortalOne:
                portalManager.ActivatePortalAndTower();
                break;
            case GameStage.FireExtinguish:
                break;
            case GameStage.FindPortalTwo:
                portalManager.SetPortalSetting(true);
                portalManager.SetPortalState(true);
                portalManager.SetTowerState(false);
                break;
            case GameStage.RebuildTower:
                break;
            default:
                break;
        }
    }

    public void OnGameStageEnded()
    {
        Debug.Log("OnGameStageEnded: CurrentGame = " + CurrentGame);
        switch (CurrentGame)
        {
            case GameStage.FindPortalOne:
                yearBehaviour.ChangeYear(SceneYear.FirstYear);
                dialogueManager.StartDialogueScene(DialogueSceneId.Fire);
                portalManager.SetTowerState(false);
                fireExtinguishManager.BeginInteraction();
                break;
            case GameStage.FireExtinguish:
                yearBehaviour.ChangeYear(SceneYear.FirstYear);
                dialogueManager.StartDialogueScene(DialogueSceneId.AfterFire);
                break;
            case GameStage.FindPortalTwo:
                yearBehaviour.ChangeYear(SceneYear.SecondYear);
                fireExtinguishManager.HideFireInteraction();
                dialogueManager.StartDialogueScene(DialogueSceneId.RebuildTower);
                portalManager.SetTowerState(false);
                rebuildController.BeginRebuildInteraction();
                break;
            case GameStage.RebuildTower:
                yearBehaviour.ChangeYear(SceneYear.SecondYear);
                dialogueManager.StartDialogueScene(DialogueSceneId.AfterRebuildTower);
                break;
            default:
                break;
        }

        CurrentGame = GameStage.None;
    }

    public void OnDialogueSceneEnded()
    {
        switch (dialogueManager.CurrentDialogueSceneId)
        {
            case DialogueSceneId.PresentSearchPortal:
                StartGameStage(GameStage.FindPortalOne);
                break;
            case DialogueSceneId.Fire:
                StartGameStage(GameStage.FireExtinguish);
                break;
            case DialogueSceneId.AfterFire:
                inventoryManager?.AddItem(binocularsToAdd);
                StartGameStage(GameStage.FindPortalTwo);
                break;
            case DialogueSceneId.RebuildTower:
                StartGameStage(GameStage.RebuildTower);
                break;
            case DialogueSceneId.AfterRebuildTower:
                rebuildController.StopRebuildInteraction();
                EndScreen?.SetActive(true);
                break;
            default:
                break;
        }
    }
}

public class SceneYear
{
    public const string Present = "Gegenwart";

    public const string FirstYear = "1944";

    public const string SecondYear = "1952";
}

public enum GameStage
{
    None,
    FindPortalOne,
    FireExtinguish,
    FindPortalTwo,
    RebuildTower
}
