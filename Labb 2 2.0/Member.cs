using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Labb_2_2._0
{
    internal class Member : User
    {
        public enum Level
        {
            Bronze,
            Silver,
            Gold
        }

        private Level RewardLevel { get; set; }
        public Member(string name, string password, Level rewardlevel): base(name, password)
        {
            RewardLevel = rewardlevel;   
        }
        
        public override string ToString()
        {
            return $"{Name}\n{Password}\n{RewardLevel}\n*******************";
        }
        public double DecideBonusLevel(double price)
        {
            double discount = 1.0;

            switch(RewardLevel)
            {
                case Level.Bronze:
                    discount = 0.95;
                    break;
                case Level.Silver:
                    discount = 0.90;
                    break;
                case Level.Gold:
                    discount = 0.85;
                    break;
            }
           return discount * price;
        }
        
        
    }
}
