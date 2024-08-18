using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCanvas : MonoBehaviour
{
    [SerializeField] private List<CanvasObject> _objects;

    public void StartTheSlides()
    {
        foreach(var obj in _objects)
        {
            obj.StartSlide();
        }
    }
}
