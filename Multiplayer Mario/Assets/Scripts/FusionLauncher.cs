using UnityEngine;
using Fusion;
using Fusion.Sockets;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NetworkRunner))]
public class FusionLauncher : MonoBehaviour
{
    [Header("Prefabs & Spawn")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform fixedSpawnPoint;

    private NetworkRunner runner;

    // Constants so host and client look for the same session on the same port
    private const ushort PORT = 7777;
    private const string SESSION_NAME = "LAN_Mario";

    private void Awake() => runner = GetComponent<NetworkRunner>();

    /* ---------- UI entry points ---------- */
    public async void StartHost()
    {
        runner.ProvideInput = true;          // local player can send input
        await runner.StartGame(new StartGameArgs
        {
            GameMode    = GameMode.Host,     // start a LAN host
            Address     = NetAddress.Any(PORT), // bind to 0.0.0.0:7777 and advertise
            SessionName = SESSION_NAME,
            Scene       = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex),
            SceneManager= gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    public async void StartClient()
    {
        runner.ProvideInput = true;
        await runner.StartGame(new StartGameArgs
        {
            GameMode    = GameMode.Client,   // act purely as a client
            Address     = NetAddress.Any(PORT), // broadcast-discover on the same port
            SessionName = SESSION_NAME,
            Scene       = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex),
            SceneManager= gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    /* ---------- (Optional) manual spawn helper if you need it somewhere else ---------- */
    public void SpawnPlayer(NetworkRunner r, PlayerRef p)
    {
        var pos = fixedSpawnPoint ? fixedSpawnPoint.position : Vector3.zero;
        r.Spawn(playerPrefab, pos, Quaternion.identity, p);
    }
}
