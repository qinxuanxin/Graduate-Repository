using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lyw.blueunion.backstagesystem.Dal.Authority;
namespace lyw.blueunion.backstagesystem.Bll.Authority
{
   public class AuthorityBll
    {
       AuthorityDal authordal = new AuthorityDal();
       public string VerificationAuthority(string actionName)
       {
           string responseText = "";
          responseText=authordal.VerificationAuthority(actionName);
          return responseText;
       }
    }
}
