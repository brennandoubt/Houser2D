using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickerController2D : MonoBehaviour
{
    int currHomees;
    protected bool hit;
    protected bool minus;

    public float spawnWidth = 1;
    //public float spawnRate = 1;

    public GameObject HomeePrefab;
    public GameObject lastHomee;
    //public GameObject nextHomee;

    protected LinkedList<GameObject> HomeesInScene;
    void Start()
    {
        HomeesInScene = new LinkedList<GameObject>();
    }

    
    // spawning homee into scene on + click
    public void ClickPlus()
    {
        currHomees++;
        // TODO: add a homee to the scene (and maybe move into a house)
        hit = true;
    }
    // removing homee from scene on - click
    public void ClickMinus()
    {
        currHomees--;
        // TODO: remove homee ("Person" prefab) from scene (a "home" in scene probably)
        minus = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 spawnPos = transform.position;
        spawnPos += new Vector3(Random.Range(-spawnWidth, spawnWidth), 0, 0);

        if (hit)
        {
            GameObject newHomee = Instantiate(HomeePrefab, spawnPos, Quaternion.identity);
            HomeesInScene.AddLast(newHomee);
            //currHomees++;
            lastHomee = HomeePrefab;
            hit = false;
        }
        else if (minus)
        {
            if (HomeesInScene.Count > 0)
            {
                GameObject homeeToRemove = HomeesInScene.Last.Value; // last added to list is first removed (similar to stack data structure)
                Destroy(homeeToRemove);

                HomeesInScene.RemoveLast();
            }
            //currHomees--;
            minus = false;
        }
    }
}
