using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugTester : MonoBehaviour
{
    public DiceRoller diceRoller;
    public PlayerProp player;
    public UIManager uiManager;
    public WaypointMover waypointMover;

    void Start()
    {
        if (diceRoller == null) diceRoller = Object.FindAnyObjectByType<DiceRoller>();
        if (player == null) player = Object.FindAnyObjectByType<PlayerProp>();
        if (uiManager == null) uiManager = Object.FindAnyObjectByType<UIManager>();
        if (waypointMover == null) waypointMover = Object.FindAnyObjectByType<WaypointMover>();

        RunTests();
    }

    public void RunTestsManually() => RunTests();

    void RunTests()
    {
        Debug.Log("Starting Debug Tests...");

        TestDiceRoller();
        TestPlayerProperties();
        TestUIManager();
        TestWaypointMover();
        TestWaypointArray();
        TestSceneReload();
        TestAudioComponents();
        TestAnimator();
        TestRigidbody();
        TestGameObjectActivation();
        TestPlayerMovement();
        TestMissingMaterials(player?.gameObject);
        TestMissingMaterials(gameObject); // For this object......
        TestFPS();

        Debug.Log("Debug Tests Completed.");
    }

    void TestDiceRoller()
    {
        if (!CheckComponent(diceRoller, "DiceRoller")) return;

        diceRoller.RollDice();
        Debug.Log("Dice Rolled: " + diceRoller.diceValues[0] + ", " + diceRoller.diceValues[1]);

        if (diceRoller.diceValues[0] < 1 || diceRoller.diceValues[0] > 6 ||
            diceRoller.diceValues[1] < 1 || diceRoller.diceValues[1] > 6)
        {
            Debug.LogError("Dice roll values are out of expected range!");
        }
    }

    void TestPlayerProperties()
    {
        if (!CheckComponent(player, "PlayerProp")) return;

        if (player.transform == null)
        {
            Debug.LogError("Player has no transform component!");
        }
    }

    void TestUIManager()
    {
        if (!CheckComponent(uiManager, "UIManager")) return;

        if (!uiManager.gameObject.activeSelf)
        {
            Debug.LogWarning("UIManager is disabled in the scene!");
        }
    }

    void TestWaypointMover()
    {
        if (!CheckComponent(waypointMover, "WaypointMover")) return;

        Vector3 initialPosition = waypointMover.transform.position;
        waypointMover.MoveToNextWaypoint();

        if (waypointMover.transform.position == initialPosition)
        {
            Debug.LogError("WaypointMover did not change position after MoveToNextWaypoint!");
        }
    }

    void TestWaypointArray()
    {
        if (waypointMover != null && (waypointMover.waypoints == null || waypointMover.waypoints.Length == 0))
        {
            Debug.LogError("WaypointMover has no waypoints assigned!");
        }
    }

    void TestSceneReload()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log("Scene Name: " + scene.name);
    }

    void TestAudioComponents()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio == null)
        {
            Debug.LogWarning("No AudioSource attached to DebugTester.");
        }
        else
        {
            Debug.Log("AudioSource found. Is playing: " + audio.isPlaying);
        }
    }

    void TestAnimator()
    {
        Animator animator = player?.GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("Player does not have an Animator component.");
        }
        else
        {
            Debug.Log("Animator component present. Initialized: " + animator.isInitialized);
        }
    }

    void TestRigidbody()
    {
        Rigidbody rb = player?.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning("Player does not have a Rigidbody component.");
        }
        else
        {
            Debug.Log("Rigidbody found. IsKinematic: " + rb.isKinematic);
        }
    }

    void TestPlayerMovement()
    {
        var movement = player?.GetComponent<PlayerMovement>();
        if (movement == null)
        {
            Debug.LogWarning("PlayerMovement script not found on Player.");
        }
        else if (!movement.enabled)
        {
            Debug.LogWarning("PlayerMovement script is disabled.");
        }
        else
        {
            Debug.Log("PlayerMovement script is active.");
        }
    }

    void TestMissingMaterials(GameObject go)
    {
        if (go == null) return;

        Renderer renderer = go.GetComponent<Renderer>();
        if (renderer != null && renderer.sharedMaterial == null)
        {
            Debug.LogWarning(go.name + " has missing material!");
        }
    }

    void TestFPS()
    {
        float fps = 1.0f / Time.deltaTime;
        Debug.Log("Current FPS: " + Mathf.RoundToInt(fps));
    }

    void TestGameObjectActivation()
    {
        if (player != null && !player.gameObject.activeInHierarchy)
        {
            Debug.LogWarning("Player GameObject is not active in hierarchy.");
        }

        if (uiManager != null && !uiManager.gameObject.activeInHierarchy)
        {
            Debug.LogWarning("UIManager GameObject is not active in hierarchy.");
        }
    }

    bool CheckComponent<T>(T component, string name) where T : Object
    {
        if (component == null)
        {
            Debug.LogError($"{name} is not assigned!");
            return false;
        }
        return true;
    }
}
