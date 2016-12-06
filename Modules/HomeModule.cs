using Nancy;
using System.Collections.Generic;
using System;
using ToDo.Objects;

namespace ToDo
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["ToDo.cshtml"];
      };

    }
  }
}
