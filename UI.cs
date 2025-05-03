using MelonLoader;
using UnityEngine;
using UnityEngine.UI;

namespace Repo_Library.UI
{
    public class UI : MelonMod
    {
        public Vector3[] CalculateCorners(RectTransform rectTransform)
        {
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            return corners;
        }

        // Get window height
        public float GetWindowHeight()
        {
            return Screen.height;
        }

        // Get window width
        public float GetWindowWidth()
        {
            return Screen.width;
        }
        public Vector3 GetBottomLeft(Canvas canvas)
        {
            Vector3[] corners = CalculateCorners(canvas.GetComponent<RectTransform>());
            return corners[0];
        }

        public Vector3 GetTopLeft(Canvas canvas)
        {
            Vector3[] corners = CalculateCorners(canvas.GetComponent<RectTransform>());
            return corners[1];
        }

        public Vector3 GetTopRight(Canvas canvas)
        {
            Vector3[] corners = CalculateCorners(canvas.GetComponent<RectTransform>());
            return corners[2];
        }

        public Vector3 GetBottomRight(Canvas canvas)
        {
            Vector3[] corners = CalculateCorners(canvas.GetComponent<RectTransform>());
            return corners[3];
        }

        // Create a canvas
        public Canvas CreateCanvasComponent(string name)
        {
            Canvas canvas = new GameObject(name).AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 999999;
            CanvasScaler scaler = canvas.gameObject.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvas.gameObject.AddComponent<GraphicRaycaster>();
            Object.DontDestroyOnLoad(canvas.gameObject);
            return canvas;
        }

        // Create a text component
        public TextMesh CreateTextComponent(GameObject parent, string text, Vector3 position, Color color, int fontSize)
        {
            GameObject textObject = new GameObject("Repo_Mod_Library_Generated_Text_Component");
            textObject.transform.SetParent(parent.transform);
            textObject.transform.localPosition = position;
            textObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            TextMesh textMesh = textObject.AddComponent<TextMesh>();
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.characterSize = 1f;
            textMesh.color = color;
            textMesh.alignment = TextAlignment.Center;
            textMesh.anchor = TextAnchor.MiddleCenter;
            textMesh.transform.LookAt(Camera.main.transform);
            return textMesh;
        }

        // Create a button
        public Button CreateButtonComponent(GameObject parent, string text, Vector3 position, Color color, int fontSize)
        {
            GameObject buttonObject = new GameObject("Repo_Mod_Library_Generated_Button_Component");
            Button button = buttonObject.AddComponent<Button>();
            GameObject textObject = new GameObject("Repo_Mod_Library_Generated_Button_Text_Component");
            textObject.transform.SetParent(parent.transform);
            textObject.transform.localPosition = position;
            TextMesh textMesh = textObject.AddComponent<TextMesh>();
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.characterSize = 1f;
            textMesh.color = color;
            textMesh.alignment = TextAlignment.Center;
            textMesh.anchor = TextAnchor.MiddleCenter;
            textMesh.transform.LookAt(Camera.main.transform);
            return button;
        }

        // Create a panel holder
        public GameObject CreatePanelHolderComponent(GameObject parent, Vector3 position, Vector3 scale)
        {
            GameObject panelHolderObject = new GameObject("Repo_Mod_Library_Generated_PanelHolder_Component");
            panelHolderObject.transform.SetParent(parent.transform);
            panelHolderObject.transform.localPosition = position;
            panelHolderObject.transform.localScale = scale;
            return panelHolderObject;
        }

        // Create a TextArea
        public GameObject CreateTextAreaComponent(GameObject parent, string text, Vector3 position, Color color, int fontSize)
        {
            GameObject textAreaObject = new GameObject("Repo_Mod_Library_Generated_TextArea_Component");
            textAreaObject.transform.SetParent(parent.transform);
            textAreaObject.transform.localPosition = position;
            TextMesh textMesh = textAreaObject.AddComponent<TextMesh>();
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.characterSize = 1f;
            textMesh.color = color;
            textMesh.alignment = TextAlignment.Center;
            textMesh.anchor = TextAnchor.MiddleCenter;
            textMesh.transform.LookAt(Camera.main.transform);
            return textAreaObject;
        }

        // Create a panel
        public GameObject CreatePanelComponent(GameObject parent, Vector3 position, Color color, Vector3 scale)
        {
            GameObject panelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
            panelObject.transform.SetParent(parent.transform);
            panelObject.transform.localPosition = position;
            panelObject.transform.localScale = scale;
            panelObject.GetComponent<Renderer>().material.color = color;
            return panelObject;
        }

