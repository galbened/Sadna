using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Policy
{
    
    public class Policy
    {
        private int moderatorNum;
        private string passwordEnsuringDegree;
        private int minLength;
        private Boolean upperCase;
        private Boolean lowerCase;
        private Boolean numbers;
        private Boolean symbols;


        public Policy()
        {
        }

        public Policy(int moderN, string passEnsDeg, Boolean upper, Boolean lower, Boolean nums, Boolean symbs)
        {
            moderatorNum = moderN;
            passwordEnsuringDegree = passEnsDeg;
            upperCase = upper;
            lowerCase = lower;
            numbers = nums;
            symbols = symbs;
        }

        public Boolean isValid(string password)
        {
            return true; 
        }

        internal void setNumOfModerators(int numOfModerators)
        {
            moderatorNum = numOfModerators;
        }

        
        internal void setdefreeOfEnsuring(string degreeOfEnsuring)
        {
            passwordEnsuringDegree = degreeOfEnsuring;
        }


    }
}
