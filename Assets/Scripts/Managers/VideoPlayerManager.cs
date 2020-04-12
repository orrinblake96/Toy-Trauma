using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Managers
{
    public class VideoPlayerManager : MonoBehaviour
    {
        public RawImage rawImage;

        public VideoPlayer staticVideo;
        
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(PlayVideo());
        }

        IEnumerator PlayVideo()
        {
            staticVideo.Prepare();
            while (!staticVideo.isPrepared)
            {
                yield return new WaitForSeconds(1f);
                break;    
            }

            rawImage.texture = staticVideo.texture;
            staticVideo.Play();
        }
    }
}
