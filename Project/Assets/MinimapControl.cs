using UnityEngine;
using System.Collections;

public class MinimapControl : MonoBehaviour {
    public Sprite ChunkOutline;
    public Sprite CellOutline;
    public Font UILabelFont;

    public UILabel[,] UILabels;

    public void Init(SudokuPuzzle puzzleRef) {
        int size = puzzleRef.size;
        UILabels = new UILabel[size * size, size * size];

        UIWidget widget = GetComponent<UIWidget>();
        Vector2 widgSize = widget.localSize;

        Vector2 chunkSize = widgSize / size;
        Vector2 cellSize = chunkSize / size;

        UI2DSprite URHere = transform.FindChild("URHere").GetComponent<UI2DSprite>();
        URHere.height = Mathf.RoundToInt(cellSize.y + 2);
        URHere.width = Mathf.RoundToInt(cellSize.x + 2);

        for(int chunk = 0; chunk < size * size; chunk++) {
            GameObject newChunk = MakeChunk(chunkSize, chunk);
            newChunk.transform.localPosition = new Vector2(chunkSize.x * (chunk % size), chunkSize.y * -(chunk / size)) + new Vector2(-chunkSize.x, chunkSize.y) * ((size - 1) * 0.5f);

            for(int cell = 0; cell < size * size; cell++) {
                GameObject newCell = MakeCell(newChunk.transform, cellSize, cell);
                newCell.transform.localPosition = new Vector2(cellSize.x * (cell % size), cellSize.y * -(cell / size)) + new Vector2(-cellSize.x, cellSize.y) * ((size - 1) * 0.5f);
                GameObject newUILabel = MakeUILabel(cellSize);
                newUILabel.transform.parent = newCell.transform;
                newUILabel.transform.localPosition = Vector3.zero;
                newUILabel.transform.localScale = Vector3.one;

                UILabels[cell % size + (chunk % size) * size, cell / size + (chunk / size) * size] = newUILabel.GetComponent<UILabel>();
            }
        }
    }

    private GameObject MakeChunk(Vector2 chunkSize, int chunk) {
        GameObject newChunk = new GameObject("Chunk " + (chunk + 1));
        newChunk.transform.parent = transform;
        UI2DSprite newChunkSprite = newChunk.AddComponent<UI2DSprite>();
        newChunkSprite.sprite2D = ChunkOutline;
        newChunkSprite.type = UIBasicSprite.Type.Sliced;
        newChunkSprite.border = new Vector4(2, 2, 2, 2);
        newChunkSprite.height = Mathf.RoundToInt(chunkSize.y);
        newChunkSprite.width = Mathf.RoundToInt(chunkSize.x);
        newChunkSprite.depth = 99;
        return newChunk;
    }

    private GameObject MakeCell(Transform chunk, Vector2 cellSize, int cell) {
        GameObject newCell = new GameObject("Cell " + (cell + 1));
        newCell.transform.parent = chunk;
        UI2DSprite newCellSprite = newCell.AddComponent<UI2DSprite>();
        newCellSprite.sprite2D = CellOutline;
        newCellSprite.type = UIBasicSprite.Type.Sliced;
        newCellSprite.border = new Vector4(1, 1, 1, 1);
        newCellSprite.height = Mathf.RoundToInt(cellSize.y);
        newCellSprite.width = Mathf.RoundToInt(cellSize.x);
        return newCell;
    }

    private GameObject MakeUILabel(Vector2 cellSize) {
        GameObject newUILabel = new GameObject("Label");
        UILabel label = newUILabel.AddComponent<UILabel>();
        label.ambigiousFont = UILabelFont;
        label.height = Mathf.RoundToInt(cellSize.y - 2);
        label.width = Mathf.RoundToInt(cellSize.x - 2);
        label.fontSize = 24;
        label.text = " ";
        label.maxLineCount = 1;
        label.depth = 98;
        label.overflowMethod = UILabel.Overflow.ShrinkContent;
        label.color = Color.black;
        return newUILabel;
    }
}
