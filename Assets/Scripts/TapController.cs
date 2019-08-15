using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TapController : MonoBehaviour
{
    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;

    public float tapForce = 10;
    public Vector3 startPosition;
    Rigidbody2D rigidBody;

    GameManager game;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.simulated = false;
        transform.localPosition = startPosition;
        game = GameManager.Instance;
    }

    void OnEnable()
    {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    void OnDisable()
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    void OnGameStarted()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.simulated = true;
    }

    void OnGameOverConfirmed()
    {
        transform.localPosition = startPosition;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "DeathZone")
        {
            // Freeze the player
            rigidBody.simulated = false;

            // Register a dead event
            OnPlayerDied(); // event sent to gamemanager

            // Play a sound

        }
        else if (col.gameObject.tag == "ScoreZone")
        {
            // register a score event
            OnPlayerScored(); // event sent to GameManager
            
            // Play a sound
        }
    }
}
