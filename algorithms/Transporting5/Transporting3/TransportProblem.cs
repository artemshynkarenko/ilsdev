using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO;


namespace Transporting3
{
    public class TransportProblem
    {
        public int m, n;
        public int[] a;
        public int[] b;
        public int[,] c;
        public int[,] x;
        public int[] alpha;
        public int[] beta;
        public int[,] delta;

        public string path;

        private int[,] parrX;
        private int[,] parrY;
        private bool[,] was;

        private System.Collections.ArrayList cycleX;
        private System.Collections.ArrayList cycleY;

        private int cycleCount = 0;

        private int posX, posY;
        
        private bool cycleFinded;

        private bool tt = true;

        public TransportProblem() 
        {
            m = 1;
            n = 1;
        }
        public TransportProblem(int m, int n)
        {
            this.m = m;
            this.n = n;
            CreateArrays();
        }
        private void CreateArrays() 
        {
            a = new int[m];
            b = new int[n];
            c = new int[m, n];
            x = new int[m, n];
            alpha = new int[m];
            beta = new int[n];
            delta = new int[m,n];

            cycleX = new System.Collections.ArrayList();
            cycleY = new System.Collections.ArrayList();

            parrX = new int[m, n];
            parrY = new int[m, n];

            was = new bool[m, n];
            for (int i = 0; i < m; ++i)
            {
                a[i] = 0;
                alpha[i] = 0;
            }
            for (int i = 0; i < n; ++i)
            {
                b[i] = 0;
                beta[i] = 0;
            }
            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                {
                    c[i, j] = x[i, j] = parrX[i, j] = parrY[i, j] = delta[i, j] = 0;
                    was[i, j] = false;
                }


        }
        public void SetSize(int m, int n)
        {
            this.m = m;
            this.n = n;
            CreateArrays();
        }
        public void SaveToFile(string fileName)
        {

            string []res = new string [3+m];
            res[0] = m + " " + n;

            res[1] = a[0].ToString();
            for (int i = 1; i < m; ++i)
                res[1] += " " + a[i];

            res[2] = b[0].ToString();
            for (int i = 1; i < n; ++i)
                res[2] += " " + b[i];

            for (int i = 0; i < m; ++i) 
            {
                res[i + 3] = c[i,0].ToString();
                for (int j = 1; j < n; ++j)
                    res[i + 3] += " " + c[i,j];
            }
            File.WriteAllLines(fileName, res);
        }

        public void LoadFromFile(string fileName)
        {

            string[] res = File.ReadAllLines(fileName);
            string[] s = res[0].Split();
            m = int.Parse(s[0]);
            n = int.Parse(s[1]);

            SetSize(m, n);
            s = res[1].Split();
            for (int i = 0; i < m; ++i)
                a[i] = int.Parse(s[i]);

            s = res[2].Split();
            for (int i = 0; i < n; ++i)
                b[i] = int.Parse(s[i]);

            for (int i = 0; i < m; ++i)
            {
                s = res[i + 3].Split();
                for (int j = 0; j < n; ++j)
                    c[i,j] = int.Parse(s[j]);
            }
           
        }

        public void getPlanNorthWest()
        {
            int []ax = new int[m];
            int []bx = new int[n];
            for (int i = 0; i < m; ++i)
                ax[i] = a[i];
            for (int i = 0; i < n; ++i)
                bx[i] = b[i];

            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    x[i, j] = 0;

            int ii, jj;
            for (ii = 0, jj = 0; ii < m && jj < n; )
            {
                if (ax[ii] >= bx[jj])
                {
                    x[ii,jj] += bx[jj];
                    if (x[ii, jj] == 0)
                        x[ii, jj] = -1;
                    ax[ii] -= bx[jj];
                    bx[jj] = 0;
                    ++jj;
                }
                else
                    if (ax[ii] < bx[jj])
                    {
                        x[ii, jj] += ax[ii];
                        if (x[ii, jj] == 0)
                            x[ii, jj] = -1;
                        bx[jj] -= ax[ii];
                        ax[ii] = 0;
                        ++ii;
                    }
                    else 
                    {
                    }
            }
            if (ii < m)
            {
                ++ii;
                --jj;
                for (; ii < m; ++ii)
                    x[ii, jj] = -1;
            }
            else
                if (jj < n)
                {
                    ++jj;
                    --ii;
                    for (; jj < n; ++jj)
                        x[ii, jj] = -1;
                }

            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    if (x[i, j] == 0)
                        x[i, j] = -1;
                    else
                        if (x[i, j] == -1)
                            x[i, j] = 0;
        }

