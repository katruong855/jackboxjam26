using UnityEngine;
using UnityEngine.SceneManagement;
using PurrNet;
using PurrNet.Modules;

public class SceneManagement : NetworkBehaviour
{
    // TODO: Initialize SyncList of all players as the server
    // TODO: Initialize SyncList of all unplayed minigames in the pool
    // TODO: Initialize SyncList of all players who haven't been the carnie (the prompter)
    

    [PurrScene] public string mainGame;

    public string minigame1 = "PettingZoo";
    // public string minigame2 = "FishingDucks";
    // public string minigame3 = "BalloonDarts";
    // public string minigame4 = "BumperCars";

    // This variable stores the newly chosen minigame
    private string chosenMinigame;

    [ContextMenu("Change scene for everyone")]
    private void ChangeSceneForEveryone()
    {
        networkManager.sceneModule.LoadSceneAsync(minigame1 + "_Player");
    }

    [ContextMenu("Change scene")]
    private void ChangeSceneToMinigame()
    {
        // Need to determine who is who 
        // who is the carnie and everyone else
        // TODO: Randomly pick a minigame out of the pool of games that haven't been played yet
        chosenMinigame = minigame1;
        
        // Loads the new minigame scene
        PurrSceneSettings settings = new()
        {
            isPublic = false,
            mode = LoadSceneMode.Additive
        };
        networkManager.sceneModule.LoadSceneAsync(chosenMinigame, settings);



        // TODO: Randomly get a player ID from the pool of possible carnies (anyone who hasn't been one yet)
        // If the list is empty and the game is not over, then we'll reset to put everyone back into the pool
        // if this player is that player ID then put them into the carnie scene

        // else, request to put them into the player

        
    }


    [ServerRpc(requireOwnership: false)]
    private void RequestScenePlayer(RPCInfo info = default)
    {
        var scene = SceneManager.GetSceneByName(chosenMinigame + "_Player");
        if(!scene.isLoaded)
            return;
        
        if (networkManager.sceneModule.TryGetSceneID(scene, out SceneID sceneID))
            networkManager.scenePlayersModule.AddPlayerToScene(info.sender, sceneID);
    }

    [ServerRpc(requireOwnership: false)]
    private void RequestSceneCarnie(RPCInfo info = default)
    {
        var scene = SceneManager.GetSceneByName(chosenMinigame + "_Carnie");
        if(!scene.isLoaded)
            return;
        
        if (networkManager.sceneModule.TryGetSceneID(scene, out SceneID sceneID))
            networkManager.scenePlayersModule.AddPlayerToScene(info.sender, sceneID);
    }

    [ContextMenu("Request scene change to player version")]
    private void RequestScenePlayerLocal()
    {
        RequestScenePlayer();
    }

    [ContextMenu("Request scene change to carnie version")]
    private void RequestSceneCarnieLocal()
    {
        RequestSceneCarnie();
    }
}
