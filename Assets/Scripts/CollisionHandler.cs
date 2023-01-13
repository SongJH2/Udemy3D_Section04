using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashVFX;

    private void OnTriggerEnter(Collider other)
    {
        StartCrashSwquence();        
    }

    private void StartCrashSwquence()
    {
        crashVFX.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<PlayerControls>().enabled = false;        
        GetComponent<BoxCollider>().enabled = false;        
        Invoke("ReloadLevel", loadDelay);
    }

    void ReloadLevel()
    {
        int sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneBuildIndex);
    }
}
