using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumsSystem
{
    
    public class Policy
    {
        private int moderatorNum;
        private string passwordStructure;
        private string passwordEnsuringDegree;

        public Policy()
        {
        }
        public Policy(int moderN, string passStruct, string passEnsDeg)
        {
            moderatorNum = moderN;
            passwordStructure = passStruct;
            passwordEnsuringDegree = passEnsDeg;
        }

        public Boolean isValid(string password)
        {
            return true; 
        }

        internal void setNumOfModerators(int numOfModerators)
        {
            moderatorNum = numOfModerators;
        }

        internal void setstructureOfPassword(string structureOfPassword)
        {
            passwordStructure = structureOfPassword;
        }

        internal void setdefreeOfEnsuring(string degreeOfEnsuring)
        {
            passwordEnsuringDegree = degreeOfEnsuring;
        }
    }
}
