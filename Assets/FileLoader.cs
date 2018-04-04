using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileLoader{

    List<List<Coordinate>> positions;
    List<Coordinate> vespidPositions;
    List<List<bool>> aliveList;

    public FileLoader()
    {
        positions = new List<List<Coordinate>>();
        aliveList = new List<List<bool>>();
    }
    public void loadPositions(string file)
    {
        positions = new List<List<Coordinate>>();
        aliveList = new List<List<bool>>();
        string[] lines = System.IO.File.ReadAllLines(file);
        foreach (string line in lines)
        {
            if (line.Contains("apid"))
            {
                positions.Add(LineToCoordinate(line));
            }
            else if (line.Contains("vespid"))
            {
                vespidPositions = (LineToCoordinate(line));        
            }
        }
    }

    public List<Coordinate> LineToCoordinate(string line)
    {
        List<Coordinate> positions = new List<Coordinate>();
        List<bool> alive = new List<bool>();
        string[] splitLine = line.Split(':');
        foreach(string s in splitLine)
        {
            if(!(s.Contains("apid") || s.Contains("vespid")))
            {
                string[] splitCoord = s.Split(',');
                positions.Add(new Coordinate(int.Parse(splitCoord[0]), int.Parse(splitCoord[1])));
                alive.Add(splitCoord[2].Equals("1") ? true : false);
            }
        }
        aliveList.Add(alive);
        return positions;
    }

    public List<List<Coordinate>> getPositions()
    {
        return this.positions;
    }

    public List<Coordinate> getVespidPositions()
    {
        return this.vespidPositions;
    }

    public List<List<bool>> getAliveList()
    {
        return this.aliveList;
    }
}
