using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;

public class ButtonData : MonoBehaviour
{
    public string path = string.Empty;
    private string foldeName = string.Empty;

    public Sprite selectedImage, deselectedImage;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();

        if(gameObject.name != "ecommerce")
            LoadThePath();
    }

    private void LoadThePath()
    {
        foldeName = gameObject.name;     

        path = Path.Combine(Application.streamingAssetsPath, foldeName);

        if (Directory.Exists(path))
        {
            string[] videoFiles = Directory.GetFiles(path, "*.*")
                                           .Where(s => s.EndsWith(".mp4") || s.EndsWith(".mov") || s.EndsWith(".avi"))
                                           .ToArray();

            path = Path.Combine(Application.streamingAssetsPath, foldeName, videoFiles[0]);
        }
    }    
}
