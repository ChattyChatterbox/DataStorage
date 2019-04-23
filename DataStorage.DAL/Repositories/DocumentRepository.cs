﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataStorage.DAL.Entities;
using DataStorage.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DataStorage.DAL.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly ApplicationContext _context;
        public DocumentRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DocumentEntity>> GetAllDocumentsRelatedAsync(string parentId)
        {
            return await _context.Documents.Where(d => d.ParentId == parentId && d.ParentId != string.Empty).ToListAsync();
        }

        public async Task<IEnumerable<DocumentEntity>> GetAllUserDocumentsAsync(string ownerId)
        {
            return await _context.Documents.Where(d => d.OwnerId == ownerId).ToListAsync();
        }

        public async Task CreateDocumentAsync(DocumentEntity document)
        {
            var doc = _context.Documents.Where(d => d.DocumentId == document.DocumentId);
            if (doc.Count() == 0)
            {
                var newdoc = await _context.Documents.AddAsync(document);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<DocumentEntity> GetDocumentByIdAsync(string id)
        {
            return await _context.Documents.Where(f => f.DocumentId == id).FirstOrDefaultAsync();
        }

        public async Task<bool> IsUserDocumentOwner(string documentId, string userId)
        {
            var document = await _context.Documents.Where(doc => doc.OwnerId == userId).FirstOrDefaultAsync();

            if (document != null)
            {
                return document.DocumentId == documentId;
            }

            return false;
        }

        public string GenerateAccessLink()
        {
            byte[] time = BitConverter.GetBytes(DateTime.Now.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();

            return Convert.ToBase64String(time.Concat(key).ToArray());
        }

        public async Task UpdateDocumentAsync(DocumentEntity document)
        {
            _context.Documents.Update(document);
            await _context.SaveChangesAsync();
        }

        public async Task<DocumentEntity> GetPublicDocumentByLinkAsync(string link)
        {
            return await _context.Documents.Where(doc => doc.DocumentLink == link && doc.IsPublic == true).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserDocument>> GetAllAvailbleDocumentsForUserAsync(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            return user.UserDocuments.ToList();
        }

        public async Task<UserDocument> GetAvailbleDocumentForUserAsync(string link, string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            return user.UserDocuments.FirstOrDefault(doc => doc.DocumentLink == link);;
        }

        public async Task DeleteDocumentAsync(string id)
        {
            _context.Documents.Remove(GetDocumentByIdAsync(id).Result);
            await _context.SaveChangesAsync();
        }

        public string GetDocumentPathById(string id)
        {
            return GetDocumentByIdAsync(id).Result.Path;
        }
    }
}
