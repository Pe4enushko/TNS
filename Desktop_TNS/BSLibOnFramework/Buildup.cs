using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseStationsLibrary
{
    public struct Buildup
    {
        public const double tight = 1.21;
        public const double medium = 0.9;
        public const double village = 0.47;
        /// <summary>
        /// 0 = tight <br/>
        /// 1 = medium <br/> 
        /// 2 = village
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static double GetBuildupByNumber(int num)
        {
            switch (num)
            {
                case 0: 
                    return tight;
                case 1:
                    return medium;
                case 3:
                    return village;
                default: throw new ArgumentException("Wrong number!");
                    break;
            }
        }
    }
}