        public void getPlanMinElement()
        {

        }

        public string CalcPotencial()
        {
            int count = 0;
            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    if (x[i, j] >= 0)
                        ++count;
            if (count != m + n - 1)
                return "Кількість базисних елементів не рівна m+n-1";
            const int INF = 10000000;
            for (int i = 0; i < m; ++i)
                alpha[i] = -INF;
            for (int i = 0; i < n; ++i)
                beta[i] = -INF;
            alpha[0] = 0;
            count = 1;
            for (int k = 0; k < m * n + 1; ++k) 
            {
                for (int i=0; i<m; ++i)
                    for (int j=0; j<n; ++j)
                        if (x[i, j] >= 0)
                        {
                            if (alpha[i] != -INF && beta[j] == -INF)
                            {
                                beta[j] = c[i, j] + alpha[i];
                                ++count;
                            }
                            else
                                if (alpha[i] == -INF && beta[j] != -INF)
                                {
                                    alpha[i] = beta[j] - c[i, j];
                                    ++count;
                                }
                        }
            }
            if (count != m + n)
                return "Неможливо обчислити потенціали";

            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    delta[i, j] = beta[j] - alpha[i] - c[i, j];
                    
            return "";
        }

        private void go(int px, int py, bool horizont)
        {

            if (cycleCount != 1 && px == posX && py == posY && tt == horizont)
                cycleFinded = true;
            if (cycleFinded)
                return;
            cycleCount++;
            if (horizont)
            {
                for (int i = 0; i < n; ++i)
                    if (i != py && x[px, i] >= 0 && !was[px, i])
                    {
                        parrX[px, i] = px;
                        parrY[px, i] = py;
                        was[px, i] = true;
                        go(px, i, !horizont);
                        was[px, i] = false;
                    }
            }
            else
            {
                for (int i = 0; i < m; ++i)
                    if (i != px && x[i, py] >= 0 && !was[i, py])
                    {
                        parrX[i, py] = px;
                        parrY[i, py] = py;
                        was[i, py] = true;
                        go(i, py, !horizont);
                        was[i, py] = false;
                    }
            }
        }

        public string NextCycle() 
        {
            int count = 0;
            posX = 0;
            posY = 0;
            int d = 0;
            int co = 100000000;
            for (int i=0; i<m; ++i)
                for (int j=0; j<n; ++j)
                    if (delta[i, j] > 0)
                    {
                        if (d < delta[i, j] || (d == delta[i, j] &&  co>c[i,j])) 
                        {
                            co = c[i, j];
                            d = delta[i, j];
                            posX = i;
                            posY = j;
                        }
                        ++count;
                    }
            if (count == 0)
                return "finish";

            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    was[i, j] = false;
            //was[posX, posY] = ;
            x[posX, posY] = 0;
            cycleX.Clear();
            cycleY.Clear();
            path = "";
            cycleFinded = false;
            cycleCount = 1;
            tt = true;
            go(posX, posY, true);
            //go(posX, posY, false);
            x[posX, posY] = -1;
            if (!cycleFinded)
                return "Неможливо знайти цикл";
            //int px = posX, py = posY;
            int px = parrX[posX, posY];
            int py = parrY[posX, posY];
            cycleX.Add(posX);
            cycleY.Add(posY);
            cycleCount = 1;
            int t=0;
            for (; !(px == posX && py == posY); t=px, px = parrX[t, py], py = parrY[t, py])
            {
                cycleX.Add(px);
                cycleY.Add(py);
                ++cycleCount;
            }

          //      cycleCount--;
            path = "A" + (posX+1) + "B" + (posY+1);
            for (int i = 1; i < cycleCount; ++i)
                path += " - " + "A" + ((int)cycleX[i] + 1) + "B" + ((int)cycleY[i] + 1);

            return "next";
        }

