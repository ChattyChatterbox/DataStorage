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
        Task<DocumentDTO> Get(Guid? id);
        Task<DocumentDTO> Add(IFormFile uploadedFile);
        //Task<Guid> Create(DocumentEntity document);
        //bool Delete(Guid? id);
    }
}