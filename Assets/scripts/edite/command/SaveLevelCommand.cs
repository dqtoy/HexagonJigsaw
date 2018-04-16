using lib;
using System;
using System.IO;

public class SaveLevelCommand
{
    public SaveLevelCommand(int level)
    {

        //读取配置
        //ConfigDecode.Decode();

        //删除之前的 level 相关信息
        if(LevelConfig.GetConfig(level) != null)
        {
            LevelConfig old = LevelConfig.GetConfig(level);
            LevelConfig.Configs.Remove(old);
            for(int i = 0; i < old.pieces.Count; i++)
            {
                PieceConfig.Configs.Remove(PieceConfig.GetConfig(old.pieces[i].id));
            }
            for (int i = 0; i < old.pieces2.Count; i++)
            {
                PieceConfig.Configs.Remove(PieceConfig.GetConfig(old.pieces2[i].id));
            }
        }
        
        LevelConfig levelConfig = new LevelConfig();
        LevelConfig.Configs.Add(levelConfig);
        levelConfig.id = level;

        for(int i = 0; i < EditorVO.Instance.level.pieces.Count; i++)
        {
            EditorLevelPiece piece = EditorVO.Instance.level.pieces[i];
            PieceConfig pieceConfig = new PieceConfig();
            levelConfig.pieces.Add(pieceConfig);
            pieceConfig.id = level * 100 + i;
            for (int c = 0; c < piece.grids.Count; c++)
            {
                if(CoordConfig.GetConfig(piece.grids[c].x * 1000 + -piece.grids[c].y) == null)
                {
                    CoordConfig coord = new CoordConfig();
                    coord.x = piece.grids[c].x;
                    coord.y = piece.grids[c].y;
                    coord.id = piece.grids[c].x * 1000 + -piece.grids[c].y;
                    CoordConfig.Configs.Add(coord);
                }
                pieceConfig.coords.Add(CoordConfig.GetConfig(piece.grids[c].x * 1000 + -piece.grids[c].y));
            }
            PieceConfig.Configs.Add(pieceConfig);
        }

        for (int i = 0; i < EditorVO.Instance.level.otherPieces.Count; i++)
        {
            EditorLevelPiece piece = EditorVO.Instance.level.otherPieces[i];
            PieceConfig pieceConfig = new PieceConfig();
            levelConfig.pieces2.Add(pieceConfig);
            pieceConfig.id = level * 100 + EditorVO.Instance.level.pieces.Count + i;
            for (int c = 0; c < piece.grids.Count; c++)
            {
                if (CoordConfig.GetConfig(piece.grids[c].x * 1000 + -piece.grids[c].y) == null)
                {
                    CoordConfig coord = new CoordConfig();
                    coord.x = piece.grids[c].x;
                    coord.y = piece.grids[c].y;
                    coord.id = piece.grids[c].x * 1000 + -piece.grids[c].y;
                    CoordConfig.Configs.Add(coord);
                }
                pieceConfig.coords.Add(CoordConfig.GetConfig(piece.grids[c].x * 1000 + -piece.grids[c].y));
            }
            PieceConfig.Configs.Add(pieceConfig);
        }
        Save();
    }

    private void Save()
    {
        //保存 CoordConfig
        string file = "int,int,int\n";
        file += "id,x,y\n";
        for (int i = 0; i < CoordConfig.Configs.Count; i++)
        {
            file += CoordConfig.Configs[i].id + "," + CoordConfig.Configs[i].x + "," + CoordConfig.Configs[i].y + "\n";
        }
        SaveFile("./Assets/Resources/config/Coord.csv", file);

        //保存 PieceConfig
        file = "int,(Coord)Array(int)\n";
        file += "id,coords\n";
        for (int i = 0; i < PieceConfig.Configs.Count; i++)
        {
            file += PieceConfig.Configs[i].id + "," + "\"";
            for (int c = 0; c < PieceConfig.Configs[i].coords.Count; c++)
            {
                file += PieceConfig.Configs[i].coords[c].id + (c < PieceConfig.Configs[i].coords.Count - 1 ? "," : "");
            }
            file += "\"\n";
        }
        SaveFile("./Assets/Resources/config/Piece.csv", file);

        //保存 LevelConfig
        file = "int,(Piece)Array(int),(Piece)Array(int)\n";
        file += "id,pieces,pieces2\n";
        for (int i = 0; i < LevelConfig.Configs.Count; i++)
        {
            file += LevelConfig.Configs[i].id + "," + "\"";
            for (int c = 0; c < LevelConfig.Configs[i].pieces.Count; c++)
            {
                file += LevelConfig.Configs[i].pieces[c].id + (c < LevelConfig.Configs[i].pieces.Count - 1 ? "," : "");
            }
            file += "\",\"";
            for (int c = 0; c < LevelConfig.Configs[i].pieces2.Count; c++)
            {
                file += LevelConfig.Configs[i].pieces2[c].id + (c < LevelConfig.Configs[i].pieces2.Count - 1 ? "," : "");
            }
            file += "\"\n";
        }
        SaveFile("./Assets/Resources/config/Level.csv", file);
    }

    private void SaveFile(string url,string content)
    {
        FileStream fs = new FileStream(url, FileMode.Create);
        //获得字节数组
        byte[] data = System.Text.Encoding.Default.GetBytes(content);
        //开始写入
        fs.Write(data, 0, data.Length);
        //清空缓冲区、关闭流
        fs.Flush();
        fs.Close();
    }
}