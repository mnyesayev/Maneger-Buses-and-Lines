using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class DriverDO
    {
        int id;
        string name;
        int seniority;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Seniority { get => seniority; set => seniority = value; }

        public DriverDO(int id, string name, int seniority)
        {
            Id = id;
            Name = name;
            Seniority = seniority;
        }
    }
}
