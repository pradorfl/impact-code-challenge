﻿namespace CodeChallenge.Application.Dtos;
public sealed class ProductDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Size { get; set; }
    public int Stars { get; set; }
}
