using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BackgroundVidStarter : MonoBehaviour
{
    private void Awake()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            videoPlayer.url = "StreamableAssets/star.mp4";
        }
    }

    private void Update()
    {
        Debug.Log("video player playing: " + GetComponent<VideoPlayer>().isPlaying);
    }
}
