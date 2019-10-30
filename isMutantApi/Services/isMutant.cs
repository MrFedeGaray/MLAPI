using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;


namespace isMutantApi.Services
{
    class IsMutant
    {
        private string[] Adn { get; set; }
        private static int X = 6, Y = 6;
        private static int[] Dirx = { 1, 0, 1, -1 };
        private static int[] diry = { 0, 1, 1, 1 };
        private static int AdnLen = 4;
        private static int MaxMatches = 2;
        private static int Matches = 0;
        private List<(int x, int y)> ValidCoordinates { get; set; }
        public bool Checkable;

        public IsMutant(string[] dna)
        {
            //Adn = makeSubject(dna);
            if (CheckDnaStructure(dna))
            {
                Adn = dna;
                Checkable = true;
            }
            else
            {
                Checkable = false;
            }
            //CheckDna();
        }

        private string[] makeSubject(string adn)
        {
            var subject = adn.Split(',');
            return subject;
        }


        public bool CheckDnaStructure(string[] dna)
        {
            if (dna.Length != 6)
            {
                return false;
            }
            foreach (string chain in dna)
            {
                Regex rx = new Regex(@"(?![A,C,G,T])\w+");
                if (rx.IsMatch(chain) || chain.Length != 6)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckDna()
        {
            ValidCoordinates = new List<(int x, int y)>();
            for (int i = 0; i < Y; i++)
            {
                for (int j = 0; j < X; j++)
                {
                    for (int direction = 0; direction < Dirx.Length; direction++)
                    {
                        var currentCoordinates = new List<(int x, int y)>();
                        string sample = "";
                        GetSample(i, j, direction, out sample, out currentCoordinates);
                        if (CheckSample(sample, currentCoordinates) == true)
                        {
                            Matches++;
                            if (Matches == MaxMatches)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private bool CheckSample(string sample, List<(int x, int y)> currentCoordinates)
        {
            bool _checked = false;
            Regex rx = new Regex(@"(C{4,}|A{4,}|T{4,}|G{4,})");
            if (sample.Length == 4)
            {
                if (rx.IsMatch(sample))
                {
                    int count = 0;
                    //checkear si no es un falso positivo: Caso A A A A A A puede dar 3 matcheos diferentes si comparte mas de una coordenada con una secuencia valida entoces esta superpuesta con la misma y se descarta
                    foreach (var t in currentCoordinates)
                    {
                        if (ValidCoordinates.Contains(t))
                        {
                            count++;
                            if (count > 1)
                            {
                                return _checked;
                            }
                        }
                    }
                    ValidCoordinates.AddRange(currentCoordinates);
                    _checked = true;
                }
            }
            return _checked;
        }


        private void GetSample(int i, int j, int direction, out string sample, out List<(int x, int y)> currentCoordinates)
        {
            sample = "";
            var coordinates = new List<(int x, int y)>();

            int xd = j, yd = i;
            for (int k = 0; k < AdnLen; k++)
            {
                //salir si el proximo valor esta fuera de los parametros
                if (xd >= X || xd < 0 || yd >= Y || yd < 0)
                {
                    break;
                }
                coordinates.Add((xd, yd));
                sample = sample + Adn[yd][xd];
                xd = xd + Dirx[direction];
                yd = yd + diry[direction];
            }
            currentCoordinates = coordinates;
        }
    }
}