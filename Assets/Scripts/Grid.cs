using System;
using DefaultNamespace;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static event Action<Vector2Int> OnCellClick;

    [SerializeField] private EnemyPath enemyPath;
    [SerializeField] private Vector2Int cellSize;
    [SerializeField] private Texture2D[] textures;  // List of textures for the cells
    [SerializeField] private SpriteRenderer spriteRenderer; 
    
    private float cellSpacing;
    private CellData[][] grid;
    private Vector2Int gridSize;
    
    private void Start()
    {
        float cameraHeight = 2f * Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;
        
        int gridHeight = (int)math.ceil(cameraHeight / cellSize.y);
        int gridWidth = (int)math.ceil(cameraWidth / cellSize.x);
        gridSize = new Vector2Int(gridWidth, gridHeight);
        
        GenerateGrid();
        GenerateEnemyPath();
        cellSpacing = Vector2.Distance(grid[0][0].position, grid[0][1].position);
        
        MakeImagesReadeble();
        RenderGrid();
    }

    private void GenerateGrid()
    {
        grid = new CellData[gridSize.y][];
        
        for (int y = 0; y < gridSize.y; y++)
        {
            grid[y] = new CellData[gridSize.x];

            for (int x = 0; x < gridSize.x; x++)
            {
                Vector2 cellPos = new Vector2(transform.position.x + x + .5f, transform.position.y + y + .5f);
                grid[y][x] = new CellData(cellPos, false, 0);
            }
        }
    }

    private void GenerateEnemyPath()
    {
        foreach (var pathTile in enemyPath.path)
        {
            CellData cell = grid[pathTile.y][pathTile.x];
            cell.occupied = true;
            cell.textureID = 1;
        }
    }

    public CellData GetClosestCell(Vector2 position)
    {
        CellData closestCellData = null;
        float closestDistance = float.MaxValue;

        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                float distance = Vector2.Distance(grid[y][x].position, position);
            
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestCellData = grid[y][x];
                }

                if (closestDistance <= cellSpacing / 2)
                {
                    return closestCellData;
                }
            }
        }
        return closestCellData;
    }

    private void MakeImagesReadeble()
    {
        foreach (var texture in textures)
        {
            string assetPath = AssetDatabase.GetAssetPath(texture);
            TextureImporter textureImporter = (TextureImporter)AssetImporter.GetAtPath(assetPath);
            if (textureImporter != null)
            {
                textureImporter.isReadable = true;
                AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
            }
        }
    }
    
    private void RenderGrid()
    {
        // Get the size of individual textures (assuming all are the same size)
        int cellWidth = textures[0].width;
        int cellHeight = textures[0].height;
            
        // Create a new combined texture with the size of the entire grid
        Texture2D combinedTexture = new Texture2D(cellWidth * gridSize.x, cellHeight * gridSize.y);
        
        Debug.Log($"grass_image width: {textures[0].width}, grass_image height: {textures[0].height}, path_image width: {textures[0].width}, path_image height: {textures[0].height}");
        
        // Loop through the grid and place each texture at its correct position
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                // Get the texture for the current grid cell based on textureID
                Texture2D texture2D = textures[grid[y][x].textureID];

                // Get the pixel data of the current texture
                Color[] pixels = texture2D.GetPixels();

                // Set the pixels in the correct position (adjust Y to match Unity's bottom-left origin)
                combinedTexture.SetPixels(x * cellWidth, y * cellHeight, cellWidth, cellHeight, pixels);
            }
        }

        // Apply the changes to the combined texture
        combinedTexture.Apply();

        Debug.Log($"Height: {combinedTexture.height}, Width: {combinedTexture.width}");
            
        // Create a new sprite from the combined texture
        Sprite combinedSprite = Sprite.Create(combinedTexture, 
            new Rect(0, 0, combinedTexture.width, combinedTexture.height), 
            new Vector2(0.5f, 0.5f));

        // Set the combined sprite to the sprite renderer
        spriteRenderer.sprite = combinedSprite;
    }
}