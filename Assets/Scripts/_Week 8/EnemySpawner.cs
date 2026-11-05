using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySpawner : MonoBehaviour
{
    // Complete each section marked TODO.

    [Header("Spawner Settings")]
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public float spawnRadius = 8f;

    [Header("Animation")]
    public Animator animator;
    public bool playIdleAnimationOnStart = true;
    public string idleAnimationName = "Idle";

    [Header("Spawn Feedback")]
    public GameObject spawnParticlePrefab;
    public float spawnParticleLifetime = 2f;
    public AudioClip spawnSound;
    public AudioSource audioSource;

    [Header("Game Manager")]
    public GameManager gameManager;

    [Header("Debug")]
    public bool spawningEnabled = true;
    public bool enableDebugKeys = true;
    public bool showDebugLogs = true;

    public Key toggleSpawnKey = Key.P;
    public Key spawnOnceKey = Key.O;
    public Key destroyEnemiesKey = Key.I;

    [Header("Debug View")]
    public float spawnTimer = 0f;

    // This collection stores all enemies created by this spawner.
    public List<GameObject> spawnedEnemies = new List<GameObject>();

    private void Awake()
    {
        // Check if audioSource is null.
        // If it is, get the AudioSource component from this GameObject.
        if(audioSource == null)
            audioSource = GetComponent<AudioSource>();


        // Check if animator is null.
        // If it is, get the Animator component from this GameObject.
        if(animator  == null)
            animator = GetComponent<Animator>();


        // Check if gameManager is null.
        // If it is, find the GameManager in the scene.
        if (gameManager == null)
            gameManager = GameObject.FindAnyObjectByType<GameManager>();

    }

    private void Start()
    {
        // Call the function that plays the idle animation.
        PlayIdleAnimation();


        // TODO: If debug logs are enabled, print that the EnemySpawner has started.
        if (showDebugLogs)
            Debug.Log("Enemy Spawner has started");


        // Check that gameManager is not null.
        // If it exists, call AddSpawner.
        if (gameManager != null)
            gameManager.AddSpawner();

    }

    private void Update()
    {
        // Call the function that handles the debug input.
        HandleDebugInput();


        // Check whether spawning is disabled.
        // If it is, stop this function.
        if (!spawningEnabled)
            return;


        // Check whether enemyPrefab is null.
        // If it is:
        // - Print a warning if debug logs are enabled.
        // - Stop this function.
        if(enemyPrefab == null)
        {
            if (showDebugLogs)
                Debug.Log("No enemy prefab assigned");

            return;
        }


        // Add Time.deltaTime to spawnTimer.
        spawnTimer += Time.deltaTime;


        // Check whether spawnTimer is greater than or equal to spawnInterval.
        // If it is:
        // - Reset spawnTimer to 0.
        // - Call SpawnEnemy.
        if(spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemy();
        }

    }

    private void PlayIdleAnimation()
    {
        // Check whether playIdleAnimationOnStart is false.
        // If it is, stop this function.
        if (!playIdleAnimationOnStart)
            return;


        // Check whether animator is null.
        // If it is:
        // - Print a warning if debug logs are enabled.
        // - Stop this function.
        if(animator == null)
        {
            if(showDebugLogs)
            {
                Debug.Log("No Animator on enemy spawner");
            }

            return;
        }


        // Check whether idleAnimationName is empty.
        // Hint: Use string.IsNullOrWhiteSpace().
        // If it is:
        // - Print a warning if debug logs are enabled.
        // - Stop this function.
        if (string.IsNullOrWhiteSpace(idleAnimationName))
        {
            if (showDebugLogs) {
                Debug.Log("Idle Animation name is null");
            }
            return;
        }

        // Play the animation stored in idleAnimationName.
        animator.Play(idleAnimationName);


        // If debug logs are enabled, print the animation being played.
        if(showDebugLogs)
        {
            Debug.Log($"Playing {idleAnimationName} animation");
        }

    }

    private void HandleDebugInput()
    {
        //  Check whether debug keys are disabled.
        // If they are, stop this function.
        if (!showDebugLogs)
            return;

        // Check whether Keyboard.current is null.
        // If it is, stop this function.
        if (Keyboard.current == null)
            return;


        // Check whether toggleSpawnKey was pressed.
        // If it was:
        // - Toggle spawningEnabled between true and false.
        // - Print the new value if debug logs are enabled.
        if (Keyboard.current[toggleSpawnKey].wasPressedThisFrame)
        {
            spawningEnabled = !spawningEnabled;

            if(showDebugLogs)
            {
                Debug.Log("Spawning Enabled");
            }
        }

        // Check whether spawnOnceKey was pressed.
        // If it was:
        // - Call SpawnEnemy.
        // - Print a debug message if debug logs are enabled.
        if (Keyboard.current[spawnOnceKey].wasPressedThisFrame)
        {
            SpawnEnemy();

            if (showDebugLogs)
            {
                Debug.Log("Enemy Spawned");
            }
        }


        // Check whether destroyEnemiesKey was pressed.
        // If it was, call DestroySpawnedEnemies.
        if (Keyboard.current[destroyEnemiesKey].wasPressedThisFrame)
        {
            DestroySpawnedEnemies();

            if (showDebugLogs)
            {
                Debug.Log("Destroying Spawned Enemies");
            }
        }

    }

    public void StopSpawning()
    {
        // Set spawningEnabled to false.
        spawningEnabled = false;


        // If debug logs are enabled, print that spawning has stopped.
        if(showDebugLogs)
            Debug.Log("Stopped spawning");

    }

    public void SpawnerDestroyed()
    {
        // Check that gameManager is not null.
        // If it exists, call RemoveSpawner.
        if (gameManager == null)
            return;

        gameManager.RemoveSpawner();

    }

    private void SpawnEnemy()
    {
        // Check whether enemyPrefab is null.
        // If it is:
        // - Print a warning if debug logs are enabled.
        // - Stop this function.
        if(enemyPrefab == null)
        {
            if (showDebugLogs)
                Debug.Log("No enemy prefab.");

            return;
        }


        // Generate a random position inside the spawn radius.
        Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;

        // Calculate the final spawn position using
        // this GameObject's position and randomOffset.
        Vector3 spawnPosition = this.transform.position;
        spawnPosition.x = randomOffset.x; 
        spawnPosition.y = randomOffset.y;

        // Instantiate enemyPrefab at spawnPosition.
        // Store the newly created GameObject in enemyObject.
        GameObject enemyObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Add enemyObject to the spawnedEnemies collection.
        spawnedEnemies.Add(enemyObject);

        // Call PlaySpawnFeedback and give it spawnPosition.
        PlaySpawnFeedback(spawnPosition);

        // If debug logs are enabled, print where the enemy spawned.
        if (showDebugLogs)
            Debug.Log($"Enemy Spawned at {spawnPosition}");

    }

    public void DestroySpawnedEnemies()
    {
        // Loop through every GameObject in the spawnedEnemies collection.
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            // TODO: Check that the enemy at position i is not null.
            if(spawnedEnemies[i] != null)
            {
                Destroy(spawnedEnemies[i]);
            }
        }

        // Clear the spawnedEnemies collection.
        spawnedEnemies.Clear();


        // If debug logs are enabled, print that all spawned enemies were destroyed.
        if(showDebugLogs)
        {
            Debug.Log("All spawned enemies destroyed");
        }

    }

    private void PlaySpawnFeedback(Vector3 spawnPosition)
    {
        // Check that spawnParticlePrefab is not null.
        if (spawnParticlePrefab != null)
        {
            // Instantiate spawnParticlePrefab at spawnPosition.
            // Store the newly created GameObject in particleObject.
            GameObject particleObject = Instantiate(spawnParticlePrefab, spawnPosition, Quaternion.identity);

            //Destroy particleObject after spawnParticleLifetime seconds.
            Destroy(particleObject, spawnParticleLifetime);
        }

        // Check that spawnSound and audioSource are not null.
        // If they exist, play spawnSound using the AudioSource.
        if (spawnSound != null && audioSource != null) { 
            audioSource.PlayOneShot(spawnSound);
        }

        // If debug logs are enabled, print that the spawn feedback played.
        if (showDebugLogs)
        {
            Debug.Log("Spawn Feedback Played");
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}