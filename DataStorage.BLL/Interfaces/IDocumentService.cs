﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataStorage.BLL.DTOs;
using Microsoft.AspNetCore.Http;

namespace DataStorage.BLL.Interfaces
{
    public interface IDocumentService
    {
        Task<IEnumerable<DocumentDTO>> GetAll(string OwnerId);
        //Task<DocumentDTO> Get(Guid? id);
        //Task<IEnumerable<DocumentDTO>> GetChildren(Guid? id);
        Task Create(IFormFile uploadedFile);
        //Task Delete(Guid? id);
    }
}
