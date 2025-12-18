using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfinerFinder : MonoBehaviour
{
    CinemachineCamera vcam;
    CinemachineConfiner2D confiner;

    private void Awake()
    {
        vcam = GetComponent<CinemachineCamera>();
        confiner = GetComponent<CinemachineConfiner2D>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(SetupCameraAfterSceneLoad());
    }

    private IEnumerator SetupCameraAfterSceneLoad()
    {
        // gaidam 1 kadru
        yield return null;

        // meklējam Player
        var player = GameObject.FindWithTag("Player");
        if (player != null)
            vcam.Follow = player.transform;

        // Meklējam Confiner
        var confObj = GameObject.FindWithTag("Confiner");
        if (confObj != null)
            confiner.BoundingShape2D = confObj.GetComponent<PolygonCollider2D>();
    }
}