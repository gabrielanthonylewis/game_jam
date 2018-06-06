using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PlayerIndicator : MonoBehaviour {


    private Image _Image = null;

    void Start()
    {
        _Image = this.GetComponent<Image>();
    }

    public void SetColour(Color col)
    {
        if(_Image == null)
            _Image = this.GetComponent<Image>();

        _Image.color = col;
    }
}
