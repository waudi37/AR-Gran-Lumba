using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWebsite : MonoBehaviour
{

    public void OpenWeb(string url)
    {
        Application.OpenURL(url);
    }
}
