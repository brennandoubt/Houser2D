using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlusClickerController : MonoBehaviour
{
    public int currHomees;
    
    // spawning homee into scene on + click
    void ClickPlus()
    {
        currHomees++;
        // TODO: add a homee to the scene (and maybe move into a house)
    }
    // removing homee from scene on - click
    void ClickMinus()
    {
        currHomees--;
        // TODO: remove homee ("Person" prefab) from scene (a "home" in scene probably)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
