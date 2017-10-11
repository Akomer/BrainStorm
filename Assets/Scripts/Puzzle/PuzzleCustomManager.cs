using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class PuzzleCustomManager : MonoBehaviour
{
    public GameObject customPanel;
    public Slider widthSlider;
    public Slider heightSlider;
    public RawImage image;
    [Tooltip("Main Menu Manager")]
    public PuzzleMenuManager manager;

    private const float maxHeight = 165;
    private const float maxWidth = 250;
    private const float widthHeightRatio = maxWidth / maxHeight;

    public void StartCustomGame()
    {
        customPanel.SetActive(false);
        manager.StartCustomGame((int)widthSlider.value, (int)heightSlider.value, image);
    }

    public void PickImage()
    {
        var imagePath = EditorUtility.OpenFilePanel("Select image", "", "png");
        if (imagePath.Length != 0)
        {
            LoadImage(imagePath);
        }
    }

    private void LoadImage(string path)
    {
        var url = "file://" + path;
        using (var www = new WWW(url))
        {
            var texture = www.texture;
            if (texture == null)
            {
                Debug.LogError("Failed to load texture url:" + url);
            }
            else
            {
                var size = texture.width;
                var sizeRatio = (float)texture.width / (float)texture.height;
                if (sizeRatio > widthHeightRatio)
                {
                    var imageSize = image.rectTransform.sizeDelta;
                    imageSize.y = maxWidth / sizeRatio;
                    imageSize.x = maxWidth;
                    image.rectTransform.sizeDelta = imageSize;
                }
                else
                {
                    var imageSize = image.rectTransform.sizeDelta;
                    imageSize.x = maxHeight * sizeRatio;
                    imageSize.y = maxHeight;
                    image.rectTransform.sizeDelta = imageSize;
                }
                image.texture = texture;
            }
        }
    }
}