        public void CalcCycle() 
        {
            x[(int)cycleX[0], (int)cycleY[0]] = 0;
            int theta = 10000000;
            for (int i = 0; i < cycleCount; ++i)
            {
                if (i % 2 == 1)
                {
                    if (theta > x[(int)cycleX[i], (int)cycleY[i]])
                        theta = x[(int)cycleX[i], (int)cycleY[i]];
                }
            }
            int px=0, py=0;
            for (int i = 0; i < cycleCount; ++i)
            {
                if (i % 2 == 1)
                {
                    if (theta == x[(int)cycleX[i], (int)cycleY[i]])
                    {
                        px = (int)cycleX[i];
                        py = (int)cycleY[i];
                        break;
                    }
                }
            }

            for (int i = 0; i < cycleCount; ++i)
            {
                if (i % 2 == 1)
                {
                    x[(int)cycleX[i], (int)cycleY[i]] -= theta;
                    if ((int)cycleX[i] == px && (int)cycleY[i] == py)
                        x[(int)cycleX[i], (int)cycleY[i]] = -1;
                }
                else
                {
                    x[(int)cycleX[i], (int)cycleY[i]] += theta;
                }
            }
        }

        public int GetCost() 
        {
            int res = 0;
            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    if (x[i, j] >= 0)
                        res += x[i, j] * c[i, j];
            return res;
        }

        public string ChekOporn()
        {
            int count = 0;
            for (int i = 0; i < m; ++i)
            {
                int t = 0;
                for (int j = 0; j < n; ++j)
                    if (x[i,j]!=-1)
                        t += x[i, j];
                if (t == a[i])
                    ++count;
            }
            for (int j = 0; j < n; ++j)
            {
                int t = 0;
                for (int i = 0; i < m; ++i)
                    if (x[i, j] != -1)
                        t += x[i, j];
                if (t == b[j])
                    ++count;
            }
            if (count != m + n)
                return "Перевезення не є планом, бо не всі споживачі/виробники залишились задоволеними.";

            count = 0;
            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    if (x[i, j] >= 0)
                        count++;
            if (count < m + n - 1)
                return "План вироджений";
            if (count > m + n - 1)
                return "Перевезення не є планом, бо кількість заповнених клітинок > m+n-1";
            count = 0;
            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    if (x[i, j] >= 0) 
                    {
                        for (int ii = 0; ii < m; ++ii)
                            for (int jj = 0; jj < n; ++jj)
                                was[ii, jj] = false;
                        cycleX.Clear();
                        cycleY.Clear();
                        path = "";
                        cycleFinded = false;
                        cycleCount = 1;
                        posX = i;
                        posY = j;
                        tt = true;
                        go(posX, posY, true);
                        if (cycleFinded)
                        {
                            int px = parrX[posX, posY];
                            int py = parrY[posX, posY];
                            cycleX.Add(posX);
                            cycleY.Add(posY);
                            cycleCount = 1;
                            int t = 0;
                            for (; !(px == posX && py == posY); t = px, px = parrX[t, py], py = parrY[t, py])
                            {
                                cycleX.Add(px);
                                cycleY.Add(py);
                                ++cycleCount;
                            }
                            path = "A" + (posX + 1) + "B" + (posY + 1);
                            for (int k = 1; k < cycleCount; ++k)
                                path += " - " + "A" + ((int)cycleX[k] + 1) + "B" + ((int)cycleY[k] + 1);
                            return "Знайдено цикл із базисних клітинок: " + path;
                        }
                    }
            return "";
        }

