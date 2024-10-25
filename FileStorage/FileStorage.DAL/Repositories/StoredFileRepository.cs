﻿using FileStorage.DAL.Data;
using FileStorage.DAL.Models;
using FileStorage.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.DAL.Repositories
{
    public class StoredFileRepository : IStoredFileRepository
    {
        private readonly FileStorageDbContext _context;

        public StoredFileRepository(FileStorageDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(StoredFile storedFile, CancellationToken cancellationToken = default)
        {
            await _context.StoredFiles.AddAsync(storedFile, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}