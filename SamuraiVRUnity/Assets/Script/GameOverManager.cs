using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    //controllers
    private InputDevice rightTargetDevice;
    private InputDevice LeftTargetDevice;

    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> allDevices = new List<InputDevice>();
        InputDeviceCharacteristics righControllerChar = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDeviceCharacteristics leftControllerChar = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;


        //Find device that match with right controller and apply the first one
        InputDevices.GetDevicesWithCharacteristics(righControllerChar, allDevices);
        //If it has at least one
        if (allDevices.Count > 0) rightTargetDevice = allDevices[0];

        //Find device that match with left controller and apply the first one
        InputDevices.GetDevicesWithCharacteristics(leftControllerChar, allDevices);
        //If it has at least one
        if (allDevices.Count > 0) LeftTargetDevice = allDevices[0];
    }

    // Update is called once per frame
    void Update()
    {
        //goes back to the main game if you press "A" in the Oculus quest controller
        rightTargetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryRightControllerButton);
        //goes back to the main game if you press "X" in the Oculus quest controller
        LeftTargetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryLeftControllerButton);
        if (primaryRightControllerButton)
        {
            SceneManager.LoadScene("MainGame");
        }
        //Quit game if you press "X" in the Oculus quest controller
        else if (primaryLeftControllerButton)
        {
            Application.Quit();
        }
    }
}
