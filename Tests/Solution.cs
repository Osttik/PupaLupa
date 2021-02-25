using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    class Solution { 
        public string solution(string S, string T)
        {
            if (S == T)
            {
                return "EQUAL";
            }

            if (T.Length - S.Length == 1)
            {
                int distingElementIndex = 0;
                int sIndex = 0;
                int changes = 0;
                for(int tIndex = 0; tIndex < T.Length; tIndex++)
                {
                    if (S[sIndex] != T[tIndex])
                    {
                        changes++;
                        distingElementIndex = tIndex;
                        sIndex--;
                    }
                    sIndex++;
                }
                if (changes == 1)
                {
                    return "INSERT " + T[distingElementIndex];
                }
            }
            else if (S.Length == T.Length)
            {
                List<int> distinctValuesIndexes = new List<int>();
                for(int i = 0; i < S.Length; i++)
                {
                    if (S[i] != T[i])
                    {
                        distinctValuesIndexes.Add(i);
                    }
                }

                switch (distinctValuesIndexes.Count())
                {
                    case 1:
                        {
                            return $"REPLACE {S[distinctValuesIndexes[0]]} {T[distinctValuesIndexes[0]]}";
                        }
                    case 2:
                        {
                            if (S[distinctValuesIndexes[0]] == T[distinctValuesIndexes[1]] &&
                                S[distinctValuesIndexes[1]] == T[distinctValuesIndexes[0]])
                                return $"SWAP {S[distinctValuesIndexes[0]]} {S[distinctValuesIndexes[1]]}";
                        }
                        break;
                    default:
                        break;
                }
            }

            return "IMPOSSIBLE";
        }
    }
}
