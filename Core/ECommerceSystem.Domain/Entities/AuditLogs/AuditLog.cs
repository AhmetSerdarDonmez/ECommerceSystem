using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Commons;
using ECommerceSystem.Domain.Entities.Users;

namespace ECommerceSystem.Domain.Entities.AuditLogs
{
    public class AuditLog : CommonTime
    {

        public int LogId { get; set; } 
        public string TableName { get; set; }
        public int RecordId { get; set; }
        public string ColumnName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        public int? ChangedByUserId { get; set; }
        public DateTime ChangedAt 
        { 
            get => UpdatedAt;
            set => UpdatedAt = value;
        }
        public string TransactionType { get; set; }

        // Navigation
        public virtual User ChangedByUser { get; set; }




    }
}
