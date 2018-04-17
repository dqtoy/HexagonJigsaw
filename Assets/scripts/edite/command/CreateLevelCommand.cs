using lib;
using System.Collections.Generic;

/// <summary>
/// 创建关卡配置信息
/// </summary>
public class CreateLevelCommand
{
    public CreateLevelCommand()
    {
        EditorLevelConfig level = new EditorLevelConfig();
        List<EditorLevelPiece> pieces = level.pieces;
        EditorVO.Instance.level = level;

        List<GridVO> grids = new List<GridVO>();
        for(int i = 0; i < EditorVO.Instance.grids.Count; i++)
        {
            if(EditorVO.Instance.grids[i].color.value != 0)
            {
                grids.Add(EditorVO.Instance.grids[i]);
            }
        }
        List<GridVO> otherGrids1 = new List<GridVO>();
        for(int i = 0; i < EditorVO.Instance.otherGrids1.Count; i++)
        {
            if(EditorVO.Instance.otherGrids1[i].color.value != 0)
            {
                otherGrids1.Add(EditorVO.Instance.otherGrids1[i]);
            }
        }

        CheckPieces(level.pieces,grids);
        CheckPieces(level.otherPieces, otherGrids1);
    }

    private void CheckPieces(List<EditorLevelPiece> pieces,List<GridVO> grids)
    {
        while (grids.Count > 0)
        {
            GridVO grid = grids[0];
            grids.RemoveAt(0);
            EditorLevelPiece piece = new EditorLevelPiece();
            pieces.Add(piece);
            piece.grids.Add(new EditorLevelPieceGrid(grid.x.value, grid.y.value, grid.color.value));
            bool find = true;
            while (find)
            {
                find = false;
                for (int i = 0; i < piece.grids.Count; i++)
                {
                    GridVO nextGrid = GetGridNextTo(piece.grids[i].x, piece.grids[i].y, piece.grids[i].color, grids);
                    if (nextGrid != null)
                    {
                        grids.Remove(nextGrid);
                        piece.grids.Add(new EditorLevelPieceGrid(nextGrid.x.value, nextGrid.y.value, nextGrid.color.value));
                        find = true;
                    }
                }
            }
        }
    }

    private GridVO GetGridAt(int x,int y,List<GridVO> grids)
    {
        for(int i = 0; i < grids.Count; i++)
        {
            if(grids[i].x.value == x && grids[i].y.value == y)
            {
                return grids[i];
            }
        }
        return null;
    }

    private GridVO GetGridNextTo(int x,int y,int color, List<GridVO> grids)
    {
        List<Point2D> nextCoords = HaxgonCoord<Point2D>.GetCoordsNextTo(Point2D.Create(x, y));
        for (int i = 0; i < grids.Count; i++)
        {
            for(int n = 0; n < nextCoords.Count; n++)
            {
                if(grids[i].x.value == nextCoords[n].x && grids[i].y.value == nextCoords[n].y && grids[i].color.value == color)
                {
                    return grids[i];
                }
            }
        }
        return null;
    }
}