using UnityEngine;
using UnityEngine.UI;

public class ButtonExample : MonoBehaviour
{
    public Button button1, button2, button3;
    public bool isPause = false;

    private void Awake()
    {
        button1.onClick.AddListener(OnClickButton1);
        button2.onClick.AddListener(OnClickButton2);
        button3.onClick.AddListener(OnClickButton3);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isPause= !isPause;

            if(isPause)
            {
                Time.timeScale = 0f;
                print("<color=red>Pause</color>");
            }
            else
            {
                Time.timeScale = 1f;

                print("<color=blue>Pause</color>");
            }
        }
    }

    public void OnClickButton1()
    {
        print("OnClickButton1");
    }

    public void OnClickButton2()
    {
        print("OnClickButton2");
    }

    public void OnClickButton3()
    {
        print("OnClickButton3");
    }
}
