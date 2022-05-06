using System;
using System.Linq;
using System.Net.Http;

namespace BaseStationsLibrary
{
    public static class Calculations
    {
        /// <summary>
        /// Get R0
        /// </summary>
        /// <param name="serviceRegionS"></param>
        /// <returns></returns>
        private static double GetServiceRadius(int serviceRegionS)
        {
            return Math.Sqrt(serviceRegionS/Math.PI);
        }
       /// <summary>
       /// Get L
       /// </summary>
       /// <param name="buildup"></param>
       /// <param name="serviceRegionS"></param>
       /// <returns></returns>
        public static double GetCombCount(double buildup,int serviceRegionS)
        {
            return buildup * (Math.Pow(GetServiceRadius(serviceRegionS)/Math.Sqrt(serviceRegionS/Math.PI),2));
        }
        /// <summary>
        /// Combs count (L) / Base stations count (C) 
        /// </summary>
        /// <param name="Cluster"></param>
        /// <param name="BSCount"></param>
        /// <returns></returns>
        public static double GetBaseStationCount(double Cluster,int BSCount)
        {
            return Cluster / BSCount;
        }
        public static double GetCluster(double D1, double D2, double D3)
        {
            double[] Dn = new double[] { D1, D2, D3 };
            Array.Sort(Dn);
            return Math.Pow(Dn[0], 5 / 2) + Math.Pow(Dn[1], 3 / 2) + Math.Pow(Dn[0], 1 / 2);
        }
    }
}
