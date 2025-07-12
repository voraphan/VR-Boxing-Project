using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private string menuSceneName;
    // Start is called before the first frame update
    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SkipVideo();
        }
    }
    private void SkipVideo()
    {
        videoPlayer.Stop();
        SceneManager.LoadScene(menuSceneName);

    }
    private void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(menuSceneName);
    }
}