        public string ChekOpornNW()
        { 
            string s = ChekOporn();
            if (s != "")
                return s;
            TransportProblem t = new TransportProblem(m, n);
            for (int i = 0; i < m; ++i)
                t.a[i] = a[i];
            for (int i = 0; i < n; ++i)
                t.b[i] = b[i];
            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    t.c[i,j] = c[i,j];
            t.getPlanNorthWest();
            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    if (t.x[i, j] > 0 && t.x[i, j] != x[i, j])
                        return "План є опорний, але не побудований методом північно-західного кута";
            return "";
        }

        public string ChekOpornMinE() 
        {
            string s = ChekOporn();
            if (s != "")
                return s;
            TransportProblem t = new TransportProblem(m, n);
            for (int i = 0; i < m; ++i)
                t.a[i] = a[i];
            for (int i = 0; i < n; ++i)
                t.b[i] = b[i];
            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    t.c[i, j] = c[i, j];

            int flow = 0;
            for (int i = 0; i < m; ++i)
                flow += a[i];

            while (flow > 0)
            {
                int mi = 10000000;
                for (int i = 0; i < m; ++i)
                    for (int j = 0; j < n; ++j)
                    {
                        int p = Math.Min(t.a[i], t.b[j]);
                        if (mi > t.c[i, j] && p!=0)
                            mi = t.c[i, j];
                    }
                bool cont = false;
                for (int i = 0; i < m && !cont; ++i)
                    for (int j = 0; j < n && !cont; ++j)
                    {
                        int p = Math.Min(t.a[i], t.b[j]);
                        if (t.c[i, j] == mi && p!=0)
                        {
                            if (p == x[i, j])
                            {
                                t.x[i, j] = p;
                                t.a[i] -= p;
                                t.b[j] -= p;
                                cont = true;
                                flow -= p;
                            }
                        }
                    }
                if (cont == false)
                    return "План є опорний, але не побудований методом мінімального едемента.";
            }
            return "";
        }

        public string CheckPotencial() 
        {
            for (int i = 0; i < m; ++i )
                for (int j = 0; j < n; ++j)
                    if (x[i,j]>=0)
                        if (beta[j] - alpha[i] != c[i, j]) 
                        {
                            return "Альфа[і] та Бета[j] визначені НЕ правильно";
                        }
            

            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    if (x[i, j] < 0)
                    if (delta[i, j] != beta[j] - alpha[i] - c[i,j])
                        return "Альфа[і] та Бета[j] визначені правильно, але Дельта[i,j] пораховані НЕ правильно";
            return "";
        }

        public string FindCycle(bool b)
        {
            int count = 0;
            posX = 0;
            posY = 0;
            int d = 0;
            int co = 100000000;
            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    if (delta[i, j] > 0)
                    {
                        if (d < delta[i, j] || (d == delta[i, j] && co > c[i, j]))
                        {
                            co = c[i, j];
                            d = delta[i, j];
                            posX = i;
                            posY = j;
                        }
                        ++count;
                    }
            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    was[i, j] = false;
            x[posX, posY] = 0;
            cycleX.Clear();
            cycleY.Clear();
            path = "";
            cycleFinded = false;
            cycleCount = 1;
            tt = b;
            go(posX, posY, b);
            x[posX, posY] = -1;
            if (cycleFinded)
            {
                int px = parrX[posX, posY];
                int py = parrY[posX, posY];
                cycleX.Add(posX);
                cycleY.Add(posY);
                cycleCount = 1;
                int t = 0;
                for (; !(px == posX && py == posY); t = px, px = parrX[t, py], py = parrY[t, py])
                {
                    cycleX.Add(px);
                    cycleY.Add(py);
                    ++cycleCount;
                }
                path = "A" + (posX + 1) + "B" + (posY + 1);
                for (int k = 1; k < cycleCount; ++k)
                    path += "-" + "A" + ((int)cycleX[k] + 1) + "B" + ((int)cycleY[k] + 1);
                return path;
            }
            return "";
        }
    }
}

