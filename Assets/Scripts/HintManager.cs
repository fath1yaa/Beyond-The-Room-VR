using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class HintManager : MonoBehaviour
{
    public enum ActiveRoom { SpyRoom, HorrorScene, MurderScene }

    [Header("Which room is this?")]
    public ActiveRoom currentRoom;

    [Header("Vosk")]
    public VoskSpeechToText voskSpeechToText;

    [Header("Room Managers")]
    public SpyRoomGameManager spyRoomManager;
    public HorrorGameManager horrorManager;
    public MurderScenePuzzle murderManager;

    private IEnumerator Start()
    {
        // Wait 3 seconds before starting Vosk to avoid startup crash
        yield return new WaitForSeconds(3f);
        UnityEngine.Debug.Log("[HintManager] Starting Vosk...");
        voskSpeechToText?.StartVoskStt();
    }
    public void GiveHint()
    {
        string hint = currentRoom switch
        {
            ActiveRoom.SpyRoom => GetSpyRoomHint(),
            ActiveRoom.HorrorScene => GetHorrorHint(),
            ActiveRoom.MurderScene => GetMurderHint(),
            _ => "No hint available."
        };

        UnityEngine.Debug.Log("[HintManager] Hint: " + hint);
        SpeakHint(hint);
    }

    private void SpeakHint(string message)
    {
        string safeMessage = message.Replace("'", "");
        string script = $"Add-Type -AssemblyName System.Speech; " +
                        $"$s = New-Object System.Speech.Synthesis.SpeechSynthesizer; " +
                        $"$s.Rate = 1; $s.Speak('{safeMessage}');";

        Process.Start(new ProcessStartInfo
        {
            FileName = "powershell",
            Arguments = $"-WindowStyle Hidden -Command \"{script}\"",
            UseShellExecute = false,
            CreateNoWindow = true
        });
    }

    private string GetSpyRoomHint()
    {
        if (spyRoomManager == null) return "Hint system not connected.";
        if (!spyRoomManager.corkboardInspected)
            return "Examine the corkboard. Two clues are pinned there, a keyword and a number.";
        if (!spyRoomManager.serverSolved)
            return "The server rack needs a colour sequence. Check the satellite photo for the order.";
        if (!spyRoomManager.workstationSolved)
            return "The workstation needs a password. Combine the keyword and number you found.";
        if (!spyRoomManager.doorCodeAccepted)
            return "Enter the door code on the keypad. The number was hidden in the server logs.";
        return "You have completed all puzzles. Find the exit door!";
    }

    private string GetHorrorHint()
    {
        if (horrorManager == null) return "Hint system not connected.";
        if (!horrorManager.allCandlesLit)
            return "Light all the candles in the room. Look for unlit ones in the dark corners.";
        if (!horrorManager.vialPuzzleSolved)
            return "Find the vials and arrange them in the correct order. Look around the room for clues.";
        if (!horrorManager.wallClueRead)
            return "There is something written on the wall. Look carefully around the room.";
        if (!horrorManager.doorUnlocked)
            return "You have the door code. Enter it on the keypad to unlock the door.";
        return "The door is unlocked. Escape now!";
    }

    private string GetMurderHint()
    {
        if (murderManager == null) return "Hint system not connected.";
        if (!murderManager.allCluesFound)
            return "Examine every object in the room. The knife, phone, and note each reveal a clue.";
        if (!murderManager.suspectIdentified)
            return "Study the suspect board carefully. Match the evidence to the correct photo card.";
        return "You have identified the culprit. Report your finding!";
    }
}