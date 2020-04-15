﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.Track;

public class LevelManager : MonoBehaviour
{
    public int numberOfLevels = 2;

    public int curLevel = 1;

    TrackManager trackManager;

    SceneController sceneController;

    CutsceneController cutsceneController;

    void Start()
    {
        sceneController = GetComponent<SceneController>();
        sceneController.AddLoadCallback(SetTrackManager);
        sceneController.AddLoadCallback(SetTimeline);
    }

    void Update()
    {
        if (curLevel != 0 && trackManager != null && trackManager.IsRaceStopped)
        {
            cutsceneController.playCutscene();
            if (!cutsceneController.isPlaying())
            {
                print("LOADING SCENE");
                trackManager = null;
                sceneController.LoadScene(GetNextLevel());
            }
        }
    }

    void SetTrackManager(string sceneName)
    {
        if (curLevel != 0)
        {
            trackManager = GameObject.Find("TrackManager").GetComponent<TrackManager>();
        }
    }

    void SetTimeline(string sceneName)
    {
        if (curLevel != 0)
        {
            cutsceneController = GameObject.Find("Timeline").GetComponent<CutsceneController>();
        }

    }

    string GetNextLevel()
    {
        ++curLevel;
        if (curLevel > numberOfLevels)
        {
            curLevel = 1;
            return "Title";
        }
        else 
        {
            return "Level" + curLevel;
        }
    }
}
