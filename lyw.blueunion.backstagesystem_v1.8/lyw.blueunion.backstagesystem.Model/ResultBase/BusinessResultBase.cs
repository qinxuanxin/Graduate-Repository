using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lyw.blueunion.backstagesystem.Model.ResultBase
{
  public  class BusinessResultBase
    {
      public string msg { get; set; }
      public string status { get; set; }
      
      public string Title { get; set; }
      public bool Status{get;set;}
      public string StatusCode { get; set; }
      public string StatusMessage { get; set; }
    }
}
