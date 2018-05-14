namespace Hys.Platform.Domain
{
    /// <summary>
    /// Base class for entities. This class provides base implementation for
    /// classes who will be entitled with "Entity"
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Gets or sets the unique id.
        /// </summary>
        /// <value>
        /// The unique id.
        /// </value>
        public virtual object UniqueId { get; set; }

        /// <summary>
        /// Determines whether this instance is transient.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is transient; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsTransient()
        {
            const bool ret = false;
            if (UniqueId == null || UniqueId == default(object))
            {
                return true;
            }
            return ret;
        }

        #region Overrided Methods

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            var entity = obj as Entity;
            if (entity.IsTransient() || IsTransient())
            {
                return false;
            }
            return UniqueId.Equals(entity.UniqueId);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return UniqueId == null ? base.GetHashCode() : UniqueId.GetHashCode();
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Entity left, Entity right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null) ? true : false;
            }
            return left.Equals(right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }

        #endregion Overrided Methods
    }
}