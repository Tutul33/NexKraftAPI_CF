using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DataAccess.ORM.CodeFirst
{
    public class Chat
    {
        [Key]
        public int id { get; set; }
        public int groupId { get; set; }
        public string messages { get; set; }
        public string mediaUrl { get; set; }
        public string mediaExt { get; set; }
        public int fromUserId { get; set; }
        public int toUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool isActive { get; set; }
    }
}
