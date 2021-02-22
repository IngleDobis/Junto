namespace Junto.Domain.Model
{
    /// <summary>
    /// User model.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        public virtual long Id { get; set; }

        /// <summary>
        /// Gets or sets Login.
        /// </summary>
        public virtual string Login { get; set; }

        /// <summary>
        /// Gets or sets Password.
        /// </summary>
        public virtual string Password { get; set; }
    }
}
