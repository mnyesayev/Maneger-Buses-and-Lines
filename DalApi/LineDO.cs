using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public enum Areas
    {
        General, North, South, Center, Jerusalem, Lowland, JudeaAndSamaria
    }
    /// <summary>
    /// 
    /// </summary>
    class LineDO
    {

        int idLine;
        int numLine;
        Areas area;
        int codeFirstStop;
        int codeLastStop;
        string moreInfo;
        /// <summary>
        /// Represents the unique number of the "Line"
        /// </summary>
        public int IdLine { get => idLine; set => idLine = value; }
        public int NumLine { get => numLine; set => numLine = value; }
        public Areas Area { get => area; set => area = value; }
        public int CodeFirstStop { get => codeFirstStop; set => codeFirstStop = value; }
        public int CodeLastStop { get => codeLastStop; set => codeLastStop = value; }
        public string MoreInfo { get => moreInfo; set => moreInfo = value; }
        public LineDO(int idLine = 0, int numLine = 0, Areas area = 0, int codeFirstStop = 0,
                        int codeLastStop = 0, string moreInfo = "")
        {
            IdLine = idLine;
            NumLine = numLine;
            Area = area;
            CodeFirstStop = codeFirstStop;
            CodeLastStop = codeLastStop;
            MoreInfo = moreInfo;
        }
    }
}
