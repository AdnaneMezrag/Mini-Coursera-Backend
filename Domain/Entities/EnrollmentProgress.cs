using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EnrollmentProgress
    {
        // Both (EnrollmentId && ModuleContentId) must be unique
        // EnrollmentId should be indexed
        public int Id { get; set; } = default!;
        public int EnrollmentId { get; set; } = default!;
        public int ModuleContentId { get; set; } = default!;

    }
}
