using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LandscapeManager : MonoBehaviour
{

  public int blockedCellProbability;

  GameObject player;
  GameObject grid;
  GameObject mainCamera;

  Vector3 origin;
  Vector3Int intOrigin;
  Tilemap ground, blockingCells, boundaries;
  // Tilemap onGround;
  Tile [][] world;
  char [, ] cellType;
  int psInPrevRow = 0;

  public int COLS;
  public int ROWS;
  public int NUM_OF_PARTS;

  public Sprite[] blockedCellSprites;

  // public Sprite[] onGroundCellSprites;
  public Sprite boundarySprite;
  public Sprite[] normalCellSprites;

  public void init() {
    player = GameManager.instance.getPlayer();
    grid = GameManager.instance.getGrid();
    mainCamera = GameManager.instance.getMainCamera();
    origin = mainCamera.GetComponent<Camera>().ViewportToWorldPoint(mainCamera.transform.position);
    intOrigin = Vector3Int.FloorToInt(origin);
    ground = grid.transform.Find("Ground").gameObject.GetComponent<Tilemap>();
    // onGround = grid.transform.Find("On Ground").gameObject.GetComponent<Tilemap>();
    blockingCells = grid.transform.Find("Blocking Cells").gameObject.GetComponent<Tilemap>();
    boundaries = grid.transform.Find("Boundaries").gameObject.GetComponent<Tilemap>();
    world = new Tile[ROWS][];
    cellType = new char[ROWS + 1, COLS];

    generateWorldPart(0, true);
    displayWorldPart(0);
    generateBoundaryRow(-1);

    // generateWorldPart(1);
    // displayWorldPart(1);
    //
    // generateWorldPart(2);
    // displayWorldPart(2);
    // string s = "";
    // for (int i = ROWS; i >= 0; --i) {
    //   for (int j = 0; j < COLS; ++j) {
    //     if (j != 0)
    //       s += ' ';
    //     s += cellType[i, j];
    //   }
    //   s += '\n';
    // }
    // print(s);

  }

  Tile[] buildRow(int curRowNum) {
    psInPrevRow = 0;
    for (int j = 1; j < COLS - 1; ++j) {
      if (cellType[curRowNum - 1, j] == 'P')
      ++psInPrevRow;
    }
    Tile[] curRow = new Tile[COLS];
    for (int i = 0; i < COLS; ++i) {
      curRow[i] = buildCell(i, curRowNum);
    }
    return curRow;
  }

  Tile buildCell(int curColNum, int curRowNum) {
    Tile curCell = ScriptableObject.CreateInstance<Tile>();

    char curCellType = 'B';

    if (curColNum == 0 || curColNum == COLS - 1) {
      curCell.sprite = boundarySprite;
      // boundaries.SetTile(pos, curCell);
      cellType[curRowNum, curColNum] = curCellType;
      return curCell;
    }

    // Check previous row
    switch (cellType[curRowNum - 1, curColNum]) {
      case 'X':
      case 'N':
      curCellType = randomXWithProbability(blockedCellProbability);
      break;
      case 'P':
      if (psInPrevRow > 1 && curRowNum > 1)  {
        curCellType = randomXWithProbability(blockedCellProbability);
        if (curCellType == 'N')
          curCellType = 'P';
        else
          --psInPrevRow;
      }
      else {
        curCellType = 'P';
      }
      break;
    }

    cellType[curRowNum, curColNum] = curCellType;

    switch (cellType[curRowNum, curColNum]) {
      case 'X':
      curCell.sprite = blockedCellSprites[Random.Range(0, blockedCellSprites.Length)];
      // blockingCells.SetTile(pos, curCell);
      break;
      case 'N':
      case 'P':
      curCell.sprite = normalCellSprites[Random.Range(0, normalCellSprites.Length)];
      // ground.SetTile(pos, curCell);
      break;
    }

    // iterate row to update Ps
    return curCell;
  }

  char randomXWithProbability(int prob) {
    int seed = Random.Range(0, 100);
    if (seed < Mathf.Max(15, prob - GameManager.instance.difficulty * 0.2f))
      return 'X';
    else
      return 'N';
  }

  void connectPath(int rowNum) {
    List<int> nIndices = new List<int>();
    for (int i = 1; i < COLS - 1; ++i) {
      bool seenP = false;
      for (int j = i; j < COLS - 1; ++j) {
        bool breakFromJLoop = false;
        switch (cellType[rowNum, j]) {
          case 'X':
          if (seenP) {
            foreach (int nIndex in nIndices) {
              cellType[rowNum, nIndex] = 'P';
            }
          }
          nIndices.Clear();
          i = j;
          breakFromJLoop = true;
          break;
          case 'N':
          nIndices.Add(j);
          break;
          case 'P':
          seenP = true;
          break;
        }
        if (breakFromJLoop)
        break;
      }
    }
  }

  void generateBoundaryRow(int rowNumber) {
    for (int j = 0; j < COLS; ++ j) {
      Tile curCell = ScriptableObject.CreateInstance<Tile>();
      curCell.sprite = boundarySprite;
      boundaries.SetTile(new Vector3Int(intOrigin.x + j, intOrigin.y + rowNumber, 0), curCell);
    }
  }

  void displayRows(int firstRow, int lastRow) {
    for (int i = firstRow; i <= lastRow; ++i) {
      for (int j = 0; j < COLS; ++ j)
        displayTile(i, j);
    }
  }

  void displayTile(int row, int col) {
    // print("(" + row + ", " + col + ")");
    Vector3Int pos = new Vector3Int(intOrigin.x + col, intOrigin.y + row, 0);
    Tile curCell = world[row][col];

    switch (cellType[row + 1, col]) {
      case 'B':
      boundaries.SetTile(pos, curCell);
      break;
      case 'X':
      blockingCells.SetTile(pos, curCell);
      break;
      case 'N':
      case 'P':
      ground.SetTile(pos, curCell);
      break;
    }
  }

  // Be careful! This is destructive, the new half will replace an old half
  void generateWorldPart(int part, bool initial = false) {
    cellType[0, 0] = 'B';
    cellType[0, COLS - 1] = 'B';
    if (part == 0) {
      if (initial) {
        for (int j = 1; j < COLS - 1; ++j) {
          cellType[0, j] = 'P';
        }
      }
      else {
        for (int j = 1; j < COLS - 1; ++j) {
          cellType[0, j] = cellType[ROWS, j];
        }
      }
    }
    for (int i = part * ROWS / NUM_OF_PARTS; i < ROWS / NUM_OF_PARTS * (part + 1); ++i) {
      world[i] = buildRow(i + 1);
      connectPath(i + 1);
    }
  }

  void displayWorldPart(int part) {
    displayRows(part * ROWS / NUM_OF_PARTS, ROWS / NUM_OF_PARTS * (part + 1) - 1);
  }
}
