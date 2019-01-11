using samsung_api.Models.Interfaces;
using System.Collections.Generic;

namespace samsung.api.Models.Response
{
    public class GetTeachingSubjectsResponse
    {
        public IEnumerable<ITeachingSubject> teachingSubjects { get; set; }
    }
}