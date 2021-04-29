using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueScene : List<Dialogue>
{
    public static DialogueScene PresentSearchPortalDialogueScene
    {
        get
        {
            return new DialogueScene()
            {
                new Dialogue("Eddy", "Aaaah. der chinesische Turm. 1792 wurde er eröffnet hab ich gehört. "),
                new Dialogue("Eddy", "Ziemlich altes Gebäude. Dass es solange dem Zahn der Zeit getrotzt hat ist beindruckend."),
                new Dialogue("Eddy", "Mein Bruder am Monopteros hatte erwähnt, dass wir hier was Gutes zu essen finden werden. Da bin ich mal gespa..."),
                new Dialogue("Eddy", "Warte mal ich glaube hier ist eine weitere Zeit Spur. Ich glaube wir können hier jemanden aus meiner Familie finden."),
                new Dialogue("Eddy", "Komm! Schauen wir uns mal um.")
            };
        }
    }

    public static DialogueScene FireDialogueScene
    {
        get
        {
            return new DialogueScene()
            {
                new Dialogue("Eddy", "Feuer! Feuer! Feuer!!!"),
                new Dialogue("Eddy", "Was ist hier passiert? Wieso steht der Turm in Flammen!?"),
                new Dialogue("Eddy", "Ach du meine Güte ist das..."),
                new Dialogue("Eddy", "IST DAS DANNY DA OBEN!!!"),
                new Dialogue("Eddy", "Schnell wir müssen ihm helfen. Die Feuerwehr ist auch schon da. Schnapp dir einen Kübel. Wir müssen den Turm löschen!!!")
            };
        }
    }

    public static DialogueScene AfterFireDialogueScene
    {
        get
        {
            return new DialogueScene()
            {
                new Dialogue("Eddy", "Danny!!! So ein Glück. Dir ist nichts passiert. Was ist passiert wieso stand der Turm in Flammen."),
                new Dialogue("Danny", "Ich hab keine Ahnung. Ich wollte nur die Aussicht mit meinem neuen Fernglas genießen aber in der Gegenwart darf man dass ja nicht."),
                new Dialogue("Danny", "Ich hab nachgelesen, dass der Turm irgendwann in den den 1970er Jahren aus Sicherheitsgründen geschlossen wurde und dachte mir ich könnte raufklettern wenn ich in eine Zeit davor reise."),
                new Dialogue("Danny", "Wo ich oben war sind Flugzeuge über mich geflogen und haben Bomben abgeworfen und der Turm hat angefangen zu brennen."),
                new Dialogue("Eddy", "Warte mal welches Jahr haben wir. 1944! Danny du bist in die Zeit vom zweiten Weltkrieg gereist!!!"),
                new Dialogue("Danny", "Oh. OOOOOOHH!!!"),
                new Dialogue("Danny", "Wie konnte ich das nur vergessen. Ich kann euch nicht genug danken dass ihr mich gerettet habt."),
                new Dialogue("Danny", "Hier nimm mein Fernglas. Es bedeutet mir sehr viel, aber das ist das mindeste was ich dir geben sollte."),
                new Dialogue("", "*Danny gibt Fernglas.*"),
                new Dialogue("Eddy", "Die Hauptsache ist dass es dir gut geht. Komm Zeit, dass wir zurück in die Gegenwart gehen..."),
                new Dialogue("Eddy", "Moment mal hier ist schon wieder ein Portal erschienen. Komm vielleicht müssen wir noch jemandem aus meiner Familie helfen.")
            };
        }
    }

    public static DialogueScene RebuildTowerDialogueScene
    {
        get
        {
            return new DialogueScene()
            {
                new Dialogue("Eddy", "Der Turm ist weg! Naja, nachdem was hier 1944 passiert ist kein Wunder."),
                new Dialogue("Eddy", "Wobei. So wie es aussieht wird er grade neu aufgebaut. Oh, da ist mein Vater. Hallo Chester!"),
                new Dialogue("Chester", "Ah. Hallo ihr beiden. Sieht so aus als hättet ihr mich gefunden. Wie siehts aus? Helft ihr uns den Turm aufzubauen."),
                new Dialogue("Eddy", "Ähm wieso genau hilfst du dabei den Turm wiederaufzubauen?"),
                new Dialogue("Chester", "Ich hatte irgendwie mal wieder Lust auf ein Heimwerkerprojekt und wenn ich schonmal in der Nähe vom Chinesischen Turm bin dachte ich mir wieso nicht hier mithelfen."),
                new Dialogue("Chester", "Spart jedenfalls Materialkosten, auch wenn mir das Resultat nicht gehört."),
                new Dialogue("Eddy", "Und was ist für uns dabei drin."),
                new Dialogue("Chester", "Spaß?"),
                new Dialogue("Eddy", "..."),
                new Dialogue("Chester", "Okay, okay. Helft mir und ich gebe euch einen Hinweis, wo sich das nächste Familienmitglied versteckt."),
                new Dialogue("Chester", "Bringe einfach die Holzbalken, Dachziegel und die Turmspitze zu der Baustelle sobald sie benötigt werden."),
                // Holz, dachziegel und die Turmspitze 
            };
        }
    }

    public static DialogueScene AfterRebuildTowerDialogueScene
    {
        get
        {
            return new DialogueScene()
            {
                new Dialogue("Chester", "Ahhh gute Arbeit. Schön, dass man sich dafür entschieden hat den Turm wiederaufzubauen."),
                new Dialogue("Eddy", "Ja. Ja. Ja. Der Hinweis jetzt bitte."),
                new Dialogue("Chester", "Schon gut."),
                new Dialogue("Chester", "Folgt einfach dem Fluss Richtung Norden, dann solltet ihr jemanden finden."),
                new Dialogue("Eddy", "Na endlich. Okay Zeit dass wir in die Gegenwart zurückkehren. Bald haben wir meine ganze Familie gefunden."),
            };
        }
    }

}

public class Dialogue
{
    public Dialogue(string speaker, string text)
    {
        Speaker = speaker;
        Text = text;
    }

    public string Speaker
    {
        get;
        set;
    }

    public string Text
    {
        get;
        set;
    }
}

public enum DialogueSceneId
{
    PresentSearchPortal,
    Fire,
    AfterFire,
    RebuildTower,
    AfterRebuildTower
}

