using ASPC.Marvel.CrimeAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Http.Results;
using System.Web.Http.Cors;

namespace ASPC.Marvel.CrimeAPI.Controller
{
    public class NodeController : ODataControllerBase<Node> { public NodeController() : base(StorageManager.Storage<Node>()) { } }

    public class CrimeController : ODataControllerBase<Crime> { public CrimeController() : base(StorageManager.Storage<Crime>()) {} }

    public class AgentController : ODataControllerBase<Agent> { public AgentController() : base(StorageManager.Storage<Agent>()) { } }
}