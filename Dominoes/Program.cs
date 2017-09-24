using System;
using System.Linq;

using Domino = System.Collections.Generic.List<int>;
using DominoChain = System.Collections.Generic.List<System.Collections.Generic.List<int>>;

namespace Dominoes
{
    public class Program
    {
        private static int maxChainLen = 0;

        private static void Main(string[] args)
        {
            Console.Write("Enter you dominoes: ");
            Func(ParseDoms(Console.ReadLine()), new DominoChain());
            Console.WriteLine("Length of the longest possible chain: " + maxChainLen.ToString());
            Console.ReadLine();
        }

        private static void Func(DominoChain dominoes, DominoChain chain)
        {
            int chainLen = chain.Count;

            if (chainLen > maxChainLen)
            {
                maxChainLen = chainLen;
            }

            for (int i = 0; i < dominoes.Count; i++)
            {
                DominoChain allExceptThis = new DominoChain();

                for (int j = 0; j < dominoes.Count; j++)
                {
                    if (j != i)
                    {
                        allExceptThis.Add(dominoes[i]);
                    }
                }

                DominoChain reversed = new DominoChain { new Domino { dominoes[i][1], dominoes[i][0] } };

                DominoChain newChain = new DominoChain();

                if (chain.Count == 0)
                {
                    newChain.Add(dominoes[i]);
                }
                else if (dominoes[i].First() == chain.Last().Last())
                {
                    newChain.AddRange(chain);
                    newChain.Add(dominoes[i]);
                }
                else if (dominoes[i].First() == chain.First().First())
                {
                    newChain.AddRange(reversed);
                    newChain.AddRange(chain);
                }
                else if (dominoes[i].Last() == chain.First().First())
                {
                    newChain.Add(dominoes[i]);
                    newChain.AddRange(chain);
                }
                else if (dominoes[i].Last() == chain.Last().Last())
                {
                    newChain.AddRange(chain);
                    newChain.AddRange(reversed);
                }

                Func(allExceptThis, newChain);
            }
        }

        private static DominoChain ParseDoms(string strDoms)
        {
            DominoChain doms = new DominoChain();

            string[] arrValues = strDoms.Split(GetNumberSeparators(), StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < arrValues.Length; i++)
            {
                if (i % 2 == 0)
                {
                    doms.Add(new Domino());
                }

                doms.Last().Add(int.Parse(arrValues[i]));
            }

            return doms;
        }

        private static char[] GetNumberSeparators()
        {
            char[] sep = new char[86];
            char c = ' ';

            for (int i = 0; i < sep.Length; i++)
            {
                sep[i] = c++;

                if (c == '0')
                {
                    c = ':';
                }
            }

            return sep;
        }
    }
}