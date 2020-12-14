using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class UserGoDO
    {
        int idDriveGo;
        string userName;
        int idLine;
        int codeStopUp;
        int codeStopDown;
        TimeSpan timeUp;
        TimeSpan timeDown;

        public UserGoDO(int idDriveGo, string userName, int idLine, int codeStopUp,
                        int codeStopDown, TimeSpan timeUp, TimeSpan timeDown)
        {
            IdDriveGo=idDriveGo;
            UserName=userName;
            IdLine=idLine;
            CodeStopUp=codeStopUp;
            CodeStopDown=codeStopDown;
            TimeUp=timeUp;
            TimeDown=TimeDown;
        }

        public int IdDriveGo { get => idDriveGo; set => idDriveGo = value; }
        public string UserName { get => userName; set => userName = value; }
        public int IdLine { get => idLine; set => idLine = value; }
        public int CodeStopUp { get => codeStopUp; set => codeStopUp = value; }
        public int CodeStopDown { get => codeStopDown; set => codeStopDown = value; }
        public TimeSpan TimeUp { get => timeUp; set => timeUp = value; }
        public TimeSpan TimeDown { get => timeDown; set => timeDown = value; }
    }
}
