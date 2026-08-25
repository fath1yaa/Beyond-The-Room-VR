using UnityEngine;

public class SpyRoomGameManager : MonoBehaviour
{
    [Header("Puzzle Progress")]
    public bool corkboardInspected = false;
    public bool serverSolved = false;
    public bool workstationSolved = false;
    public bool doorCodeAccepted = false;
    public bool missionComplete = false;

    [Header("Puzzle Answers")]
    public string serverSequence = "BLUE-AMBER-BLUE-RED";
    public string workstationPassword = "PORT7ECHO";
    public string finalDoorCode = "7392";
}