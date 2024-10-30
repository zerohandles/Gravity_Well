using UnityEngine;

// Not included in build, used to take screen shots for itch.io submission page
public class ScreenShot : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            string screenshotName;
            int randomNumber = Random.Range(0, 10000);

            screenshotName = "ScreenShot" + randomNumber + ".png";

            ScreenCapture.CaptureScreenshot(screenshotName);
        }
    }
}
