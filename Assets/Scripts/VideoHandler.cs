using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

public class VideoHandler : MonoBehaviour
{
    public static VideoHandler instance;

    [SerializeField] private VideoPlayer player;

    private string idleVideoPath = string.Empty;

    [SerializeField] private RawImage videoImage;

    [SerializeField] private RenderTexture videoTexture;

    [SerializeField] private float tweenSpeed = 0.25f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        PlayIdleVideo();
    }

    private void PlayIdleVideo()
    {
        string foldername = Path.Combine(Application.streamingAssetsPath, "home");

        if (Directory.Exists(foldername))
        {
            string[] videoFiles = Directory.GetFiles(foldername, "*.*")
                                           .Where(s => s.EndsWith(".mp4") || s.EndsWith(".mov") || s.EndsWith(".avi"))
                                           .ToArray();
            
            string path = Path.Combine(Application.streamingAssetsPath, foldername, videoFiles[0]);

            PlayVideo(path);
        }
        else
        {
            Debug.Log($"folder doesnt exist : {foldername}");
        }
    }

    public void PlayVideo(string path)
    {  
        StartFadeOut(path);
    }

    private void StartFadeOut(string path)
    {
        videoImage.DOColor(new Color(1, 1, 1, 0), tweenSpeed).OnComplete(() => StartFadeIn(path));
    }

    private void StartFadeIn(string path)
    {
        player.Stop();
        videoTexture.Release();
        player.url = path;
        player.Play();
        videoImage.DOColor(new Color(1, 1, 1, 1), tweenSpeed);
    }

    public void PauseOrPlayTheVideo()
    {
        //if video player is playing pause it
        if (player.isPlaying)
        {
            player.Pause(); 
        }
        else
        {
            player.Play();
        }
    }
}
