using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hys.CareRIS.WebApi.Models
{
    /// <summary>
    /// Resource server
    /// </summary>
    public class Audience
    {   
        /// <summary>
        /// GUID
        /// </summary>
        [Key]
        [MaxLength(32)]
        public string ClientId { get; set; }

        /// <summary>
        /// Symmetric key
        /// </summary>
        [MaxLength(80)]
        [Required]
        public string Base64Secret { get; set; }

        /// <summary>
        /// Name of resource server
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
    }
}