using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATP.Search;
using System.Reflection;
using System.Threading;
using System.Runtime.CompilerServices;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using ATP.MazeGenerators;
using System.Collections;
using ATP.Compression;
using System.IO.Compression;

namespace ATPProject.Model1
{

    class Model : IModel
    {
        public event ModelEventDelegate Event;
        public Dictionary<string, List<object>> m_mazesAndSolutins;
        public List<Thread> generate;
        public List<Thread> solve;
        int nuofthreads;
        string solvingalgo;
        string generatealgo;
        public Model()
        {
            ActivateThreadPool();
            generate = new List<Thread>();
            solve = new List<Thread>();
            nuofthreads = Settings.Default.Threads;
            solvingalgo = Settings.Default.SolvingAlgorithm;
            generatealgo = Settings.Default.Generate;
            if (!File.Exists("mazes_solutions.zip"))
                m_mazesAndSolutins = new Dictionary<string, List<object>>();
            else
                LoadDictionaryFromDisk("mazes_solutions.zip");
        }

        private void ActivateThreadPool()
        {
            int workingThreads = 10;
            int completedThreads = 10;
            ThreadPool.SetMaxThreads(workingThreads, completedThreads);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void AddSolution(string mazename, Solution solution)
        {
            m_mazesAndSolutins[mazename].Add(solution);
            SaveDictionaryToDisk("mazes_solutions.zip");
        }
        public Solution GetSolution(string mazename)
        {
            return RetrieveSolution(mazename);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        private Solution RetrieveSolution(string mazename)
        {
            if (m_mazesAndSolutins.ContainsKey(mazename))
                return (m_mazesAndSolutins[mazename].ElementAt(1) as Solution);
            return null;
        }

        private void AddMaze(string mazename, AMaze maze)
        {
            m_mazesAndSolutins[mazename] = new List<object>();
            m_mazesAndSolutins[mazename].Add(maze);
            SaveDictionaryToDisk("mazes_solutions.zip");
        }
        public AMaze GetMaze(string mazename)
        {
            return RetrieveMaze(mazename);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        private AMaze RetrieveMaze(string mazename)
        {
            if (m_mazesAndSolutins.ContainsKey(mazename))
                return (m_mazesAndSolutins[mazename].ElementAt(0) as AMaze);
            return null;
        }

        public void SolveMaze(string mazename)
        {
            if (m_mazesAndSolutins.ContainsKey(mazename))
            {
                if (m_mazesAndSolutins[mazename].Count > 1)
                    Event("SolutionExist " + mazename);
                else
                    Solve(mazename);
            }
            else
                Event("MazeNotExist " + mazename);
        }

        public void GenerateMaze(string mazename, int x, int y, int z)
        {
            if (!m_mazesAndSolutins.ContainsKey(mazename.ToLower()))
                RunGenerate(mazename, x, y, z);
            else
                Event("MazeExist " + mazename);
        }
        private void Solve(string mazename)
        {
            foreach (string key in m_mazesAndSolutins.Keys)
            {
                if (!key.Equals(mazename))
                {
                    if ((m_mazesAndSolutins[key].ElementAt(0) as AMaze).Equals((m_mazesAndSolutins[mazename].ElementAt(0) as AMaze)) && m_mazesAndSolutins[key].Count > 1)
                        m_mazesAndSolutins[mazename].Add(m_mazesAndSolutins[key].ElementAt(1));
                }
                return;
            }
            ThreadPool.QueueUserWorkItem(new WaitCallback((state) =>
                {
                    SearchableMaze3d sm = new SearchableMaze3d((m_mazesAndSolutins[mazename].ElementAt(0) as Maze3d));
                    Solution sol;
                    if (solvingalgo.ToLower().Equals("dfs"))
                    {
                        DepthFirstSearch ds = new DepthFirstSearch();
                        sol = ds.Solve(sm);
                    }
                    else
                    {
                        BreadthFirstSearch bs = new BreadthFirstSearch();
                        sol = bs.Solve(sm);
                    }
                    if (sol != null)
                    {
                        AddSolution(mazename, sol);
                        //solve.Add()
                        Event("SolutionReady " + mazename);
                    }

                })
                );
        }
        private void RunGenerate(string mazename, int x, int y, int z)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback((state) =>
            {
                ArrayList s = new ArrayList();
                s.Add(x);
                s.Add(y);
                s.Add(z);
                MyMaze3dGenerator mg = new MyMaze3dGenerator();
                AMaze maze = mg.generate(s);
                if (maze != null)
                {
                    AddMaze(mazename, maze);
                    
                    Event("MazeReady " + mazename);
                }
            })
                );
        }
        public string printsolution(string mazename)
        {
            Solution sol = GetSolution(mazename);
            string solution = "";
            foreach (Astate position in sol.m_solu)
            {
                solution = solution + position.state + " \n";
            }
            return solution;
        }
        public void LoadDictionaryFromDisk(string path)
        {
            GZipStream zip = new GZipStream(File.OpenRead(path), CompressionMode.Decompress);
            try
            {
                m_mazesAndSolutins = (Dictionary<string, List<object>>)(new BinaryFormatter()).Deserialize(zip);
            }
            catch
            {
                Event("UnzipError  ");
            }
            finally
            {
                if (zip != null)
                    ((IDisposable)zip).Dispose();
            }
        }

        public void SaveDictionaryToDisk(string path)
        {
            byte[] tocompress;
            MemoryStream memorystram = new MemoryStream();
            try
            {
                GZipStream zip = new GZipStream(memorystram, CompressionMode.Compress);
                try
                {
                    (new BinaryFormatter()).Serialize(zip, m_mazesAndSolutins);
                }
                catch
                {
                    Event("ZipError  ");
                }
                finally
                {
                    if (zip != null)
                        ((IDisposable)zip).Dispose();
                }
                tocompress = memorystram.ToArray();
            }
            finally
            {
                if (memorystram != null)
                    ((IDisposable)memorystram).Dispose();
            }
            File.WriteAllBytes(path, tocompress);
        }

        public void SaveMaze(string mazename, string path)
        {
            //string path = mazename + ".maze";
            if (m_mazesAndSolutins.ContainsKey(mazename.ToLower()))
            {
                byte[] comp = (m_mazesAndSolutins[mazename.ToLower()].ElementAt(0) as Maze3d).toByteArray();
                using (FileStream fileOutStream = new FileStream(path, FileMode.Create))
                {
                    using (Stream outStream = new MyCompressorStream(fileOutStream, 1))
                    {
                        int current = 0;
                        while (current < comp.Length)
                        {
                            if (current + 100 < comp.Length)
                                outStream.Write(comp, current, 100);
                            else
                                outStream.Write(comp, current, comp.Length - current);
                            current = current + 100;
                        }
                        outStream.Flush();
                    }
                }
            }
            Event("MazeSaved " + mazename);
        }
        public void LoadMaze(string mazename, string path)
        {
            //string path = mazename + ".maze";
            if (!m_mazesAndSolutins.ContainsKey(mazename.ToLower()))
            {
                //  (m_controller as MyController).M_view.Output("destroying old maze (including solution if exists) and creating a new one");
                Event("MazeNotExist " + mazename);
            }
            byte[] todecompress = File.ReadAllBytes(path);
            int[] sizes = getMazeSizes(todecompress);
            byte[] buffer = new byte[sizes[0] * sizes[1] * sizes[2] + 3];
            using (FileStream fileOutStream = new FileStream(path, FileMode.Open))
            {
                using (Stream outStream = new MyCompressorStream(fileOutStream, 2))
                {
                    int current = 0;
                    while (current < buffer.Length)
                    {
                        if (current + 100 < buffer.Length)
                            outStream.Read(buffer, current, 100);
                        else
                            outStream.Read(buffer, current, buffer.Length - current);
                        current = current + 100;
                    }
                    outStream.Flush();
                }
            }
            AMaze maze = new Maze3d(buffer);
            if (maze != null)
            {
                //m_mazesAndSolutins[mazename.ToLower()].Add(maze);
                Event("DisplayMaze " + mazename);
            }
            else
                Event("DisplayError " + mazename);

        }
        private int[] getMazeSizes(byte[] bytearray)
        {
            int[] toreturn = new int[3];
            if (bytearray[1] == 0)
            {
                toreturn[0] = bytearray[0];
                if (bytearray[3] == 0)
                {
                    toreturn[1] = bytearray[2];
                    toreturn[2] = bytearray[4];
                }
                else
                    toreturn[1] = toreturn[2] = bytearray[2];
            }
            else if (bytearray[1] == 1)
            {
                toreturn[0] = toreturn[1] = bytearray[0];
                toreturn[2] = bytearray[2];
            }
            else if (bytearray[1] == 2)
            {
                toreturn[0] = toreturn[1] = toreturn[2] = bytearray[0];
            }
            return toreturn;
        }

    }

}
