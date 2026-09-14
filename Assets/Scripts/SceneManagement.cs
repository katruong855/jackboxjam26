using UnityEngine;
using PurrNet;

public class SceneManagement : NetworkBehaviour
{
    [PurrScene] public string sceneToChange;
    [ContextMenu("Change scene")]
    private void ChangeScene()
    {

    }
}
