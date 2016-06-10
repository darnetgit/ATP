using ATP.MazeGenerators;
using ATP.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPProject.Model1
{
    public delegate void ModelEventDelegate(string Command);

    public interface IModel
    {
        event ModelEventDelegate Event;
        void GenerateMaze(string mazenam, int x, int y, int z);
        void SolveMaze(string mazename);
        void SaveMaze(string mazename, string path);
        void LoadMaze(string mazename, string path);
        void LoadDictionaryFromDisk(string path);
        void SaveDictionaryToDisk(string path);

        //CHECKKKKKKKKKKKKKKKKKKKKK
        string printsolution(string mazename);
        AMaze GetMaze(string mazename);
        Solution GetSolution(string mazename);
    }
}
