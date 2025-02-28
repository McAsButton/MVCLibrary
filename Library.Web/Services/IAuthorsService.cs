using System.Collections.Generic;
using Humanizer;
using Library.Common.Core;
using Library.Common.DTOs;
using Library.Web.Data;
using Library.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Web.Services
{
    public interface IAuthorsService
    {
        public Task<Response<Author>> CreateAsync(AuthorDTO dto);
        public Task<Response<object>> DeleteAsync(int id);
        public Task<Response<Author>>EditAsync(AuthorDTO dto);
        public Task<Response<AuthorDTO>> GetOne(int id);
        public Task<Response<List<Author>>> GetListAsync();
    }

    public class AuthorsService : IAuthorsService
    {
        private readonly DataContext _context;

        public AuthorsService(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<Author>> CreateAsync(AuthorDTO dto)
        {
            try
            {
                Author author = new Author
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName
                };

                await _context.Authors.AddAsync(author);
                await _context.SaveChangesAsync();

                Response<Author> response = new Response<Author>
                {
                    IsSuccess = true,
                    Message = "Autor creado con éxito",
                    Result = author
                };

                return response;
            }
            catch (Exception ex)
            {
                Response<Author> response = new Response<Author>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };

                return response;
            }
        }

        public async Task<Response<object>> DeleteAsync(int id)
        {
            try
            {
                Author? author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

                if (author is null)
                {
                    return new Response<object>
                    {
                        IsSuccess = false,
                        Message = $"El autor con id {id} no existe",
                    };
                }

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();

                return new Response<object>
                {
                    IsSuccess = true,
                    Message = $"Autor eliminado con éxito."
                };
            }
            catch (Exception ex)
            {
                return new Response<object>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }

        }

        public async Task<Response<Author>> EditAsync(AuthorDTO dto)
        {
            try
            {
                Author? author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == dto.Id);

                if (author is null)
                {
                    return new Response<Author>
                    {
                        IsSuccess = false,
                        Message = $"El autor con id {dto.Id} no existe",
                    };
                }

                author.FirstName = dto.FirstName;
                author.LastName = dto.LastName;

                _context.Authors.Update(author);
                await _context.SaveChangesAsync();

                Response<Author> response = new Response<Author>
                {
                    IsSuccess = true,
                    Message = "Autor actualizado con éxito",
                    Result = author
                };

                return response;
            }
            catch (Exception ex)
            {
                Response<Author> response = new Response<Author>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };

                return response;
            }
        }

        public async Task<Response<List<Author>>> GetListAsync()
        {
            try
            {
                List<Author> list = await _context.Authors.ToListAsync();

                Response<List<Author>> response = new Response<List<Author>>
                {
                    IsSuccess = true,
                    Message = "Lista obtenida con éxito",
                    Result = list
                };
                return response;
            }
            catch (Exception ex)
            {
                Response<List<Author>> response = new Response<List<Author>>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };

                return response;
            }
        }

        public async Task<Response<AuthorDTO>> GetOne(int id)
        {
            try
            {
                Author? author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

                if (author is null)
                {
                    return new Response<AuthorDTO>
                    {
                        IsSuccess = false,
                        Message = $"El autor con id {id} no existe",
                    };
                }

                return new Response<AuthorDTO>
                {
                    IsSuccess = true,
                    Message = "Autor obtenido con éxito",
                    Result = new AuthorDTO
                    {
                        Id = author.Id,
                        FirstName = author.FirstName,
                        LastName = author.LastName
                    }
                };
            }
            catch (Exception ex)
            {
                Response<AuthorDTO> response = new Response<AuthorDTO>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
                return response;
            }
        }
    }
}
