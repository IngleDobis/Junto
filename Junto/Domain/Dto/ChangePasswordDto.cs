using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Junto.Domain
{
    public class ChangePasswordDto
    {
        /// <summary>
        /// Gets or sets CurrentPassword
        /// </summary>
        public string CurrentPassword { get; set; }

        /// <summary>
        /// Gets or sets NewPassword
        /// </summary>
        public string NewPassword { get; set; }
    }
}
