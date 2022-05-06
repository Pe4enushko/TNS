using NUnit.Framework;
using System;
using Desktop_TNS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSTester
{
    class DBTest
    {
        [Test]
        public void AddSubHWTest()
        {
            // series	title	ports	transmit_standard	speed	address
            // АО567 - ТНС - 11    Точка доступа HP ProCurve   RG 45   802.11  до 35 Мбит / с    1077:10:18
            Assert.IsTrue(DBWork.AddSubHardware("АО567 - ТНС - 11", "Точка доступа HP ProCurve ", "RG 45", "802.11", "до 35 Мбит / с", "1077:10:18"));
        }
        [Test]
        public void AddHighwayHWTest()
        {
            // series	title	frequency	fade_coefficent	transmit_technology	address
            // М0123ТНС312 Транспондер TS - 100E 17,2999992370605    WDM СПб Кронверкский пр. д. 5
            Assert.IsTrue(DBWork.AddHighWayHardware("М0123ТНС312", "Транспондер TS - 100E", 17.2999992370605, "WDM", "СПб", "Кронверкский пр. д. 5"));
        }
        [Test]
        public void AddWebHWTest()
        {
            // series	title	ports_count	transmit_standard	frequency	interfaces	speed	address
            // СД12ТНС_01 Точка доступа Cisco AIR - SAP702I     24  IEEE 802.1p 17,6    SSI 1 Гбит СПб, В.О., 4 линия, д. 41
            Assert.IsTrue(DBWork.AddWebHardware("СД12ТНС_01", "Точка доступа Cisco AIR - SAP702I", "24", "IEEE 802.1p", 17.6, "SSI", "1 Гбит", "СПб, В.О., 4 линия, д. 41"));
        }
    }
}