        // Create an input field
        public GameObject CreateInputFieldComponent(GameObject parent, string text, Vector3 position, Color color, int fontSize)
        {
            GameObject inputFieldObject = new GameObject("Repo_Mod_Library_Generated_InputField_Component");
            inputFieldObject.transform.SetParent(parent.transform);
            inputFieldObject.transform.localPosition = position;
            TextMesh textMesh = inputFieldObject.AddComponent<TextMesh>();
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.characterSize = 1f;
            textMesh.color = color;
            textMesh.alignment = TextAlignment.Center;
            textMesh.anchor = TextAnchor.MiddleCenter;
            textMesh.transform.LookAt(Camera.main.transform);
            return inputFieldObject;
        }

        // Create Material
        public Material CreateMaterial(Color color)
        {
            Material material = new Material(Shader.Find("Standard"))
            {
                color = color
            };
            return material;
        }

        // Update Material
        public void UpdateMaterial(Material material, Color color)
        {
            material.color = color;
        }

        // Create a texture from color
        public Texture TextureFromColor(Color color)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, color);
            texture.Apply();
            return texture;
        }

        // Create image background
        public GameObject CreateImageComponent(Canvas canvas, string name, float width, float height, Vector3 position, Color color)
        {
            GameObject background = new GameObject(name);
            background.transform.parent = canvas.transform;
            RectTransform backgroundTransform = background.AddComponent<RectTransform>();
            backgroundTransform.transform.localPosition = position;
            background.AddComponent<RawImage>().color = color;
            backgroundTransform.anchorMin = new Vector2(0.5f, 1);
            backgroundTransform.anchorMax = new Vector2(0.5f, 1);
            backgroundTransform.pivot = new Vector2(0.5f, 1);
            backgroundTransform.sizeDelta = new Vector2(width, height);
            return background;
        }

        // Create a texture from image
        public Texture TextureFromImage(string image)
        {
            byte[] fileData = System.IO.File.ReadAllBytes($"/images/{image}");
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadRawTextureData(fileData);
            return texture;
        }

        // Update a texture
        public void UpdateTexture(Texture texture, Color color)
        {
            Texture2D texture2D = (Texture2D)texture;
            for (int x = 0; x < texture2D.width; x++)
            {
                for (int y = 0; y < texture2D.height; y++)
                {
                    texture2D.SetPixel(x, y, color);
                }
            }
            texture2D.Apply();
        }

        // Create a sprite from texture
        public Sprite SpriteFromTexture(Texture texture)
        {
            return Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }

        // Create a sprite from image
        public Sprite SpriteFromImage(string image)
        {
            Texture texture = TextureFromImage(image);
            return SpriteFromTexture(texture);
        }

        // Create a sprite renderer
        public SpriteRenderer CreateSpriteRenderer(GameObject parent, Sprite sprite, Vector3 position, Vector3 scale)
        {
            GameObject spriteObject = new GameObject("Repo_Mod_Library_Generated_SpriteRenderer_Component");
            spriteObject.transform.SetParent(parent.transform);
            spriteObject.transform.localPosition = position;
            spriteObject.transform.localScale = scale;
            SpriteRenderer spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            return spriteRenderer;
        }

        // Update a sprite renderer
        public void UpdateSpriteRenderer(SpriteRenderer spriteRenderer, Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
        }

        // Create a mesh renderer
        public MeshRenderer CreateMeshRenderer(GameObject parent, Mesh mesh, Material material, Vector3 position, Vector3 scale)
        {
            GameObject meshObject = new GameObject("Repo_Mod_Library_Generated_MeshRenderer_Component");
            meshObject.transform.SetParent(parent.transform);
            meshObject.transform.localPosition = position;
            meshObject.transform.localScale = scale;
            MeshFilter meshFilter = meshObject.AddComponent<MeshFilter>();
            meshFilter.mesh = mesh;
            MeshRenderer meshRenderer = meshObject.AddComponent<MeshRenderer>();
            meshRenderer.material = material;
            return meshRenderer;
        }

        // Update a mesh renderer
        public void UpdateMeshRenderer(MeshRenderer meshRenderer, Mesh mesh, Material material)
        {
            meshRenderer.material = material;
            meshRenderer.GetComponent<MeshFilter>().mesh = mesh;
        }

        // Move a component
        public void MoveComponent(GameObject component, Vector3 position)
        {
            component.transform.localPosition = position;
        }

        // Resize a component
        public void ResizeComponent(GameObject component, Vector3 scale)
        {
            component.transform.localScale = scale;
        }

        // Change the font size of a component
        public void ChangeFontSize(GameObject component, int fontSize)
        {
            TextMesh textMesh = component.GetComponent<TextMesh>();
            if (textMesh != null)
            {
                textMesh.fontSize = fontSize;
            }
        }

        // Change the font color of a component
        public void ChangeFontColor(GameObject component, Color color)
        {
            TextMesh textMesh = component.GetComponent<TextMesh>();
            if (textMesh != null)
            {
                textMesh.color = color;
            }
        }

        // Change the text of a component
        public void ChangeText(GameObject component, string text)
        {
            TextMesh textMesh = component.GetComponent<TextMesh>();
            if (textMesh != null)
            {
                textMesh.text = text;
            }
        }
    }
}