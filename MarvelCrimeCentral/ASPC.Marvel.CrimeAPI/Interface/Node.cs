using System;

namespace ASPC.Marvel.CrimeAPI
{
    [Serializable]
    public abstract class Node
    {
        #region Constructor

        public Node()
        {
            this.Id = Guid.NewGuid();
            this.Name = "New Node";
            this.Created = DateTimeOffset.UtcNow;
        }

        #endregion

        #region Properties

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }

        #endregion

        #region Override

        public override string ToString()
        {
            return this.Name;
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is Node)
            {
                Node compare = (Node)obj;
                return compare.Id == this.Id && compare.Created == this.Created && compare.Name == this.Name;
            }
            else { return false; }
        }

        #endregion
    }
}