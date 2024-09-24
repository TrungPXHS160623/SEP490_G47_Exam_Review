﻿using Library.Common;
using Library.Models;

namespace WebClient.IServices
{
    public interface ICampusService
    {
        Task<ResultResponse<Campus>> GetCampus();
    }
}
