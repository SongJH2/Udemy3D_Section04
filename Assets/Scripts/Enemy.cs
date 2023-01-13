using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] GameObject parentGameObject;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int hitPoints = 4;

    Rigidbody rigidbody;

    ScoreBoard scoreBoard;

    private void Start()
    {
        scoreBoard = GameObject.FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidBody();
    }

    private void AddRigidBody()
    {
        rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHitted();

        if (hitPoints < 1)
        {
            ProcessDied();
        }
    }

    void ProcessHitted()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;

        hitPoints--;
    }

    void ProcessDied()
    {
        scoreBoard.IncreaseScore(scorePerHit);

        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;

        Destroy(gameObject);
    }
}
