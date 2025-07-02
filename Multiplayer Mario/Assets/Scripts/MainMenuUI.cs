using UnityEngine;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [Header("Scene References")]
        public FusionLauncher launcher;
        public TMP_InputField ipInput;
    
        public void OnClickHost()
        {
            launcher.StartHost();
        }
    
        public void OnClickJoin()
        {
            launcher.StartClient(ipInput.text);
        }
}
