﻿namespace SGH.Shopper.Domain;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<IEnumerable<Product>> GetByContent(string content);
    Task<PagingResponse<Product>> GetAllAsync(PagingParameters parameters);
    Task<Product> GetByIdAsync(int id);

}
