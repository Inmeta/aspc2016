using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;

namespace ASPC.Marvel.CrimeAPI.Controller
{
    public abstract class ODataControllerBase<T> : ODataController where T : Node
    {
        private IStorage<T> Storage;

        public ODataControllerBase(IStorage<T> storage) : base()
        {
            if (storage == null) { throw new Exception("Storage is NULL"); }
            this.Storage = storage;
        }

        [AllowAnonymous]
        // GET odata/Node
        [EnableQuery]
        public IQueryable<T> Get()
        {
            return Storage.Get();
        }

        // GET odata/Node(5)
        [AllowAnonymous]
        [EnableQuery]
        public SingleResult<T> Get([FromODataUri] Guid key)
        {
            return SingleResult<T>.Create<T>(Storage.Get().Where(node => node.Id == key).AsQueryable());
        }

        [AllowAnonymous]
        // PUT odata/Node(5)
        public IHttpActionResult Put([FromODataUri] Guid key, T node)
        {
            //HTTP PUT method specifies that an update operation MUST be carried out by using replace semantics.
            if (!ModelState.IsValid || node == null)
            {
                return BadRequest(ModelState);
            }

            if (key != node.Id) { return BadRequest(); }

            T temp = Storage.Get().Where(x => x.Id == key).SingleOrDefault();

            if (temp != null)
            {
                Storage.Remove(temp);
                Storage.Add(node);
            }

            if (!NodeExists(key)) { return NotFound(); }

            return Updated(node);
        }

        [AllowAnonymous]
        // POST odata/Node
        public IHttpActionResult Post(T node)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (NodeExists(node.Id))
            {
                return Conflict();
            }
            else
            {
                Storage.Add(node);
                return Created(node);
            }

        }

        // PATCH odata/Node(5)
        [AllowAnonymous]
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] Guid key, Delta<T> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            T node = Storage.Find(key);

            if (node == null) { return NotFound(); }

            patch.Patch(node);

            if (!NodeExists(key)) { return NotFound(); }

            return Updated(node);
        }

        [AllowAnonymous]
        //[Authorize(Roles = "superhero")]
        // DELETE odata/Node(5)
        public IHttpActionResult Delete([FromODataUri] Guid key)
        {
            T node = Storage.Find(key);
            if (node == null) { return NotFound(); }

            Storage.Remove(node);

            return StatusCode(HttpStatusCode.NoContent);
        }

        private bool NodeExists(Guid key)
        {
            return Storage.Get().Count(e => e.Id == key) > 0;
        }
    }
}
