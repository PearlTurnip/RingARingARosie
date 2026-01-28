using UnityEngine;

public class BigSceneManager : MonoBehaviour
{

    private string currentScene = "menu";
    public void LoadScene(string scene)
    {
        foreach (Renderer thing in FindObjectsByType<Renderer>(FindObjectsSortMode.None))
        {
            if (!thing.CompareTag("persistent") && !thing.CompareTag("MainCamera") && !thing.CompareTag("Canvas"))
            {
                thing.forceRenderingOff = true;
            }
        }
        foreach (CanvasRenderer thing in FindObjectsByType<CanvasRenderer>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            if (!thing.CompareTag("persistent") && !thing.CompareTag("MainCamera") && !thing.CompareTag("Canvas"))
            {
                thing.gameObject.SetActive(false);
            }
        }

        switch (scene)
        {
            case "serve":
                foreach (Renderer thing in FindObjectsByType<Renderer>(FindObjectsSortMode.None))
                {
                    if (thing.CompareTag("UIServeScene") || thing.CompareTag("ServeScene"))
                    {
                        thing.forceRenderingOff = false;
                    }
                }
                foreach (CanvasRenderer thing in FindObjectsByType<CanvasRenderer>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                {
                    if (thing.CompareTag("UIServeScene") || thing.CompareTag("ServeScene"))
                    {
                        thing.gameObject.SetActive(true);
                    }
                }
                break;
            case "mask":
                foreach (Renderer thing in FindObjectsByType<Renderer>(FindObjectsSortMode.None))
                {
                    if (thing.CompareTag("UIMaskScene") || thing.CompareTag("MaskScene") || thing.CompareTag("FlowerPot") || thing.CompareTag("Mask"))
                    {
                        thing.forceRenderingOff = false;
                    }
                }
                foreach (CanvasRenderer thing in FindObjectsByType<CanvasRenderer>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                {
                    if (thing.CompareTag("UIMaskScene") || thing.CompareTag("MaskScene"))
                    {
                        thing.gameObject.SetActive(true);
                    }
                }
                break;
            case "grow":
                foreach (Renderer thing in FindObjectsByType<Renderer>(FindObjectsSortMode.None))
                {
                    if (thing.CompareTag("UIGrowScene") || thing.CompareTag("GrowScene"))
                    {
                        thing.forceRenderingOff = false;
                    }
                }
                foreach (CanvasRenderer thing in FindObjectsByType<CanvasRenderer>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                {
                    if (thing.CompareTag("UIGrowScene") || thing.CompareTag("GrowScene"))
                    {
                        thing.gameObject.SetActive(true);
                    }
                }
                break;
            case "menu":
                break;
        }
    }

    private void Start()
    {
        LoadScene("menu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentScene = "serve";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentScene = "mask";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentScene = "grow";
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            currentScene = "menu";
        }

        LoadScene(currentScene);
    }
}
