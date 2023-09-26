using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_2_2._0
{
    internal class RewardSystem //: User
    {
        public enum Level
        {
            Bronze,
            Silver,
            Gold,
        }
        
        private Level RewardLevel { get; set; }
        

        public RewardSystem(Level rewardlevel) 
        {
            Level Rewardlevel = rewardlevel;
        }

        public double DecideBonusLevel()
        {
            double discount = 0;

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
            return discount;
        }
    }
}
