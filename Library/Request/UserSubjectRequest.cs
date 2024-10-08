﻿using Library.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Request
{
    public class UserSubjectRequest : UserRequest
    {
        public List<SubjectResponse> SubjectResponses = new List<SubjectResponse>();
    }
}
