using DataTableToIEnumerable.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataTableToIEnumerable.Models
{
    /// <summary>
    /// User object.
    /// </summary>
    [TableFieldName("USER_TABLE")]
    public class User
    {
        /// <summary>
        /// UserName: Unique identifier to define the user.
        /// </summary>
        [Required]
        [TableFieldName("UserNameCode")]
        [JsonProperty(PropertyName = "Name")]
        public string UserName { get; set; }

        /// <summary>
        /// Groups: User groups which define the user's rights.
        /// </summary>
        [Required]
        [TableFieldName("GroupCode")]
        [JsonProperty(PropertyName = "Groups")]
        public string Groups { get; set; }
    }
}
