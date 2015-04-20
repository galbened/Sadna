using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    interface IPolicyManager
    {
        Boolean isValid(string password);
        Boolean setNumOfModerators(int numOfModerators);
        Boolean setdefreeOfEnsuring(string degreeOfEnsuring);
        Boolean setMinLenght(int len); 
        Boolean setUpper(Boolean upper);
        Boolean setLower(Boolean lower);
        Boolean setNums(Boolean nums);
        Boolean setSymbs(Boolean symbs);
    }
}
