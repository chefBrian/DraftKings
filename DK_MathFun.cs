using System.Collections.Generic;
using System.Linq;
using DraftKings.Models;

namespace DraftKings
{
    public static class DK_MathFun
    {
        public static long LoopCount { get; set; }

        static DK_MathFun()
        {
            LoopCount = 0;
        }
        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> elements, int k)
        {
            return k == 0 ? new[] { new T[0] } : elements.SelectMany((e, i) => elements.Skip(i + 1).Combinations(k - 1).Select(c => (new[] { e }).Concat(c)));
        }

        public static IEnumerable<IEnumerable<T>> Combs<T>(List<T> e, int size)
        {
            //LoopCount++;

            for (int i = 0; i < e.Count; i++)
            {
                //LoopCount++;
                if (size == 1)
                {
                    yield return new [] { e[i] };
                }

                foreach (var next in Combs(e.GetRange(i + 1, e.Count - (i + 1)), size - 1))
                {
                    //LoopCount++;
                    var newList = new [] { e[i] }.Concat(next);
                    //var rbCount = newList.Count(p => p.pn == "RB");
                    //var wrCount = newList.Count(p => p.pn == "WR");
                    //var teCount = newList.Count(p => p.pn == "TE");

                    //if (newList.Count(p => p.pn == "QB") > 1)
                    //{
                    //}
                    //else if (newList.Count(p => p.pn == "DST") > 1)
                    //{
                    //}

                    //else if ((rbCount > 2 && !(wrCount < 4 && teCount < 2)) ||
                    //         (wrCount > 3 && !(rbCount < 3 && teCount < 2)) ||
                    //         (teCount > 1 && !(wrCount < 4 && rbCount < 3)))
                    //{
                    //}
                    
                    //else if (newList.Sum(p => p.s) > 50000)
                    //{
                    //}

                    //else
                    //{
                        yield return newList;
                    //}
                }
            }
        }
    }
}