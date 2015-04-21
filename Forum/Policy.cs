using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forum
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

        public Policy(int moderN, string passEnsDeg, Boolean upper, Boolean lower, Boolean nums, Boolean symbs, int mLen)
        {
            moderatorNum = moderN;
            passwordEnsuringDegree = passEnsDeg;
            upperCase = upper;
            lowerCase = lower;
            numbers = nums;
            symbols = symbs;
            minLength = mLen;
        }

        public Boolean isValid(string password)
        {
            Boolean up = !upperCase;
            Boolean low = !lowerCase;
            Boolean nums = !numbers;
            Boolean symbs = !symbols;
            if (password.Length < minLength)
                return false;
            for (int i = 0; i < password.Length; i++)
            {
                if (!up && password[i] > 'A' && password[i] < 'Z')
                {
                    up = true;
                    continue;
                }
                if (!low && password[i] > 'a' && password[i] < 'z')
                {
                    low = true;
                    continue;
                }
                if (!nums && password[i] > '0' && password[i] < '9')
                {
                    nums = true;
                    continue;
                }
                if (!symbs && ((password[i] > 32 && password[i] < 48)
                    || (password[i] > 57 && password[i] < 65) ||
                    (password[i] > 90 && password[i] < 97) ||
                    (password[i] > 122 && password[i] < 127)))
                {
                    symbs = true;
                    continue;
                }
            }
            return (up & low & nums & symbs);
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
