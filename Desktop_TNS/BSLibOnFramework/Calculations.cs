using BSLibOnFramework;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BaseStationsLibrary
{
    // S, id из БД
    // R = D/2s
    // Rn = Math.Sqrt(Sn/PI)
    // L = K*(Math.Sqrt(s/PI)/R)^2
    // C = (2*R1^5/2 + 2*R2^3/2 + 2*R3^1/2)
    // n = L/C
    public static class Calculations
    {
        
        public static async Task<double> DoWork(double buildup, int[] ids,int BSindex)
        {
            double[] Cs = GetClusterAreas(ids);
            double C = GetCluster(Cs);
            for (int i = 0; i < Cs.Length; i++)
            {
                Cs[i] = Math.Pow((Cs[i]/2),2) * Math.PI;
            }
            double S = Cs.Sum();
            double L = buildup * Math.Pow((Math.Sqrt(S / Math.PI)/Cs[BSindex]),2);
            double handover = await HandoverCheck.GetHandover(ids[BSindex]);
            if (handover > 1)
                return L / C;
            else
                return L / C * 1.4;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        private static double[] GetClusterAreas(int[] ids)
        {
            double[] results = new double[ids.Length];
            for (int i = 0; i < ids.Length; i++)
            {
                results[i] = DBWork.GetSquare(ids[i]);
            }
            return results;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="D1"></param>
        /// <param name="D2"></param>
        /// <param name="D3"></param>
        /// <returns>C</returns>
        private static double GetCluster(double D1, double D2, double D3)
        {
            double[] Dn = new double[] { D1, D2, D3 };
            Array.Sort(Dn);
            return Math.Pow(Dn[0], 5 / 2) + Math.Pow(Dn[1], 3 / 2) + Math.Pow(Dn[0], 1 / 2);
        }
        private static double GetCluster(double[] Dn)
        {
            Array.Sort(Dn);
            return Math.Pow(Dn[0], 5 / 2) + Math.Pow(Dn[1], 3 / 2) + Math.Pow(Dn[0], 1 / 2);
        }
    }
}
