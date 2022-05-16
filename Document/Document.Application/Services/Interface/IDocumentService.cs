﻿using Document.Application.DTO;
using Document.Domain.Models;
using Infrastructure.Shared.Interfaces;

namespace Document.Application.Services.Interface;

public interface IDocumentService : IServiceBase<Documents>
{
	Task<(List<Documents> list, int totalPages, int page)> GetAll(int page);
}